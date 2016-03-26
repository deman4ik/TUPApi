using System;
using System.Collections.Generic;
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
    }
}