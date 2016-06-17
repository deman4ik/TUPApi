using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using Microsoft.Azure.Mobile.Server.Tables;
using tupapiService.Helpers.DBHelpers;

namespace tupapiService.Models
{
    public class TupapiContext : DbContext, ITupapiContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public TupapiContext() : base(connectionStringName)
        {
        }

        public void MarkAsModified(object item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public EfStatus SaveChangesWithValidation()
        {
            var status = new EfStatus();
            try
            {
                SaveChanges(); //then update it
            }
            catch (DbEntityValidationException ex)
            {
                return status.SetErrors(ex.EntityValidationErrors);
            }
            return status;
        }

        //protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        //{
        //    DbEntityValidationResult result = DbValidationHelper.ValidateEntity(entityEntry, items, this);
        //    return result ?? base.ValidateEntity(entityEntry, items);
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
            /* User */
            modelBuilder.Entity<User>()
                .HasMany(s => s.Accounts)
                .WithRequired(s => s.User)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<User>()
                .HasMany(s => s.Posts)
                .WithRequired(s => s.User)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<User>()
                .HasMany(s => s.Votes)
                .WithRequired(s => s.User)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(false);


            /* Post */
            modelBuilder.Entity<Post>()
                .HasMany(s => s.Votes)
                .WithRequired(s => s.Post)
                .HasForeignKey(s => s.PostId);
        }

        public DbSet<DataObjects.PostDTO> PostDTOes { get; set; }

        public DbSet<DataObjects.VoteDTO> VoteDTOes { get; set; }
    }
}