using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data;

public class ContextBase : DbContext
{
    public ContextBase()
    {
    }

    public ContextBase(DbContextOptions options)
        : base(options)
    {
    }

    public override int SaveChanges()
    {
        SetCreatedAndModifiedDate();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetCreatedAndModifiedDate();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetCreatedAndModifiedDate()
    {
        var now = DateTime.UtcNow;
        foreach (var entityEntry in ChangeTracker.Entries().Where((Func<EntityEntry, bool>)(e =>
                 {
                     if (!(e.Entity is ITrackModified) && !(e.Entity is ITrackCreated))
                         return false;
                     return e.State == EntityState.Added || e.State == EntityState.Modified;
                 })))
        {
            if (entityEntry.State == EntityState.Added && entityEntry.Entity is ITrackModified entity1)
                entity1.ModifiedDate = now;
            if (entityEntry.State == EntityState.Added && entityEntry.Entity is ITrackCreated entity2)
                entity2.CreatedDate = now;
        }
    }
}