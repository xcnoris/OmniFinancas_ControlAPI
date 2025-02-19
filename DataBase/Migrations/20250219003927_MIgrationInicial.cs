using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class MIgrationInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoEntidade = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentificadoresEntidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntidadeId = table.Column<int>(type: "int", nullable: false),
                    CNPJ_CPF = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificadoresEntidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentificadoresEntidade_Entidades_EntidadeId",
                        column: x => x.EntidadeId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Revendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntidadeId = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revendas_Entidades_EntidadeId",
                        column: x => x.EntidadeId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntidadeId = table.Column<int>(type: "int", nullable: false),
                    RevendaId = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Entidades_EntidadeId",
                        column: x => x.EntidadeId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clientes_Revendas_RevendaId",
                        column: x => x.RevendaId,
                        principalTable: "Revendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanosLicencaModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevendaModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosLicencaModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanosLicencaModel_Revendas_RevendaModelId",
                        column: x => x.RevendaModelId,
                        principalTable: "Revendas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SoftwaresModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevendaModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwaresModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoftwaresModel_Revendas_RevendaModelId",
                        column: x => x.RevendaModelId,
                        principalTable: "Revendas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRevendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevendaId = table.Column<int>(type: "int", nullable: false),
                    EntidadeId = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRevendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosRevendas_Entidades_EntidadeId",
                        column: x => x.EntidadeId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuariosRevendas_Revendas_RevendaId",
                        column: x => x.RevendaId,
                        principalTable: "Revendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EntidadeId",
                table: "Clientes",
                column: "EntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_RevendaId",
                table: "Clientes",
                column: "RevendaId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificadoresEntidade_EntidadeId",
                table: "IdentificadoresEntidade",
                column: "EntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosLicencaModel_RevendaModelId",
                table: "PlanosLicencaModel",
                column: "RevendaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Revendas_EntidadeId",
                table: "Revendas",
                column: "EntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwaresModel_RevendaModelId",
                table: "SoftwaresModel",
                column: "RevendaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRevendas_EntidadeId",
                table: "UsuariosRevendas",
                column: "EntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRevendas_RevendaId",
                table: "UsuariosRevendas",
                column: "RevendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "IdentificadoresEntidade");

            migrationBuilder.DropTable(
                name: "PlanosLicencaModel");

            migrationBuilder.DropTable(
                name: "SoftwaresModel");

            migrationBuilder.DropTable(
                name: "UsuariosRevendas");

            migrationBuilder.DropTable(
                name: "Revendas");

            migrationBuilder.DropTable(
                name: "Entidades");
        }
    }
}
