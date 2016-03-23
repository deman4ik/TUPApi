using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupapiService.Helpers.DBHelpers;
using tupapiService.Models;

namespace tupapiService.Test.Infrastructure
{
   public class TestContext : ITupapiContext
    {
       public TestContext()
       {
           this.Users = new TestDbSet<User>();
            this.Accounts = new TestDbSet<Account>();
            this.Posts = new TestDbSet<Post>();
            this.Votes = new TestDbSet<Vote>();
       }

       public DbSet<User> Users { get; }
       public DbSet<Account> Accounts { get; }
       public DbSet<Post> Posts { get; }
       public DbSet<Vote> Votes { get; }

        public int SaveChanges()
       {
           return 0;
       }

       public void MarkAsModified(object item)
       {
           
       }

       public EfStatus SaveChangesWithValidation()
       {
            return new EfStatus();
        }

       public void Dispose()
        {

        }
    }
}
