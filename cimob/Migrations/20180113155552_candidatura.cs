using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace cimob.Migrations
{
    public partial class candidatura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    DocumentoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FicheiroCaminho = table.Column<string>(nullable: true),
                    FicheiroNome = table.Column<string>(nullable: true),
                    OrigemCimob = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.DocumentoID);
                });

            migrationBuilder.CreateTable(
                name: "EstadosCandidatura",
                columns: table => new
                {
                    EstadoCandidaturaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosCandidatura", x => x.EstadoCandidaturaID);
                });

            migrationBuilder.CreateTable(
                name: "Candidaturas",
                columns: table => new
                {
                    CandidaturaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnoLetivo = table.Column<int>(nullable: false),
                    ContactoPessoal = table.Column<int>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    EmailAlternativo = table.Column<string>(nullable: true),
                    EmegerenciaParentescoID = table.Column<int>(nullable: false),
                    EmergenciaContacto = table.Column<int>(nullable: false),
                    Entrevista = table.Column<string>(nullable: true),
                    EstadoCandidaturaID = table.Column<int>(nullable: false),
                    Estagio = table.Column<int>(nullable: false),
                    IpsCursoID = table.Column<int>(nullable: false),
                    Observacoes = table.Column<string>(nullable: true),
                    Pontuacao = table.Column<int>(nullable: false),
                    RejeicaoRazao = table.Column<string>(nullable: true),
                    Rejeitada = table.Column<int>(nullable: false),
                    Semestre = table.Column<int>(nullable: false),
                    TipoMobilidadeID = table.Column<int>(nullable: false),
                    UtilizadorID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidaturas", x => x.CandidaturaID);
                    table.ForeignKey(
                        name: "FK_Candidaturas_Parentescos_EmegerenciaParentescoID",
                        column: x => x.EmegerenciaParentescoID,
                        principalTable: "Parentescos",
                        principalColumn: "ParentescoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidaturas_EstadosCandidatura_EstadoCandidaturaID",
                        column: x => x.EstadoCandidaturaID,
                        principalTable: "EstadosCandidatura",
                        principalColumn: "EstadoCandidaturaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidaturas_IpsCursos_IpsCursoID",
                        column: x => x.IpsCursoID,
                        principalTable: "IpsCursos",
                        principalColumn: "IpsCursoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidaturas_TiposMobilidade_TipoMobilidadeID",
                        column: x => x.TipoMobilidadeID,
                        principalTable: "TiposMobilidade",
                        principalColumn: "TipoMobilidadeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidaturas_AspNetUsers_UtilizadorID",
                        column: x => x.UtilizadorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CandidaturaCursos",
                columns: table => new
                {
                    CandidaturaCursosID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CandidaturaID = table.Column<int>(nullable: false),
                    CursoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidaturaCursos", x => x.CandidaturaCursosID);
                    table.ForeignKey(
                        name: "FK_CandidaturaCursos_Candidaturas_CandidaturaID",
                        column: x => x.CandidaturaID,
                        principalTable: "Candidaturas",
                        principalColumn: "CandidaturaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidaturaCursos_Cursos_CursoID",
                        column: x => x.CursoID,
                        principalTable: "Cursos",
                        principalColumn: "CursoID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CandidaturaDocumentos",
                columns: table => new
                {
                    CandidaturaDocumentosID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CandidaturaID = table.Column<int>(nullable: false),
                    DocumentoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidaturaDocumentos", x => x.CandidaturaDocumentosID);
                    table.ForeignKey(
                        name: "FK_CandidaturaDocumentos_Candidaturas_CandidaturaID",
                        column: x => x.CandidaturaID,
                        principalTable: "Candidaturas",
                        principalColumn: "CandidaturaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidaturaDocumentos_Documentos_DocumentoID",
                        column: x => x.DocumentoID,
                        principalTable: "Documentos",
                        principalColumn: "DocumentoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidaturaCursos_CandidaturaID",
                table: "CandidaturaCursos",
                column: "CandidaturaID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidaturaCursos_CursoID",
                table: "CandidaturaCursos",
                column: "CursoID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidaturaDocumentos_CandidaturaID",
                table: "CandidaturaDocumentos",
                column: "CandidaturaID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidaturaDocumentos_DocumentoID",
                table: "CandidaturaDocumentos",
                column: "DocumentoID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_EmegerenciaParentescoID",
                table: "Candidaturas",
                column: "EmegerenciaParentescoID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_EstadoCandidaturaID",
                table: "Candidaturas",
                column: "EstadoCandidaturaID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_IpsCursoID",
                table: "Candidaturas",
                column: "IpsCursoID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_TipoMobilidadeID",
                table: "Candidaturas",
                column: "TipoMobilidadeID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_UtilizadorID",
                table: "Candidaturas",
                column: "UtilizadorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidaturaCursos");

            migrationBuilder.DropTable(
                name: "CandidaturaDocumentos");

            migrationBuilder.DropTable(
                name: "Candidaturas");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "EstadosCandidatura");
        }
    }
}
