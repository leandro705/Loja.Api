using Loja.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Repository.EntityConfig
{
    public class UserEstabelecimentoConfig : IEntityTypeConfiguration<UserEstabelecimento>
    {
        public void Configure(EntityTypeBuilder<UserEstabelecimento> builder)
        {
            builder.ToTable("UserEstabelecimento");

            builder.HasKey(x => new { x.UserId, x.EstabelecimentoId });

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.UserEstabelecimentos)
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne(x => x.Estabelecimento)
                .WithMany(x => x.UserEstabelecimentos)
                .HasForeignKey(x => x.EstabelecimentoId);
        }
    }
}
