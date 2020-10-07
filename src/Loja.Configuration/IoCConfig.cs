using Loja.Application.Services;
using Loja.Domain.Interfaces.Repository;
using Loja.Domain.Interfaces.Services;
using Loja.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Loja.Configuration
{
    public static class IoCConfig
    {
        public static void AddIoC(this IServiceCollection services)
        {
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();

            services.AddScoped<IMunicipioService, MunicipioService>();
            services.AddScoped<IMunicipioRepository, MunicipioRepository>();

            services.AddScoped<IAgendamentoService, AgendamentoService>();
            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

            services.AddScoped<IAtendimentoService, AtendimentoService>();
            services.AddScoped<IAtendimentoRepository, AtendimentoRepository>();

            services.AddScoped<IEstabelecimentoService, EstabelecimentoService>();
            services.AddScoped<IEstabelecimentoRepository, EstabelecimentoRepository>();

            services.AddScoped<IServicoService, ServicoService>();
            services.AddScoped<IServicoRepository, ServicoRepository>();

            services.AddTransient<UserService>();

            services.AddScoped<IEmailService, EmailService>();
       
        }
    }
}
