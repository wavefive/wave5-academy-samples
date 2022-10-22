using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wave5.AcademyServices.Data;

public static class ServiceExtension
{
    #region [ Public Methods - Service ]
    public static void AddAcademyServicesDataProviders(this IServiceCollection services, IConfiguration configuration) {
        services.AddAcademyServicesDataProviders();
    }

    public static void AddAcademyServicesDataProviders(this IServiceCollection services) {
        services.AddTransient<IStudentDataProvider, StudentDataProvider>();
    }
    #endregion
}