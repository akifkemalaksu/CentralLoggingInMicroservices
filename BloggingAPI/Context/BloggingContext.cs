using BloggingAPI.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedModels.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SharedModels.Services;

namespace BloggingAPI.Context
{
    public class BloggingContext : DbContext
    {
        private readonly IMessageBus _messageBus;
        public BloggingContext(DbContextOptions options, IMessageBus messageBus) : base(options)
        {
            _messageBus = messageBus;
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public override int SaveChanges() => OnBeforeSaveChanges(Guid.NewGuid().ToString());

        private int OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();

            var addedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
            var deletedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted).ToList();

            foreach (var entity in modifiedEntries)
            {
                var auditEntry = new AuditEntry()
                {
                    TableName = entity.Entity.GetType().GetCustomAttributes<TableAttribute>().Single().Name,
                    UserId = userId.ToString(),
                    Type = EntityState.Modified.ToString()
                };

                foreach (var property in entity.Properties)
                {
                    var propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;

                    if (property.IsModified)
                    {
                        var currentValue = property.CurrentValue;
                        var originalValue = entity.GetDatabaseValues().GetValue<object>(propertyName);
                        if (currentValue != originalValue)
                        {
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            auditEntry.OldValues[propertyName] = entity.GetDatabaseValues().GetValue<object>(propertyName);
                        }
                    }
                }

                _messageBus.Send(auditEntry.ToAudit(), "BloggingAPIAuditLogCreated");
            }

            int record = base.SaveChanges();

            foreach (var entity in addedEntries)
            {
                var auditEntry = new AuditEntry()
                {
                    TableName = entity.Entity.GetType().GetCustomAttributes<TableAttribute>().Single().Name,
                    UserId = userId.ToString(),
                    Type = EntityState.Added.ToString(),
                };

                foreach (var property in entity.Properties)
                {
                    var propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;

                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                }

                _messageBus.Send(auditEntry.ToAudit(), "BloggingAPIAuditLogCreated");
            }

            foreach (var entity in deletedEntries)
            {
                var auditEntry = new AuditEntry()
                {
                    TableName = entity.Entity.GetType().GetCustomAttributes<TableAttribute>().Single().Name,
                    UserId = userId.ToString(),
                    Type = EntityState.Deleted.ToString()
                };

                foreach (var property in entity.Properties)
                {
                    var propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;

                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                }

                _messageBus.Send(auditEntry.ToAudit(), "BloggingAPIAuditLogCreated");
            }

            return record;
        }
    }
}
