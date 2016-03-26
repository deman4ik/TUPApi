using System;
using System.Diagnostics;
using tupapi.Shared.DataObjects;
using tupapi.Shared.Enums.Auth;
using tupapiService.Helpers;
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

        public User CreateUser(Provider provider, StandartRegistrationRequest request)
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
    }
}