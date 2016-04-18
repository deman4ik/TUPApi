using System;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using System.Linq;
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
    public static class BaseAuth
    {
        public static User CreateUser(ITupapiContext context, Provider provider, StandartAuthRequest request)
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

            context.Users.Add(newUser);
            context.SaveChanges();
            CreateAccount(context, provider, providerName, newUser.Id, providerId);
            return newUser;
        }

        public static void CreateAccount(ITupapiContext context, Provider provider, string providerName, string userId,
            string providerId,
            string accesstoken = null)
        {
            try
            {
                Account newAccount = new Account
                {
                    Id = SequentialGuid.NewGuid(),
                    AccountId = providerName + ":" + userId,
                    UserId = userId,
                    Provider = provider,
                    ProviderId = providerId,
                    AccessToken = accesstoken
                };
                context.Accounts.Add(newAccount);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public static void CheckPassword(User user, string password)
        {
            if (user.SaltedAndHashedPassword == null || user.Salt == null)
                throw new ApiException(ApiResult.Validation, ErrorType.UserNoPassword, user.Id);
            var pass = AuthHelper.Hash(password, user.Salt);
            if (!AuthHelper.SlowEquals(pass, user.SaltedAndHashedPassword))
                throw new ApiException(ApiResult.Denied, ErrorType.PasswordWrong, password);
        }

        public static string CreateToken(string accountId)
        {
            var token =
                AppServiceLoginHandler.CreateToken(new Claim[] {new Claim(JwtRegisteredClaimNames.Sub, accountId)},
                    ConfigurationManager.AppSettings["SigningKey"],
                    ConfigurationManager.AppSettings["ValidAudience"],
                    ConfigurationManager.AppSettings["ValidIssuer"],
                    TimeSpan.FromHours(24));

            return token.RawData;
        }

        public static string GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier) == null)
                throw new ApiException(ApiResult.Denied, ErrorType.ClaimNotFound, null);
            return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static User GetUser(ITupapiContext context, ClaimsPrincipal claimsPrincipal)
        {
            string userId = GetUserId(claimsPrincipal);
            var user = context.Users.AsNoTracking().SingleOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ApiException(ApiResult.Denied, ErrorType.UserNotFound, userId);
            return user;
        }
    }
}