using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Repository.EntityConfig
{
    public class AtendimentoItemConfig : IEntityTypeConfiguration<AtendimentoItem>
    {
        public void Configure(EntityTypeBuilder<AtendimentoItem> builder)
        {
            builder.ToTable("AtendimentoItem");

            builder.HasKey(x => x.AtendimentoItemId);
            
            builder.Property(x => x.Valor)
               .HasColumnType("decimal(10,2)")
               .IsRequired();
            
            builder.HasOne(x => x.Servico);

        }
    }
}
