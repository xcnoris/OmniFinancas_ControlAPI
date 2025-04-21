using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class ajustespequenos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratos_Licencas_LicencaId",
                table: "Contratos");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_LicencaId",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "LicencaId",
                table: "Contratos");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Clientes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "LicencaId",
                table: "Contratos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_LicencaId",
                table: "Contratos",
                column: "LicencaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratos_Licencas_LicencaId",
                table: "Contratos",
                column: "LicencaId",
                principalTable: "Licencas",
                principalColumn: "Id");
        }
    }
}
