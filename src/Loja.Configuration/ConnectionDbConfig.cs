using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Loja.Repository.Context;

namespace Loja.Configuration
{
    public static class ConnectionDbConfig
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<LojaDbContext>(options => options
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))
                .UseSqlServer(connectionString));
        }
    }
}
