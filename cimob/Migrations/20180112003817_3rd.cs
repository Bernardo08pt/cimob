using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace cimob.Migrations
{
    public partial class _3rd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curso_Escolas_EscolaID1",
                table: "Curso");

            migrationBuilder.DropForeignKey(
                name: "FK_Curso_Pais_PaisID",
                table: "Curso");

            migrationBuilder.DropForeignKey(
                name: "FK_Escolas_Pais_PaisID",
                table: "Escolas");

            migrationBuilder.DropForeignKey(
                name: "FK_Escolas_TipoMobilidade_TipoMobilidadeID",
                table: "Escolas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoMobilidade",
                table: "TipoMobilidade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pais",
                table: "Pais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Curso",
                table: "Curso");

            migrationBuilder.RenameTable(
                name: "TipoMobilidade",
                newName: "TiposMobilidade");

            migrationBuilder.RenameTable(
                name: "Pais",
                newName: "Paises");

            migrationBuilder.RenameTable(
                name: "Curso",
                newName: "Cursos");

            migrationBuilder.RenameColumn(
                name: "EscolaID1",
                table: "Cursos",
                newName: "EscolaID");

            migrationBuilder.RenameIndex(
                name: "IX_Curso_PaisID",
                table: "Cursos",
                newName: "IX_Cursos_PaisID");

            migrationBuilder.RenameIndex(
                name: "IX_Curso_EscolaID1",
                table: "Cursos",
                newName: "IX_Cursos_EscolaID");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Cursos",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposMobilidade",
                table: "TiposMobilidade",
                column: "TipoMobilidadeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paises",
                table: "Paises",
                column: "PaisID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cursos",
                table: "Cursos",
                column: "CursoID");

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
                name: "Parentescos",
                columns: table => new
                {
                    ParentescoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parentescos", x => x.ParentescoID);
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
                name: "FK_Cursos_Escolas_EscolaID",
                table: "Cursos",
                column: "EscolaID",
                principalTable: "Escolas",
                principalColumn: "EscolaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Paises_PaisID",
                table: "Cursos",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Escolas_TiposMobilidade_TipoMobilidadeID",
                table: "Escolas",
                column: "TipoMobilidadeID",
                principalTable: "TiposMobilidade",
                principalColumn: "TipoMobilidadeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Escolas_EscolaID",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Paises_PaisID",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Escolas_Paises_PaisID",
                table: "Escolas");

            migrationBuilder.DropForeignKey(
                name: "FK_Escolas_TiposMobilidade_TipoMobilidadeID",
                table: "Escolas");

            migrationBuilder.DropTable(
                name: "IpsCursos");

            migrationBuilder.DropTable(
                name: "Parentescos");

            migrationBuilder.DropTable(
                name: "IpsEscolas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposMobilidade",
                table: "TiposMobilidade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paises",
                table: "Paises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cursos",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Cursos");

            migrationBuilder.RenameTable(
                name: "TiposMobilidade",
                newName: "TipoMobilidade");

            migrationBuilder.RenameTable(
                name: "Paises",
                newName: "Pais");

            migrationBuilder.RenameTable(
                name: "Cursos",
                newName: "Curso");

            migrationBuilder.RenameColumn(
                name: "EscolaID",
                table: "Curso",
                newName: "EscolaID1");

            migrationBuilder.RenameIndex(
                name: "IX_Cursos_PaisID",
                table: "Curso",
                newName: "IX_Curso_PaisID");

            migrationBuilder.RenameIndex(
                name: "IX_Cursos_EscolaID",
                table: "Curso",
                newName: "IX_Curso_EscolaID1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoMobilidade",
                table: "TipoMobilidade",
                column: "TipoMobilidadeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pais",
                table: "Pais",
                column: "PaisID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Curso",
                table: "Curso",
                column: "CursoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_Escolas_EscolaID1",
                table: "Curso",
                column: "EscolaID1",
                principalTable: "Escolas",
                principalColumn: "EscolaID",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Escolas_TipoMobilidade_TipoMobilidadeID",
                table: "Escolas",
                column: "TipoMobilidadeID",
                principalTable: "TipoMobilidade",
                principalColumn: "TipoMobilidadeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
