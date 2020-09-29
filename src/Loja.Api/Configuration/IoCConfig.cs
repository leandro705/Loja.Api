using Loja.Application.Services;
using Loja.Domain.Interfaces.Repository;
using Loja.Domain.Interfaces.Services;
using Loja.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Loja.Api.Configuration
{
    public static class IoCConfig
    {
        public static void AddIoC(this IServiceCollection services)
        {
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();

            services.AddScoped<IMunicipioService, MunicipioService>();
            services.AddScoped<IMunicipioRepository, MunicipioRepository>();

            services.AddTransient<UserService>();

            services.AddScoped<IEmailService, EmailService>();
       
        }
    }
}
