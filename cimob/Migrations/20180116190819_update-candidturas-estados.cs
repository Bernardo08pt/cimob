using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace cimob.Migrations
{
    public partial class updatecandidturasestados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cor",
                table: "EstadosCandidatura",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "EstadosCandidatura",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cor",
                table: "EstadosCandidatura");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "EstadosCandidatura");
        }
    }
}
