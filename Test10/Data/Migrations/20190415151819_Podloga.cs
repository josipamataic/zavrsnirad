using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Test10.Data.Migrations
{
    public partial class Podloga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Podloga",
                table: "Teren");

            migrationBuilder.AddColumn<int>(
                name: "PodlogaId",
                table: "Teren",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Podloga",
                columns: table => new
                {
                    IdPodloga = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivPodloga = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podloga", x => x.IdPodloga);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teren_PodlogaId",
                table: "Teren",
                column: "PodlogaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teren_Podloga_PodlogaId",
                table: "Teren",
                column: "PodlogaId",
                principalTable: "Podloga",
                principalColumn: "IdPodloga",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teren_Podloga_PodlogaId",
                table: "Teren");

            migrationBuilder.DropTable(
                name: "Podloga");

            migrationBuilder.DropIndex(
                name: "IX_Teren_PodlogaId",
                table: "Teren");

            migrationBuilder.DropColumn(
                name: "PodlogaId",
                table: "Teren");

            migrationBuilder.AddColumn<string>(
                name: "Podloga",
                table: "Teren",
                maxLength: 50,
                nullable: true);
        }
    }
}
