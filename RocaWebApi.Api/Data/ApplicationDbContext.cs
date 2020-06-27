using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RocaWebApi.Api.Base.Entity;
using RocaWebApi.Api.Features.Users;
using RocaWebApi.Api.Features.Workers;

namespace RocaWebApi.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Worker> Workers { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                // 1. Check if has DeletedAt property
                var property = entityType.FindProperty("DeletedAt");

                if (property != null)
                {
                    // 2. Create the query filter
                    var parameter = Expression.Parameter(entityType.ClrType);

                    // EF.Property<DateTimeOffset>(post, "DeletedAt")
                    var propertyMethodInfo =
                        typeof(EF).GetMethod("Property")?.MakeGenericMethod(typeof(DateTimeOffset?));

                    if (propertyMethodInfo == null) continue;

                    var deletedAtProperty = Expression.Call(propertyMethodInfo, parameter,
                        Expression.Constant("DeletedAt"));

                    // EF.Property<DateTimeOffset>(post, "DeletedAt") == null
                    var compareExpression = Expression.MakeBinary(ExpressionType.Equal, deletedAtProperty,
                        Expression.Constant(null));

                    // post => EF.Property<DateTimeOffset>(post, "DeletedAt") == null
                    var lambda = Expression.Lambda(compareExpression, parameter);

                    builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is TrackableEntity trackable)
                {
                    var now = DateTimeOffset.UtcNow;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            trackable.CreatedAt = now;
                            break;
                        case EntityState.Modified:
                            trackable.CreatedAt = now;
                            trackable.UpdatedAt = now;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            trackable.DeletedAt = now;
                            break;
                    }
                }
            }
        }
    }
}
