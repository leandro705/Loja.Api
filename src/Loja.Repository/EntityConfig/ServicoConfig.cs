using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Repository.EntityConfig
{
    public class ServicoConfig : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.ToTable("Servico");

            builder.HasKey(x => x.ServicoId);

            builder.Property(x => x.Nome)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.Duracao)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.DataCadastro)
               .HasColumnType("datetime2");

            builder.HasOne(x => x.Situacao)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Estabelecimento)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
