using System;
using System.Collections.Generic;
using tupapi.Shared.Enums;
using tupapi.Shared.Enums.Auth;
using tupapi.Shared.Helpers;
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

        public void PopulateDb(int userAmount, int postsPerUser = 10)
        {
            PopulateUsers(userAmount);
            PopulateStandartAccounts(userAmount);
            PopulatePosts(postsPerUser, userAmount);
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
                SaltedAndHashedPassword = AuthHelper.Hash("user" + numb + "pwd", salt),
                IsBlocked = numb == 2 //  only for "u2"
            };
        }

        public List<User> GetUsers(int amount)
        {
            var result = new List<User>();

            for (var i = 1; i <= amount; i++)
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
            var result = new List<Account>();

            for (var i = 1; i <= amount; i++)
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

        public Post GetPost(int numb, int usernumb)
        {
            return new Post
            {
                Id = "p" + numb + "u" + usernumb,
                UserId = "u" + usernumb,
                CreatedAt = DateTimeOffset.Now.AddDays(-numb),
                Deleted = false,
                Description = "selfie",
                PhotoUrl = "https://pp.vk.me/c635102/v635102178/2976/XUhTojLUKQs.jpg",
                Status = numb%2 == 0 ? PhotoStatus.Running : PhotoStatus.Ended,
                Type = PhotoType.Basic
            };
        }

        public List<Post> GetPosts(int amountPerUser, int userAmount)
        {
            var result = new List<Post>();

            for (var i = 1; i <= userAmount; i++)
            {
                for (var j = 1; j <= amountPerUser; j++)
                {
                    result.Add(
                        GetPost(j, i)
                        );
                }
            }
            return result;
        }

        public void PopulatePosts(int amountPerUser, int userAmount)
        {
            foreach (var post in GetPosts(amountPerUser, userAmount))
            {
                _context.Posts.Add(post);
            }
        }
    }
}