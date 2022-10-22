using Microsoft.EntityFrameworkCore;
using RCode;
using RCode.Data;

namespace RCode.Data.SqlServerDataProviders;

public static class ModelBuilderExtensions
{
    #region [ Methods -]
    public static void CreateModel_BaseEntity<TEntity>(this ModelBuilder modelBuilder, string tableName)
        where TEntity : BaseEntity {

        modelBuilder.Entity<TEntity>()
            .ToTable(tableName)
            .HasKey(x => x.Id);

        modelBuilder.Entity<TEntity>().Property(x => x.Id)
            .HasColumnType("nvarchar")
            .HasMaxLength(DataModelHelpers.ID_FIELD_LENGTH);

        modelBuilder.Entity<TEntity>().Ignore(nameof(BaseEntity.EntityType));

        modelBuilder.Entity<TEntity>().Property(x => x.CreatedAt)
            .HasColumnType("datetime")
            .HasDefaultValueSql("getutcdate()")
            .IsRequired();

        modelBuilder.Entity<TEntity>().Property(x => x.UpdatedAt)
            .HasColumnType("datetime")
            .HasDefaultValueSql("getutcdate()")
            .IsRequired();

        modelBuilder.Entity<TEntity>().Property(x => x.IsActive)
            .HasColumnType("bit")
            .HasDefaultValueSql("1")
            .IsRequired();
    }
    #endregion
}