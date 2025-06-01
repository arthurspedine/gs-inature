using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iNature.Migrations
{
    /// <inheritdoc />
    public partial class ReportEReportConfirmacoesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INATURE_REPORTS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Titulo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Corpo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Logradouro = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Numero = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INATURE_REPORTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INATURE_REPORTS_INATURE_USUARIOS_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "INATURE_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INATURE_REPORT_CONFIRMACOES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ReportId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataConfirmacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INATURE_REPORT_CONFIRMACOES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INATURE_REPORT_CONFIRMACOES_INATURE_REPORTS_ReportId",
                        column: x => x.ReportId,
                        principalTable: "INATURE_REPORTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INATURE_REPORT_CONFIRMACOES_INATURE_USUARIOS_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "INATURE_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_INATURE_REPORT_CONFIRMACOES_ReportId",
                table: "INATURE_REPORT_CONFIRMACOES",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_INATURE_REPORT_CONFIRMACOES_UsuarioId",
                table: "INATURE_REPORT_CONFIRMACOES",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_INATURE_REPORTS_UsuarioId",
                table: "INATURE_REPORTS",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INATURE_REPORT_CONFIRMACOES");

            migrationBuilder.DropTable(
                name: "INATURE_REPORTS");
        }
    }
}
