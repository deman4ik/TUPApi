using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
