using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddTBsDoControleDeContratos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licencas_Clientes_ClienteId",
                table: "Licencas");

            migrationBuilder.DropForeignKey(
                name: "FK_Licencas_Planos_Licenca_PlanoLicencaId",
                table: "Licencas");

            migrationBuilder.DropForeignKey(
                name: "FK_Licencas_Softwares_SoftwareId",
                table: "Licencas");

            migrationBuilder.DropTable(
                name: "Planos_Modulos");

            migrationBuilder.DropIndex(
                name: "IX_Licencas_ClienteId",
                table: "Licencas");

            migrationBuilder.DropIndex(
                name: "IX_Licencas_SoftwareId",
                table: "Licencas");

            migrationBuilder.DropColumn(
                name: "DuracaoMeses",
                table: "Planos_Licenca");

            migrationBuilder.DropColumn(
                name: "QuantidadeUsuarios",
                table: "Planos_Licenca");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Licencas");

            migrationBuilder.DropColumn(
                name: "DataExpiracao",
                table: "Licencas");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "Licencas");

            migrationBuilder.RenameColumn(
                name: "Situacao",
                table: "Planos_Licenca",
                newName: "QuantidadeDeAcoes");

            migrationBuilder.RenameColumn(
                name: "SoftwareId",
                table: "Licencas",
                newName: "QuantidadeAcoesDisponivel");

            migrationBuilder.RenameColumn(
                name: "PlanoLicencaId",
                table: "Licencas",
                newName: "ContratoId");

            migrationBuilder.RenameIndex(
                name: "IX_Licencas_PlanoLicencaId",
                table: "Licencas",
                newName: "IX_Licencas_ContratoId");

            migrationBuilder.AddColumn<int>(
                name: "Identificacao",
                table: "Modulos_Software",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Modulos_Software",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ClientesModelId",
                table: "Licencas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo_PlanoId = table.Column<int>(type: "integer", nullable: false),
                    ClienteFinalId = table.Column<int>(type: "integer", nullable: false),
                    LicencaId = table.Column<int>(type: "integer", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClientesModelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contratos_Clientes_ClienteFinalId",
                        column: x => x.ClienteFinalId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contratos_Clientes_ClientesModelId",
                        column: x => x.ClientesModelId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contratos_Licencas_LicencaId",
                        column: x => x.LicencaId,
                        principalTable: "Licencas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contratos_Planos_Licenca_Tipo_PlanoId",
                        column: x => x.Tipo_PlanoId,
                        principalTable: "Planos_Licenca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Numeros_Contrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContratoId = table.Column<int>(type: "integer", nullable: false),
                    Numero = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NomeInstancia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TokenInstancia = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numeros_Contrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Numeros_Contrato_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modulos_Por_Numero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumeroId = table.Column<int>(type: "integer", nullable: false),
                    ModuloId = table.Column<int>(type: "integer", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos_Por_Numero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modulos_Por_Numero_Modulos_Software_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos_Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modulos_Por_Numero_Numeros_Contrato_NumeroId",
                        column: x => x.NumeroId,
                        principalTable: "Numeros_Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Licencas_ClientesModelId",
                table: "Licencas",
                column: "ClientesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_ClienteFinalId",
                table: "Contratos",
                column: "ClienteFinalId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_ClientesModelId",
                table: "Contratos",
                column: "ClientesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_LicencaId",
                table: "Contratos",
                column: "LicencaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_Tipo_PlanoId",
                table: "Contratos",
                column: "Tipo_PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_Por_Numero_ModuloId",
                table: "Modulos_Por_Numero",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_Por_Numero_NumeroId",
                table: "Modulos_Por_Numero",
                column: "NumeroId");

            migrationBuilder.CreateIndex(
                name: "IX_Numeros_Contrato_ContratoId",
                table: "Numeros_Contrato",
                column: "ContratoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Licencas_Clientes_ClientesModelId",
                table: "Licencas",
                column: "ClientesModelId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Licencas_Contratos_ContratoId",
                table: "Licencas",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licencas_Clientes_ClientesModelId",
                table: "Licencas");

            migrationBuilder.DropForeignKey(
                name: "FK_Licencas_Contratos_ContratoId",
                table: "Licencas");

            migrationBuilder.DropTable(
                name: "Modulos_Por_Numero");

            migrationBuilder.DropTable(
                name: "Numeros_Contrato");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropIndex(
                name: "IX_Licencas_ClientesModelId",
                table: "Licencas");

            migrationBuilder.DropColumn(
                name: "Identificacao",
                table: "Modulos_Software");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Modulos_Software");

            migrationBuilder.DropColumn(
                name: "ClientesModelId",
                table: "Licencas");

            migrationBuilder.RenameColumn(
                name: "QuantidadeDeAcoes",
                table: "Planos_Licenca",
                newName: "Situacao");

            migrationBuilder.RenameColumn(
                name: "QuantidadeAcoesDisponivel",
                table: "Licencas",
                newName: "SoftwareId");

            migrationBuilder.RenameColumn(
                name: "ContratoId",
                table: "Licencas",
                newName: "PlanoLicencaId");

            migrationBuilder.RenameIndex(
                name: "IX_Licencas_ContratoId",
                table: "Licencas",
                newName: "IX_Licencas_PlanoLicencaId");

            migrationBuilder.AddColumn<int>(
                name: "DuracaoMeses",
                table: "Planos_Licenca",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeUsuarios",
                table: "Planos_Licenca",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Licencas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExpiracao",
                table: "Licencas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "Licencas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Planos_Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuloId = table.Column<int>(type: "integer", nullable: false),
                    PlanoLicencaId = table.Column<int>(type: "integer", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "IX_Licencas_ClienteId",
                table: "Licencas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Licencas_SoftwareId",
                table: "Licencas",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Planos_Modulos_ModuloId",
                table: "Planos_Modulos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Planos_Modulos_PlanoLicencaId",
                table: "Planos_Modulos",
                column: "PlanoLicencaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Licencas_Clientes_ClienteId",
                table: "Licencas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Licencas_Planos_Licenca_PlanoLicencaId",
                table: "Licencas",
                column: "PlanoLicencaId",
                principalTable: "Planos_Licenca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Licencas_Softwares_SoftwareId",
                table: "Licencas",
                column: "SoftwareId",
                principalTable: "Softwares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
