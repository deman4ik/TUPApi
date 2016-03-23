using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupapi.Shared.DataObjects;
using tupapi.Shared.Enums;

namespace tupapi.Shared.Data
{
    public class DataGenerator
    {
        public List<User> Users { get; set; } 
        public List<Post> Posts { get; set; }
        public DataGenerator(int usersAmount = 10, int postsAmount = 10)
        {
            Users = new List<User>();
            Posts = new List<Post>();
            GenerateUsers(usersAmount);
            GeneratePosts(postsAmount);


        }

        public void GenerateUsers(int amount)
        {
            for (var i = 1; i <= amount; i++)
            {
                Users.Add(new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTimeOffset.Now,
                    Deleted = false,
                    UpdatedAt = null,
                    Email = "user"+ i + "@example.com",
                    IsBlocked = false,
                    Name = "User" + i,
                    Type = UserType.Basic

                });
            }
        }

        public void GeneratePosts(int amount)
        {

           Random rnd = new Random();
                foreach (var user in Users)
                {
                    Posts.Add(new Post()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedAt = DateTimeOffset.Now,
                        Deleted = false,
                        UpdatedAt = null,
                        Description = "Some Description for photo",
                        Likes =  (int)rnd.NextDouble(),
                        PhotoUrl = "https://pp.vk.me/c543103/v543103973/2740/ImPTggg8Spk.jpg",
                        Status = (int)rnd.NextDouble() % 2 == 0 ? PhotoStatus.Running : PhotoStatus.Ended,
                        UserId = user.Id,
                        UserName = user.Name

                    });
                }
               
            
        }
    }
}
