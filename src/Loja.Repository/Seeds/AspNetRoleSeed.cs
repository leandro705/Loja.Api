using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Loja.Repository.Seeds
{
    public static class AspNetRoleSeed
    {
        public static void InsertAspNetRoles(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(

                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { 1, "Administrador", "Administrador", Guid.NewGuid() }
            );

            migrationBuilder.InsertData(

               schema: "dbo",
               table: "AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { 2, "Gerente", "Gerente", Guid.NewGuid() }
            );

            migrationBuilder.InsertData(

               schema: "dbo",
               table: "AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { 3, "Cliente", "Cliente", Guid.NewGuid() }
            );

            migrationBuilder.InsertData(

               schema: "dbo",
               table: "AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { 4, "Funcionario", "Funcionario", Guid.NewGuid() }
            );
        
        }
    }
}
