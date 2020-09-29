using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Repository.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(x => x.Nome)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(x => x.Telefone)
               .HasColumnType("varchar(12)");

            builder.Property(x => x.Celular)
               .HasColumnType("varchar(12)");

            builder.HasOne(x => x.Endereco);
        }
    }
}
