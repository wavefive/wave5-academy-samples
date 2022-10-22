using Microsoft.Extensions.Logging;
using RCode;
using Wave5.AcademyServices.Data;

namespace Wave5.AcademyServices;

public class StudentLogicProvider : 
        BaseEntityLogicProvider<Student, IStudentDataProvider>, IStudentLogicProvider
{
    #region [ CTor ]
    public StudentLogicProvider(
        ILogger<StudentLogicProvider> logger,
        IStudentDataProvider dataProvider) : base(logger, dataProvider) {
       
    }
    #endregion
   
    #region [ Public Methods - Lists ]
    public Task<List<Student>> GetByLastNameAsync(string lastName) {
        Guard.ParamIsNullOrEmpty(lastName, nameof(lastName));

        return this._dataProvider.GetByLastNameAsync(lastName);
    }
    #endregion
}