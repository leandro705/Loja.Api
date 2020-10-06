using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Repository.EntityConfig
{
    public class EstabelecimentoConfig : IEntityTypeConfiguration<Estabelecimento>
    {
        public void Configure(EntityTypeBuilder<Estabelecimento> builder)
        {
            builder.ToTable("Estabelecimento");

            builder.HasKey(x => x.EstabelecimentoId);

            builder.Property(x => x.Nome)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasColumnType("varchar(500)")
                .IsRequired();

            builder.Property(x => x.Url)
                .HasColumnType("varchar(25)")
                .IsRequired();

            builder.Property(x => x.Celular)
               .HasColumnType("varchar(15)")
               .IsRequired();

            builder.Property(x => x.Telefone)
                .HasColumnType("varchar(15)");

            builder.Property(x => x.Instagram)
                .HasColumnType("varchar(150)");

            builder.Property(x => x.Facebook)
                .HasColumnType("varchar(150)");

            builder.Property(x => x.DataCadastro)
               .HasColumnType("datetime2");

            builder.HasOne(x => x.Situacao)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
