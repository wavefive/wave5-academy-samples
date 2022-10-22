using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RCode;

namespace Wave5.AcademyServices.Data;

public static class ServiceExtension
{
    #region [ Public Methods - Service ]
    public static void AddAcademyServicesDataProviders(
                            this IServiceCollection services, 
                            IConfiguration configuration,
                            bool usedInWebApp = false,
                            string connectionStringKey = "wave5-academy-services-data-dev") {

            var connectionString = configuration.GetConnectionString(connectionStringKey);
            Guard.IsNullOrEmpty(connectionString, $"Connection string {connectionStringKey} is not set.");

            var options = new DbContextOptions<AcademyDbContext>();
            var builder = new DbContextOptionsBuilder<AcademyDbContext>(options);
            builder.UseSqlServer(connectionString);
            builder.EnableSensitiveDataLogging();

            services.AddPooledDbContextFactory<AcademyDbContext>(options => {
                options.UseSqlServer(connectionString, sqlServerOptionsAction => {
                    sqlServerOptionsAction.EnableRetryOnFailure();
                });
                options.EnableSensitiveDataLogging();
            });

            services.AddAcademyServicesDataProviders();
    }

    public static void AddAcademyServicesDataProviders(this IServiceCollection services, bool usedInWebApp = false) {
        if (usedInWebApp) {
            services.AddScoped<AcademyDbContext>();
            services.AddScoped<IStudentDataProvider, StudentDataProvider>();

        } else {
            services.AddTransient<IStudentDataProvider, StudentDataProvider>();
        }
    }
    #endregion
}