using Loja.Domain.Entities;
using Loja.Repository.EntityConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loja.Repository.Context
{
    public class LojaDbContext : IdentityDbContext<User>
    {

        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Estabelecimento> Estabelecimentos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Servico> Servicos { get; set; }        
        

        public LojaDbContext(DbContextOptions<LojaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new AgendamentoConfig());
            modelBuilder.ApplyConfiguration(new AtendimentoConfig());
            modelBuilder.ApplyConfiguration(new AtendimentoItemConfig());
            modelBuilder.ApplyConfiguration(new EnderecoConfig());
            modelBuilder.ApplyConfiguration(new EstabelecimentoConfig());
            modelBuilder.ApplyConfiguration(new EstadoConfig());
            modelBuilder.ApplyConfiguration(new MunicipioConfig());            
            modelBuilder.ApplyConfiguration(new ServicoConfig());
            modelBuilder.ApplyConfiguration(new SituacaoConfig());            
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserEstabelecimentoConfig());            

            IgnoreDefaultColumnsAspNetUsers(modelBuilder);
        }

        private static void IgnoreDefaultColumnsAspNetUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Ignore(i => i.NormalizedEmail)
                .Ignore(i => i.EmailConfirmed)
                .Ignore(i => i.PhoneNumber)
                .Ignore(i => i.PhoneNumberConfirmed)
                .Ignore(i => i.TwoFactorEnabled)
                .Ignore(i => i.LockoutEnabled)
                .Ignore(i => i.AccessFailedCount)
                .Ignore(i => i.LockoutEnd);            
        }       
    }
}
