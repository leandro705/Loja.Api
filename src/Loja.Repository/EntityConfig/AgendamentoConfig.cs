using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Repository.EntityConfig
{
    public class AgendamentoConfig : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.ToTable("Agendamento");

            builder.HasKey(x => x.AgendamentoId);

            builder.Property(x => x.DataAgendamento)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.DataFinalAgendamento)
               .HasColumnType("datetime2")
               .IsRequired();

            builder.Property(x => x.DataCadastro)
               .HasColumnType("datetime2")
               .IsRequired();

            builder.Property(x => x.Observacao)
               .HasColumnType("varchar(500)");            

            builder.HasOne(x => x.Situacao);
            builder.HasOne(x => x.Servico);
            builder.HasOne(x => x.Usuario);
            builder.HasOne(x => x.Estabelecimento);
        }
    }
}
