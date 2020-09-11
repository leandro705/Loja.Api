using Loja.Domain.Entities;
using Loja.Repository.EntityConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loja.Repository.Context
{
    public class LojaDbContext : IdentityDbContext<User>
    {
        public LojaDbContext(DbContextOptions<LojaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfig());           

            IgnoreDefaultColumnsAspNetUsers(modelBuilder);
        }

        private static void IgnoreDefaultColumnsAspNetUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Ignore(i => i.NormalizedEmail)
                //.Ignore(i => i.NormalizedUserName)
                //.Ignore(i => i.Email)
                .Ignore(i => i.EmailConfirmed)
                //.Ignore(i => i.SecurityStamp)
                .Ignore(i => i.PhoneNumber)
                .Ignore(i => i.PhoneNumberConfirmed)
                .Ignore(i => i.TwoFactorEnabled)
                .Ignore(i => i.LockoutEnabled)
                //.Ignore(i => i.ConcurrencyStamp)
                .Ignore(i => i.AccessFailedCount)
                .Ignore(i => i.LockoutEnd);            
        }       
    }
}
