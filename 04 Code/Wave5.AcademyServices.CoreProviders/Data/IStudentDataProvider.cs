using RCode.Data.Providers;

namespace Wave5.AcademyServices.Data;

public interface IStudentDataProvider : IEntityDataProvider<Student>, IStudentProvider
{

}