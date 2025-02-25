using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddClassPLanosMOdEModulosSOft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modulos_Software",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos_Software", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modulos_Software_Softwares_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Softwares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planos_Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanoLicencaId = table.Column<int>(type: "int", nullable: false),
                    ModuloId = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos_Modulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planos_Modulos_Modulos_Software_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos_Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Planos_Modulos_Planos_Licenca_PlanoLicencaId",
                        column: x => x.PlanoLicencaId,
                        principalTable: "Planos_Licenca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_Software_SoftwareId",
                table: "Modulos_Software",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Planos_Modulos_ModuloId",
                table: "Planos_Modulos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Planos_Modulos_PlanoLicencaId",
                table: "Planos_Modulos",
                column: "PlanoLicencaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Planos_Modulos");

            migrationBuilder.DropTable(
                name: "Modulos_Software");
        }
    }
}
