using Microsoft.EntityFrameworkCore;
using RCode.Data;
using RCode.Data.SqlServerDataProviders;

namespace Wave5.AcademyServices.Data;

public partial class AcademyDbContext
{
    #region [ Public Methods - ModelBuilder ]
    private void CreateAcademyModels(ModelBuilder modelBuilder) {
        this.CreateModel_Student(modelBuilder);
    }
    #endregion

    #region [ Privae Methods ]
    private void CreateModel_Student(ModelBuilder modelBuilder) {
        modelBuilder.CreateModel_BaseEntity<Student>(nameof(Student));

        modelBuilder.Entity<Student>().Property(x => x.FirstName)
            .HasColumnType("nvarchar")
            .HasMaxLength(DataModelHelpers.DISPLAY_NAME_FIELD_LENGTH);

        modelBuilder.Entity<Student>().Property(x => x.LastName)
            .HasColumnType("nvarchar")
            .HasMaxLength(DataModelHelpers.DISPLAY_NAME_FIELD_LENGTH);
    }
    #endregion
}
