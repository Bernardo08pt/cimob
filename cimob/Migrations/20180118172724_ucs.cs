using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace cimob.Migrations
{
    public partial class ucs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UCAvaliacao",
                columns: table => new
                {
                    UCAvaliacaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CandidaturaID = table.Column<int>(nullable: true),
                    Criterio = table.Column<string>(nullable: true),
                    UC = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UCAvaliacao", x => x.UCAvaliacaoID);
                    table.ForeignKey(
                        name: "FK_UCAvaliacao_Candidaturas_CandidaturaID",
                        column: x => x.CandidaturaID,
                        principalTable: "Candidaturas",
                        principalColumn: "CandidaturaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UCAvaliacao_CandidaturaID",
                table: "UCAvaliacao",
                column: "CandidaturaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UCAvaliacao");
        }
    }
}
