using System.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Swashbuckle.Swagger;
using tupapiService.Models;

namespace tupapiService.Helpers.DBHelpers
{
    public static class DbValidationHelper
    {
        public static DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry,
            IDictionary<object, object> items, ITupapiContext context)
        {
            var result = new List<DbValidationError>();
            
            if (entityEntry.Entity is Account &&
                (entityEntry.State == EntityState.Added
                 || entityEntry.State == EntityState.Modified))
            {
                var accountToCheck = ((Account) entityEntry.Entity);

                //check for uniqueness of Account ID for User
                if (
                    context.Accounts.Any(
                        x => x.AccountId != accountToCheck.AccountId && x.UserId == accountToCheck.UserId))
                    result.Add(new DbValidationError("AccountId",
                                    $"The Account ID on Account with Provider: '{accountToCheck.Provider}' and Provider Id: '{accountToCheck.ProviderId}' for User Id: '{accountToCheck.UserId}' must be unique."));
                        }

            return result.Count > 0 ? new DbEntityValidationResult(entityEntry, result) : null;
        }
    }
}