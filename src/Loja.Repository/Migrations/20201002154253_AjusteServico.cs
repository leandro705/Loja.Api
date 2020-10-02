using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja.Repository.Migrations
{
    public partial class AjusteServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Servico",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SituacaoId",
                table: "Servico",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Servico_SituacaoId",
                table: "Servico",
                column: "SituacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servico_Situacao_SituacaoId",
                table: "Servico",
                column: "SituacaoId",
                principalTable: "Situacao",
                principalColumn: "SituacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servico_Situacao_SituacaoId",
                table: "Servico");

            migrationBuilder.DropIndex(
                name: "IX_Servico_SituacaoId",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "SituacaoId",
                table: "Servico");
        }
    }
}
