using Loja.Application.Services;
using Loja.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Loja.Api.Configuration
{
    public static class IoCConfig
    {
        public static void AddIoC(this IServiceCollection services)
        {
            //services.AddScoped<IClientService, ClientService>();
            //services.AddScoped<IClientRepository, ClientRepository>();
            services.AddTransient<UserService>();

            services.AddScoped<IEmailService, EmailService>();
       
        }
    }
}
