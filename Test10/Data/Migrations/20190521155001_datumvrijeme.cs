using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Test10.Data.Migrations
{
    public partial class datumvrijeme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NaslovPoruke",
                table: "Poruka",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Vrijeme",
                table: "Poruka",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "NazivOglas",
                table: "Oglas",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Datum",
                table: "Oglas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NaslovPoruke",
                table: "Poruka");

            migrationBuilder.DropColumn(
                name: "Vrijeme",
                table: "Poruka");

            migrationBuilder.DropColumn(
                name: "Datum",
                table: "Oglas");

            migrationBuilder.AlterColumn<string>(
                name: "NazivOglas",
                table: "Oglas",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 80);
        }
    }
}
