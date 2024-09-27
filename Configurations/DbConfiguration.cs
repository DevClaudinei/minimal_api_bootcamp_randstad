using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using minimal_api.Infrastructure.Data;

namespace minimal_api.Configurations
{
    public static class DbConfiguration
    {
        public static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {
            string connectionString = configuration.GetConnectionString("mysql");

            services.AddDbContext<DataContext>(options => 
            {
                options.UseMySql(configuration.GetConnectionString("mysql"),
                                ServerVersion.AutoDetect(connectionString));
            }, contextLifetime: ServiceLifetime.Transient);
        }
    }
}