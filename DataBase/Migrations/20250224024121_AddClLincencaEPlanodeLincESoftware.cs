using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddClLincencaEPlanodeLincESoftware : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentificadoresEntidade_Entidades_EntidadeId",
                table: "IdentificadoresEntidade");

            migrationBuilder.DropForeignKey(
                name: "FK_SoftwaresModel_Revendas_RevendaModelId",
                table: "SoftwaresModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosRevendas_Entidades_EntidadeId",
                table: "UsuariosRevendas");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosRevendas_Revendas_RevendaId",
                table: "UsuariosRevendas");

            migrationBuilder.DropTable(
                name: "PlanosLicencaModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosRevendas",
                table: "UsuariosRevendas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoftwaresModel",
                table: "SoftwaresModel");

            migrationBuilder.DropIndex(
                name: "IX_SoftwaresModel_RevendaModelId",
                table: "SoftwaresModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentificadoresEntidade",
                table: "IdentificadoresEntidade");

            migrationBuilder.DropColumn(
                name: "RevendaModelId",
                table: "SoftwaresModel");

            migrationBuilder.RenameTable(
                name: "UsuariosRevendas",
                newName: "Usuarios_Revendas");

            migrationBuilder.RenameTable(
                name: "SoftwaresModel",
                newName: "Softwares");

            migrationBuilder.RenameTable(
                name: "IdentificadoresEntidade",
                newName: "Identificadores_Entidade");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosRevendas_RevendaId",
                table: "Usuarios_Revendas",
                newName: "IX_Usuarios_Revendas_RevendaId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosRevendas_EntidadeId",
                table: "Usuarios_Revendas",
                newName: "IX_Usuarios_Revendas_EntidadeId");

            migrationBuilder.RenameIndex(
                name: "IX_IdentificadoresEntidade_EntidadeId",
                table: "Identificadores_Entidade",
                newName: "IX_Identificadores_Entidade_EntidadeId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Softwares",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Softwares",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Descrição",
                table: "Softwares",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Softwares",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProprietarioId",
                table: "Softwares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "Softwares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Versao",
                table: "Softwares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios_Revendas",
                table: "Usuarios_Revendas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Softwares",
                table: "Softwares",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Identificadores_Entidade",
                table: "Identificadores_Entidade",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Planos_Licenca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DuracaoMeses = table.Column<int>(type: "int", nullable: false),
                    QuantidadeUsuarios = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevendaModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos_Licenca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planos_Licenca_Revendas_RevendaModelId",
                        column: x => x.RevendaModelId,
                        principalTable: "Revendas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Licencas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    PlanoLicencaId = table.Column<int>(type: "int", nullable: false),
                    ChaveAtivacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licencas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licencas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Licencas_Planos_Licenca_PlanoLicencaId",
                        column: x => x.PlanoLicencaId,
                        principalTable: "Planos_Licenca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Licencas_Softwares_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Softwares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_ProprietarioId",
                table: "Softwares",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Licencas_ClienteId",
                table: "Licencas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Licencas_PlanoLicencaId",
                table: "Licencas",
                column: "PlanoLicencaId");

            migrationBuilder.CreateIndex(
                name: "IX_Licencas_SoftwareId",
                table: "Licencas",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Planos_Licenca_RevendaModelId",
                table: "Planos_Licenca",
                column: "RevendaModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Identificadores_Entidade_Entidades_EntidadeId",
                table: "Identificadores_Entidade",
                column: "EntidadeId",
                principalTable: "Entidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_Revendas_ProprietarioId",
                table: "Softwares",
                column: "ProprietarioId",
                principalTable: "Revendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Revendas_Entidades_EntidadeId",
                table: "Usuarios_Revendas",
                column: "EntidadeId",
                principalTable: "Entidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Revendas_Revendas_RevendaId",
                table: "Usuarios_Revendas",
                column: "RevendaId",
                principalTable: "Revendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Identificadores_Entidade_Entidades_EntidadeId",
                table: "Identificadores_Entidade");

            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Revendas_ProprietarioId",
                table: "Softwares");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Revendas_Entidades_EntidadeId",
                table: "Usuarios_Revendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Revendas_Revendas_RevendaId",
                table: "Usuarios_Revendas");

            migrationBuilder.DropTable(
                name: "Licencas");

            migrationBuilder.DropTable(
                name: "Planos_Licenca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios_Revendas",
                table: "Usuarios_Revendas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Softwares",
                table: "Softwares");

            migrationBuilder.DropIndex(
                name: "IX_Softwares_ProprietarioId",
                table: "Softwares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Identificadores_Entidade",
                table: "Identificadores_Entidade");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "Descrição",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "ProprietarioId",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "Versao",
                table: "Softwares");

            migrationBuilder.RenameTable(
                name: "Usuarios_Revendas",
                newName: "UsuariosRevendas");

            migrationBuilder.RenameTable(
                name: "Softwares",
                newName: "SoftwaresModel");

            migrationBuilder.RenameTable(
                name: "Identificadores_Entidade",
                newName: "IdentificadoresEntidade");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Revendas_RevendaId",
                table: "UsuariosRevendas",
                newName: "IX_UsuariosRevendas_RevendaId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Revendas_EntidadeId",
                table: "UsuariosRevendas",
                newName: "IX_UsuariosRevendas_EntidadeId");

            migrationBuilder.RenameIndex(
                name: "IX_Identificadores_Entidade_EntidadeId",
                table: "IdentificadoresEntidade",
                newName: "IX_IdentificadoresEntidade_EntidadeId");

            migrationBuilder.AddColumn<int>(
                name: "RevendaModelId",
                table: "SoftwaresModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosRevendas",
                table: "UsuariosRevendas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoftwaresModel",
                table: "SoftwaresModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentificadoresEntidade",
                table: "IdentificadoresEntidade",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_SoftwaresModel_RevendaModelId",
                table: "SoftwaresModel",
                column: "RevendaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosLicencaModel_RevendaModelId",
                table: "PlanosLicencaModel",
                column: "RevendaModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentificadoresEntidade_Entidades_EntidadeId",
                table: "IdentificadoresEntidade",
                column: "EntidadeId",
                principalTable: "Entidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoftwaresModel_Revendas_RevendaModelId",
                table: "SoftwaresModel",
                column: "RevendaModelId",
                principalTable: "Revendas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosRevendas_Entidades_EntidadeId",
                table: "UsuariosRevendas",
                column: "EntidadeId",
                principalTable: "Entidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosRevendas_Revendas_RevendaId",
                table: "UsuariosRevendas",
                column: "RevendaId",
                principalTable: "Revendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
