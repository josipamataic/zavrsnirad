using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Test10.Data.Migrations
{
    public partial class RezervacijavrijemeKraj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NazivTerena",
                table: "Teren",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Kraj",
                table: "Rezervacija",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kraj",
                table: "Rezervacija");

            migrationBuilder.AlterColumn<string>(
                name: "NazivTerena",
                table: "Teren",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
