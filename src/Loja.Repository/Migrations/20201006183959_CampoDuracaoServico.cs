using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja.Repository.Migrations
{
    public partial class CampoDuracaoServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duracao",
                table: "Servico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstabelecimentoId",
                table: "Servico",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Servico_EstabelecimentoId",
                table: "Servico",
                column: "EstabelecimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servico_Estabelecimento_EstabelecimentoId",
                table: "Servico",
                column: "EstabelecimentoId",
                principalTable: "Estabelecimento",
                principalColumn: "EstabelecimentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servico_Estabelecimento_EstabelecimentoId",
                table: "Servico");

            migrationBuilder.DropIndex(
                name: "IX_Servico_EstabelecimentoId",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "EstabelecimentoId",
                table: "Servico");
        }
    }
}
