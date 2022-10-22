using Microsoft.Extensions.Logging;
using RCode.Data.Providers;

namespace Wave5.AcademyServices.Data;

public class StudentDataProvider : BaseMockDataProvider<Student>, IStudentDataProvider
{
    #region [ CTor ]
    public StudentDataProvider(ILogger<StudentDataProvider> logger) 
        : base(logger, SeedProvider.Current.LoadStudents()) {

    }
    #endregion

    #region [ Public Methods - Lists ]
    public Task<List<Student>> GetByLastNameAsync(string lastName) {
        var result = this._sourceItems
                    .Where(x => x.LastName.Equals(lastName, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();

        return Task.FromResult(result);
    }
    #endregion
}