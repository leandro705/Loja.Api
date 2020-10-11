using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja.Repository.Migrations
{
    public partial class AjusteAtendimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Users",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Users",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgendamentoId",
                table: "Atendimento",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Atendimento",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_AgendamentoId",
                table: "Atendimento",
                column: "AgendamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_Agendamento_AgendamentoId",
                table: "Atendimento",
                column: "AgendamentoId",
                principalTable: "Agendamento",
                principalColumn: "AgendamentoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_Agendamento_AgendamentoId",
                table: "Atendimento");

            migrationBuilder.DropIndex(
                name: "IX_Atendimento_AgendamentoId",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "AgendamentoId",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Atendimento");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Users",
                type: "varchar(12)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Users",
                type: "varchar(12)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);
        }
    }
}
