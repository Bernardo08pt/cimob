using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace cimob.Migrations
{
    public partial class update_editais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Editais_TiposMobilidade_TipoMobilidadeID",
                table: "Editais");

            migrationBuilder.AlterColumn<int>(
                name: "TipoMobilidadeID",
                table: "Editais",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeFicheiro",
                table: "Editais",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Editais_TiposMobilidade_TipoMobilidadeID",
                table: "Editais",
                column: "TipoMobilidadeID",
                principalTable: "TiposMobilidade",
                principalColumn: "TipoMobilidadeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Editais_TiposMobilidade_TipoMobilidadeID",
                table: "Editais");

            migrationBuilder.DropColumn(
                name: "NomeFicheiro",
                table: "Editais");

            migrationBuilder.AlterColumn<int>(
                name: "TipoMobilidadeID",
                table: "Editais",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Editais_TiposMobilidade_TipoMobilidadeID",
                table: "Editais",
                column: "TipoMobilidadeID",
                principalTable: "TiposMobilidade",
                principalColumn: "TipoMobilidadeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
