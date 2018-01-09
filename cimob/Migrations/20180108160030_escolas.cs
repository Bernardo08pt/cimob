using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace cimob.Migrations
{
    public partial class escolas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curso_Pais_PaisID",
                table: "Curso");

            migrationBuilder.DropForeignKey(
                name: "FK_Escolas_Pais_PaisID",
                table: "Escolas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pais",
                table: "Pais");

            migrationBuilder.RenameTable(
                name: "Pais",
                newName: "Paises");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Curso",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paises",
                table: "Paises",
                column: "PaisID");

            migrationBuilder.CreateTable(
                name: "IpsEscolas",
                columns: table => new
                {
                    IpsEscolaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpsEscolas", x => x.IpsEscolaID);
                });

            migrationBuilder.CreateTable(
                name: "IpsCursos",
                columns: table => new
                {
                    IpsCursoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IpsEscolaID = table.Column<int>(nullable: true),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpsCursos", x => x.IpsCursoID);
                    table.ForeignKey(
                        name: "FK_IpsCursos_IpsEscolas_IpsEscolaID",
                        column: x => x.IpsEscolaID,
                        principalTable: "IpsEscolas",
                        principalColumn: "IpsEscolaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IpsCursos_IpsEscolaID",
                table: "IpsCursos",
                column: "IpsEscolaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_Paises_PaisID",
                table: "Curso",
                column: "PaisID",
                principalTable: "Paises",
                principalColumn: "PaisID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Escolas_Paises_PaisID",
                table: "Escolas",
                column: "PaisID",
                principalTable: "Paises",
                principalColumn: "PaisID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curso_Paises_PaisID",
                table: "Curso");

            migrationBuilder.DropForeignKey(
                name: "FK_Escolas_Paises_PaisID",
                table: "Escolas");

            migrationBuilder.DropTable(
                name: "IpsCursos");

            migrationBuilder.DropTable(
                name: "IpsEscolas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paises",
                table: "Paises");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Curso");

            migrationBuilder.RenameTable(
                name: "Paises",
                newName: "Pais");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pais",
                table: "Pais",
                column: "PaisID");

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_Pais_PaisID",
                table: "Curso",
                column: "PaisID",
                principalTable: "Pais",
                principalColumn: "PaisID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Escolas_Pais_PaisID",
                table: "Escolas",
                column: "PaisID",
                principalTable: "Pais",
                principalColumn: "PaisID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
