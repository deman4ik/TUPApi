using System;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Web.Configuration;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Login;
using tupapi.Shared.DataObjects;
using tupapi.Shared.Enums;
using tupapi.Shared.Enums.Auth;
using tupapiService.Helpers;
using tupapiService.Helpers.ExceptionHelpers;
using tupapiService.Models;
using User = tupapiService.Models.User;

namespace tupapiService.Authentication
{
    public class BaseAuth
    {
        private readonly ITupapiContext _context;

        public BaseAuth(ITupapiContext context)
        {
            _context = context;
        }

        public User CreateUser(Provider provider, StandartAuthRequest request)
        {
            User newUser = null;
            string providerName = null;
            string providerId = null;
            string accesstoken = null;

            if (provider == Provider.Standart)
            {
                var salt = AuthHelper.GenerateSalt();
                newUser = new User
                {
                    Id = SequentialGuid.NewGuid().ToString(),
                    Name = request.Name,
                    Email = request.Email,
                    Salt = salt,
                    SaltedAndHashedPassword = AuthHelper.Hash(request.Password, salt)
                };
                providerName = Const.Standart;
                providerId = newUser.Id;
            }

            _context.Users.Add(newUser);
            _context.SaveChanges();
            CreateAccount(provider, providerName, newUser.Id, providerId);
            return newUser;
        }

        public void CreateAccount(Provider provider, string providerName, string userId, string providerId,
            string accesstoken = null)
        {
            try
            {
                Account newAccount = new Account
                {
                    Id = SequentialGuid.NewGuid().ToString(),
                    AccountId = providerName + ":" + userId,
                    UserId = userId,
                    Provider = provider,
                    ProviderId = providerId,
                    AccessToken = accesstoken
                };
                _context.Accounts.Add(newAccount);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public void CheckPassword(User user, string password)
        {
            if (user.SaltedAndHashedPassword == null || user.Salt == null)
                throw new ApiException(ApiResult.Validation, ErrorType.UserNoPassword, user.Id);
            var pass = AuthHelper.Hash(password, user.Salt);
            if (!AuthHelper.SlowEquals(pass, user.SaltedAndHashedPassword))
                throw new ApiException(ApiResult.Denied, ErrorType.PasswordWrong, password);
        }

        public JwtSecurityToken CreateToken(string accountId)
        {
            return AppServiceLoginHandler.CreateToken(new Claim[] { new Claim(JwtRegisteredClaimNames.Sub, accountId) },
              ConfigurationManager.AppSettings["SigningKey"],
                 ConfigurationManager.AppSettings["ValidAudience"],
                ConfigurationManager.AppSettings["ValidIssuer"],
                TimeSpan.FromHours(24));
           
        }
    }
}