using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Repository.EntityConfig
{
    public class SituacaoConfig : IEntityTypeConfiguration<Situacao>
    {
        public void Configure(EntityTypeBuilder<Situacao> builder)
        {
            builder.ToTable("Situacao");

            builder.HasKey(x => x.SituacaoId);

            builder.Property(x => x.Nome)
                .HasColumnType("varchar(150)")
                .IsRequired();
        }
    }
}
