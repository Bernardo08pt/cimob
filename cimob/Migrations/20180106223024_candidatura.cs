using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace cimob.Migrations
{
    public partial class Candidatura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    PaisID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.PaisID);
                });

            migrationBuilder.CreateTable(
                name: "TipoMobilidade",
                columns: table => new
                {
                    TipoMobilidadeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Estagio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMobilidade", x => x.TipoMobilidadeID);
                });

            migrationBuilder.CreateTable(
                name: "Escolas",
                columns: table => new
                {
                    EscolaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    PaisID = table.Column<int>(nullable: true),
                    TipoMobilidadeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolas", x => x.EscolaID);
                    table.ForeignKey(
                        name: "FK_Escolas_Pais_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Pais",
                        principalColumn: "PaisID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Escolas_TipoMobilidade_TipoMobilidadeID",
                        column: x => x.TipoMobilidadeID,
                        principalTable: "TipoMobilidade",
                        principalColumn: "TipoMobilidadeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    CursoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EscolaID1 = table.Column<int>(nullable: true),
                    PaisID = table.Column<int>(nullable: true),
                    Vagas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.CursoID);
                    table.ForeignKey(
                        name: "FK_Curso_Escolas_EscolaID1",
                        column: x => x.EscolaID1,
                        principalTable: "Escolas",
                        principalColumn: "EscolaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Curso_Pais_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Pais",
                        principalColumn: "PaisID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curso_EscolaID1",
                table: "Curso",
                column: "EscolaID1");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_PaisID",
                table: "Curso",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Escolas_PaisID",
                table: "Escolas",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Escolas_TipoMobilidadeID",
                table: "Escolas",
                column: "TipoMobilidadeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Escolas");

            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropTable(
                name: "TipoMobilidade");
        }
    }
}
