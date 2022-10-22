using Microsoft.EntityFrameworkCore;
using RCode;
using System;
using System.Linq;

namespace Wave5.AcademyServices.Data;

public partial class AcademyDbContext : DbContext
{
    #region [ CTor ]

    public AcademyDbContext(DbContextOptions<AcademyDbContext> options) : base(options) {
        base.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    #endregion

    #region [ Properties ]
    public DbSet<Student> Students { get; set; }
    #endregion

    #region [ Protected Override Methods ]
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        this.CreateAcademyModels(modelBuilder);
    }
    #endregion

    #region [ Public Override Methods ]
    public override int SaveChanges() {
        var changedEntities = this.ChangeTracker.Entries()
                                                .Where(x => x.State == EntityState.Added
                                                         || x.State == EntityState.Modified);
        foreach (var entity in changedEntities) {
            var dateTimeOffset = DateTime.UtcNow;

            if (entity.State == EntityState.Added) {
                entity.Property(nameof(BaseEntity.CreatedAt)).CurrentValue = dateTimeOffset;
            }
            entity.Property(nameof(BaseEntity.UpdatedAt)).CurrentValue = dateTimeOffset;
        }
        return base.SaveChanges();
    }
    #endregion
}