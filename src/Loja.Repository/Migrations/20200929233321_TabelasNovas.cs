using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja.Repository.Migrations
{
    public partial class TabelasNovas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estabelecimento",
                columns: table => new
                {
                    EstabelecimentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: false),
                    Contato = table.Column<string>(type: "varchar(15)", nullable: false),
                    Instagran = table.Column<string>(type: "varchar(150)", nullable: true),
                    Facebook = table.Column<string>(type: "varchar(150)", nullable: true),
                    Situacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estabelecimento", x => x.EstabelecimentoId);
                });

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    ServicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.ServicoId);
                });

            migrationBuilder.CreateTable(
                name: "Atendimento",
                columns: table => new
                {
                    AtendimentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataAtendimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(500)", nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    EstabelecimentoId = table.Column<int>(nullable: false),
                    Situacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimento", x => x.AtendimentoId);
                    table.ForeignKey(
                        name: "FK_Atendimento_Estabelecimento_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "Estabelecimento",
                        principalColumn: "EstabelecimentoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atendimento_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserEstabelecimento",
                columns: table => new
                {
                    EstabelecimentoId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEstabelecimento", x => new { x.UserId, x.EstabelecimentoId });
                    table.ForeignKey(
                        name: "FK_UserEstabelecimento_Estabelecimento_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "Estabelecimento",
                        principalColumn: "EstabelecimentoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEstabelecimento_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    AgendamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataAgendamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinalAgendamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(500)", nullable: true),
                    ServicoId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    EstabelecimentoId = table.Column<int>(nullable: false),
                    Situacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.AgendamentoId);
                    table.ForeignKey(
                        name: "FK_Agendamento_Estabelecimento_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "Estabelecimento",
                        principalColumn: "EstabelecimentoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamento_Servico_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servico",
                        principalColumn: "ServicoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamento_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtendimentoItem",
                columns: table => new
                {
                    AtendimentoItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ServicoId = table.Column<int>(nullable: false),
                    Situacao = table.Column<int>(nullable: false),
                    AtendimentoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtendimentoItem", x => x.AtendimentoItemId);
                    table.ForeignKey(
                        name: "FK_AtendimentoItem_Atendimento_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimento",
                        principalColumn: "AtendimentoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AtendimentoItem_Servico_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servico",
                        principalColumn: "ServicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_EstabelecimentoId",
                table: "Agendamento",
                column: "EstabelecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_ServicoId",
                table: "Agendamento",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_UserId",
                table: "Agendamento",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_EstabelecimentoId",
                table: "Atendimento",
                column: "EstabelecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_UserId",
                table: "Atendimento",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AtendimentoItem_AtendimentoId",
                table: "AtendimentoItem",
                column: "AtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AtendimentoItem_ServicoId",
                table: "AtendimentoItem",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEstabelecimento_EstabelecimentoId",
                table: "UserEstabelecimento",
                column: "EstabelecimentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.DropTable(
                name: "AtendimentoItem");

            migrationBuilder.DropTable(
                name: "UserEstabelecimento");

            migrationBuilder.DropTable(
                name: "Atendimento");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "Estabelecimento");
        }
    }
}
