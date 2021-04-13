using Domain.AuditableEntity;
using Domain.Entities;
using Domain.Identity;
using Domain.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        private IDbContextTransaction _transaction;
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        #region Entities
        public virtual DbSet<AppLogItem> AppLogItems { get; set; }
        public virtual DbSet<AppDataProtectionKey> AppDataProtectionKeys { get; set; }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityCategory> ActivityCategories { get; set; }
        public virtual DbSet<Attendee> Attendees { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<UserFollowing> UserFollowings { get; set; }
        public virtual DbSet<UserPhoto> UserPhotos { get; set; }
        #endregion
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");
            }
            _transaction.Rollback();
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");
            }
            _transaction.Commit();
        }

        public override void Dispose()
        {
            _transaction?.Dispose();
            base.Dispose();
        }

        public void ExecuteSqlInterpolatedCommand(FormattableString query)
        {
            Database.ExecuteSqlInterpolated(query);
        }

        public void ExecuteSqlRawCommand(string query, params object[] parameters)
        {
            Database.ExecuteSqlRaw(query, parameters);
        }

        public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
        {
            var value = this.Entry(entity).Property(propertyName).CurrentValue;
            return value != null
                ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
                : default;
        }

        public object GetShadowPropertyValue(object entity, string propertyName)
        {
            return this.Entry(entity).Property(propertyName).CurrentValue;
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Update(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges(); //NOTE: changeTracker.Entries<T>() will call it automatically.

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        private void beforeSaveTriggers()
        {
            validateEntities();
            setShadowProperties();
        }

        private void setShadowProperties()
        {
            // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
            var props = this.GetService<IHttpContextAccessor>()?.GetShadowProperties();
            ChangeTracker.SetAuditableEntityPropertyValues(props);
        }

        private void validateEntities()
        {
            var errors = this.GetValidationErrors();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
                var loggerFactory = this.GetService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
                logger.LogError(errors);
                throw new InvalidOperationException(errors);
            }
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.AddCustomConfigurations();

            builder.AddAuditableShadowProperties();
        }
    }
}
