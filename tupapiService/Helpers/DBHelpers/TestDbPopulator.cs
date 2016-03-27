using System;
using System.Collections.Generic;
using tupapi.Shared.Enums.Auth;
using tupapiService.Models;

namespace tupapiService.Helpers.DBHelpers
{
    public class TestDbPopulator
    {
        private readonly ITupapiContext _context;

        public TestDbPopulator(ITupapiContext context)
        {
            _context = context;
        }

        public void PopulateDb(int userAmount)
        {
            PopulateUsers(userAmount);
            PopulateStandartAccounts(userAmount);
        }
        public User GetUser(int numb)
        {
            var salt = AuthHelper.GenerateSalt();
            return new User
            {
                Id = "u" + numb,
                CreatedAt = DateTimeOffset.Now.AddDays(-numb),
                Deleted = false,
                Email = "user" + numb + "@example.com",
                Name = "user" + numb,
                Salt = salt,
                SaltedAndHashedPassword = AuthHelper.Hash("user" + numb + "pwd", salt)
            };
        }

        public List<User> GetUsers(int amount)
        {
            List<User> result = new List<User>();

            for (int i = 1; i <= amount; i++)
            {
                result.Add(
                    GetUser(i)
                    );
            }
            return result;
        }

        public void PopulateUsers(int amount)
        {
            foreach (var user in GetUsers(amount))
            {
                _context.Users.Add(user);
            }
        }

        public Account GetStandartAccount(int numb)
        {
            return new Account
            {
                Id = "ac" + numb,
                CreatedAt = DateTimeOffset.Now.AddDays(-numb),
                Deleted = false,
                UserId = "u" + numb,
                AccountId = Const.Standart + ":" + "u" + numb,
                Provider = Provider.Standart,
                ProviderId = "u" + numb
            };
        }

        public List<Account> GetStandartAccounts(int amount)
        {
            List<Account> result = new List<Account>();

            for (int i = 1; i <= amount; i++)
            {
                result.Add(
                    GetStandartAccount(i)
                    );
            }
            return result;
        }

        public void PopulateStandartAccounts(int amount)
        {
            foreach (var acc in GetStandartAccounts(amount))
            {
                _context.Accounts.Add(acc);
            }
        }
    }
}