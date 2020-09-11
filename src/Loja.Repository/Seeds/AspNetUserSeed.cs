using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loja.Repository.Seeds
{
    public static class AspNetUserSeed
    {
        public static void InsertAspNetUser(MigrationBuilder migrationBuilder)
        {
            var aspNetUserId = Guid.NewGuid();

            migrationBuilder.InsertData(

                schema: "dbo",
                table: "Users",
                columns: new[] { "Id", "UserName", "Email", "Nome", "PasswordHash", "ConcurrencyStamp", "NormalizedUserName", "SecurityStamp", "DataCadastro" },
                values: new object[] { aspNetUserId, "admin@admin.com.br", "admin@admin.com.br", "Administrador", "AQAAAAEAACcQAAAAEFNeaLlRCOb0If5kkvu5GiSJX1w9+7YPtFU+z42pcbLaFt/whCzaK0pVrcLok8IXrQ==", Guid.NewGuid(), "admin@admin.com.br", Guid.NewGuid(), DateTime.Now.ToString("yyyy-MM-dd HH:mm") }
            );

            migrationBuilder.InsertData(

               schema: "dbo",
               table: "AspNetUserRoles",
               columns: new[] { "UserId", "RoleId" },
               values: new object[] { aspNetUserId, 1 }
           );

        }
    }
}
