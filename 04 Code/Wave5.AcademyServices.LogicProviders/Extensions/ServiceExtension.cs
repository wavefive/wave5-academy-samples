using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wave5.AcademyServices;

public static class ServiceExtension
{
    #region [ Public Methods - Service ]
    public static void AddAcademyServicesLogicProviders(this IServiceCollection services, IConfiguration configuration) {

        services.AddAcademyServicesLogicProviders();
    }

    public static void AddAcademyServicesLogicProviders(this IServiceCollection services) {
        services.AddTransient<IStudentLogicProvider, StudentLogicProvider>();
        services.AddTransient<LogicContext>();
    }
    #endregion
}