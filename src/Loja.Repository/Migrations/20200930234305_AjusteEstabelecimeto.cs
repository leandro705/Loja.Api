using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja.Repository.Migrations
{
    public partial class AjusteEstabelecimeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Instagran",
                table: "Estabelecimento",
                newName: "Instagram");

            migrationBuilder.RenameColumn(
                name: "Contato",
                table: "Estabelecimento",
                newName: "Celular");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Estabelecimento",
                type: "varchar(15)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Estabelecimento");

            migrationBuilder.RenameColumn(
                name: "Instagram",
                table: "Estabelecimento",
                newName: "Instagran");

            migrationBuilder.RenameColumn(
                name: "Celular",
                table: "Estabelecimento",
                newName: "Contato");
        }
    }
}
