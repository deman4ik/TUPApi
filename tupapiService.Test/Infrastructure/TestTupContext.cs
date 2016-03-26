using System.Data.Entity;
using tupapiService.Helpers.DBHelpers;
using tupapiService.Models;

namespace tupapiService.Test.Infrastructure
{
    public class TestTupContext : ITupapiContext
    {
        public TestTupContext()
        {
            Users = new TestDbSet<User>();
            Accounts = new TestDbSet<Account>();
            Posts = new TestDbSet<Post>();
            Votes = new TestDbSet<Vote>();
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