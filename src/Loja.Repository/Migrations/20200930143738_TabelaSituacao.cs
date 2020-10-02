using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja.Repository.Migrations
{
    public partial class TabelaSituacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Estabelecimento");

            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "AtendimentoItem");

            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Agendamento");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Estabelecimento",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SituacaoId",
                table: "Estabelecimento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SituacaoId",
                table: "Atendimento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Agendamento",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SituacaoId",
                table: "Agendamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Situacao",
                columns: table => new
                {
                    SituacaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Situacao", x => x.SituacaoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estabelecimento_SituacaoId",
                table: "Estabelecimento",
                column: "SituacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_SituacaoId",
                table: "Atendimento",
                column: "SituacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_SituacaoId",
                table: "Agendamento",
                column: "SituacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Situacao_SituacaoId",
                table: "Agendamento",
                column: "SituacaoId",
                principalTable: "Situacao",
                principalColumn: "SituacaoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_Situacao_SituacaoId",
                table: "Atendimento",
                column: "SituacaoId",
                principalTable: "Situacao",
                principalColumn: "SituacaoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estabelecimento_Situacao_SituacaoId",
                table: "Estabelecimento",
                column: "SituacaoId",
                principalTable: "Situacao",
                principalColumn: "SituacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Situacao_SituacaoId",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_Situacao_SituacaoId",
                table: "Atendimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Estabelecimento_Situacao_SituacaoId",
                table: "Estabelecimento");

            migrationBuilder.DropTable(
                name: "Situacao");

            migrationBuilder.DropIndex(
                name: "IX_Estabelecimento_SituacaoId",
                table: "Estabelecimento");

            migrationBuilder.DropIndex(
                name: "IX_Atendimento_SituacaoId",
                table: "Atendimento");

            migrationBuilder.DropIndex(
                name: "IX_Agendamento_SituacaoId",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Estabelecimento");

            migrationBuilder.DropColumn(
                name: "SituacaoId",
                table: "Estabelecimento");

            migrationBuilder.DropColumn(
                name: "SituacaoId",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "SituacaoId",
                table: "Agendamento");

            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "Estabelecimento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "AtendimentoItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "Atendimento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "Agendamento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
