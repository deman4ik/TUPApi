using System;
using System.Data.Entity;
using tupapiService.Helpers.DBHelpers;

namespace tupapiService.Models
{
    public interface ITupapiContext : IDisposable
    {
        DbSet<User> Users { get; }
        DbSet<Account> Accounts { get; }
        DbSet<Post> Posts { get; }
        DbSet<Vote> Votes { get; }
        int SaveChanges();
        void MarkAsModified(object item);
        EfStatus SaveChangesWithValidation();
    }
}