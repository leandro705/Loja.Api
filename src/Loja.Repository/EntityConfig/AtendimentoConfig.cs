using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Repository.EntityConfig
{
    public class AtendimentoConfig : IEntityTypeConfiguration<Atendimento>
    {
        public void Configure(EntityTypeBuilder<Atendimento> builder)
        {
            builder.ToTable("Atendimento");

            builder.HasKey(x => x.AtendimentoId);

            builder.Property(x => x.DataAtendimento)
               .HasColumnType("datetime2")
               .IsRequired();

            builder.Property(x => x.DataCadastro)
               .HasColumnType("datetime2")
               .IsRequired();

            builder.Property(x => x.Valor)
               .HasColumnType("decimal(10,2)")
               .IsRequired();

            builder.Property(x => x.Desconto)
               .HasColumnType("decimal(10,2)")
               .IsRequired();

            builder.Property(x => x.ValorTotal)
               .HasColumnType("decimal(10,2)")
               .IsRequired();

            builder.Property(x => x.Observacao)
               .HasColumnType("varchar(500)");

            builder.HasOne(x => x.Situacao);
            builder.HasOne(x => x.Usuario);
            builder.HasOne(x => x.Estabelecimento);           

            builder
                .HasOne(x => x.Agendamento)
                .WithMany(x => x.Atendimentos)
                .HasForeignKey(x => x.AgendamentoId);

            builder
                .HasMany(x => x.AtendimentoItens)
                .WithOne()                
                .HasForeignKey("AtendimentoId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
