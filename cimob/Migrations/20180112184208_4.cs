using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace cimob.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IpsCursos_IpsEscolas_IpsEscolaID",
                table: "IpsCursos");

            migrationBuilder.AlterColumn<int>(
                name: "IpsEscolaID",
                table: "IpsCursos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IpsCursos_IpsEscolas_IpsEscolaID",
                table: "IpsCursos",
                column: "IpsEscolaID",
                principalTable: "IpsEscolas",
                principalColumn: "IpsEscolaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IpsCursos_IpsEscolas_IpsEscolaID",
                table: "IpsCursos");

            migrationBuilder.AlterColumn<int>(
                name: "IpsEscolaID",
                table: "IpsCursos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_IpsCursos_IpsEscolas_IpsEscolaID",
                table: "IpsCursos",
                column: "IpsEscolaID",
                principalTable: "IpsEscolas",
                principalColumn: "IpsEscolaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
