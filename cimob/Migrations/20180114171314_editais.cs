using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace cimob.Migrations
{
    public partial class editais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Editais",
                columns: table => new
                {
                    EditalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caminho = table.Column<string>(nullable: true),
                    DataLimite = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    TipoMobilidadeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editais", x => x.EditalID);
                    table.ForeignKey(
                        name: "FK_Editais_TiposMobilidade_TipoMobilidadeID",
                        column: x => x.TipoMobilidadeID,
                        principalTable: "TiposMobilidade",
                        principalColumn: "TipoMobilidadeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Editais_TipoMobilidadeID",
                table: "Editais",
                column: "TipoMobilidadeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Editais");
        }
    }
}
