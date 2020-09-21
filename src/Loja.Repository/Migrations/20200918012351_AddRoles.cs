using Loja.Repository.Seeds;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja.Repository.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AspNetRoleSeed.InsertAspNetRoles(migrationBuilder);
            AspNetUserSeed.InsertAspNetUser(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
