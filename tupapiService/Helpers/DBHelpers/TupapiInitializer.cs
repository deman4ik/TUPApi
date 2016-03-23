using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using tupapi.Shared.Data;
using tupapiService.Models;

namespace tupapiService.Helpers.DBHelpers
{
    public class TupapiInitializer : DropCreateDatabaseIfModelChanges<TupapiContext>
    {
        protected override void Seed(TupapiContext context)
        {
            try
            {
                var data = new DataGenerator();
                foreach (var user in data.Users)
                {
                    context.Users.Add(new User()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                        IsBlocked = user.IsBlocked,
                        Type = user.Type
                    });
                }

                context.SaveChanges();
                base.Seed(context);
            }
            catch (DbEntityValidationException ex)
            {
                Debug.WriteLine("################################################");
                Debug.WriteLine(ex);
                foreach (var err in ex.EntityValidationErrors)
                {
                    Debug.WriteLine(err.Entry);
                    foreach (var errr in err.ValidationErrors)
                    {
                        Debug.WriteLine(errr.ErrorMessage);
                    }
                }
                
                Debug.WriteLine("################################################");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }
    }
}