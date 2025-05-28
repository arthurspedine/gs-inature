using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iNature.Migrations
{
    /// <inheritdoc />
    public partial class NoticiaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INATURE_NOTICIAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Titulo = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    DataPublicacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Resumo = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    Corpo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INATURE_NOTICIAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INATURE_NOTICIAS_INATURE_USUARIOS_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "INATURE_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_INATURE_NOTICIAS_UsuarioId",
                table: "INATURE_NOTICIAS",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INATURE_NOTICIAS");
        }
    }
}
