using RCode;

namespace Wave5.AcademyServices;

public interface IStudentProvider : IEntityProvider<Student>
{
    #region [ Methods - Lists ]
    Task<List<Student>> GetByLastNameAsync(string lastName);
    #endregion
}