using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
                var dbpop = new TestDbPopulator(context);
                dbpop.PopulateDb(10);
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