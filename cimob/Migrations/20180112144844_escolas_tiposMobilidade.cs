using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace cimob.Migrations
{
    public partial class escolas_tiposMobilidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Paises",
                columns: table => new
                {
                    PaisID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.PaisID);
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
                name: "TiposMobilidade",
                columns: table => new
                {
                    TipoMobilidadeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Estagio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposMobilidade", x => x.TipoMobilidadeID);
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

            migrationBuilder.CreateTable(
                name: "Escolas",
                columns: table => new
                {
                    EscolaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    PaisID = table.Column<int>(nullable: false),
                    TipoMobilidadeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolas", x => x.EscolaID);
                    table.ForeignKey(
                        name: "FK_Escolas_Paises_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Paises",
                        principalColumn: "PaisID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Escolas_TiposMobilidade_TipoMobilidadeID",
                        column: x => x.TipoMobilidadeID,
                        principalTable: "TiposMobilidade",
                        principalColumn: "TipoMobilidadeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CursoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EscolaID = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    PaisID = table.Column<int>(nullable: false),
                    Vagas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CursoID);
                    table.ForeignKey(
                        name: "FK_Cursos_Escolas_EscolaID",
                        column: x => x.EscolaID,
                        principalTable: "Escolas",
                        principalColumn: "EscolaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cursos_Paises_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Paises",
                        principalColumn: "PaisID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_EscolaID",
                table: "Cursos",
                column: "EscolaID");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_PaisID",
                table: "Cursos",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Escolas_PaisID",
                table: "Escolas",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Escolas_TipoMobilidadeID",
                table: "Escolas",
                column: "TipoMobilidadeID");

            migrationBuilder.CreateIndex(
                name: "IX_IpsCursos_IpsEscolaID",
                table: "IpsCursos",
                column: "IpsEscolaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "IpsCursos");

            migrationBuilder.DropTable(
                name: "Parentescos");

            migrationBuilder.DropTable(
                name: "Escolas");

            migrationBuilder.DropTable(
                name: "IpsEscolas");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "TiposMobilidade");
        }
    }
}
