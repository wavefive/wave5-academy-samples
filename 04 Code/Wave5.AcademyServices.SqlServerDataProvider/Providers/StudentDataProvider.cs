using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RCode.Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RCode.Data.SqlServerDataProviders;

namespace Wave5.AcademyServices.Data;

public class StudentDataProvider : BaseEntitySqlServerDataProvider<Student, AcademyDbContext>, IStudentDataProvider
{
    #region [ CTor ]
    public StudentDataProvider(
        ILogger<StudentDataProvider> logger,
        IDbContextFactory<AcademyDbContext> dbContextFactory) : base(logger, dbContextFactory) {
        
    }
    #endregion

    #region [ Public Methods - List ]
    public async Task<List<Student>> GetByLastNameAsync(string lastName) {
        try {
            using var context = base.GetContext();
            var dbResult = await context
                                            .Set<Student>()
                                            .AsNoTracking()
                                            .Where(x => x.LastName.Equals(lastName, StringComparison.InvariantCultureIgnoreCase))
                                            .ToListAsync();
            return dbResult;
        }
        catch (Exception ex) {
            throw DataProviderExceptionFactory.GetListException<Student>(ex);
        }
    }
    #endregion
}