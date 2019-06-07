using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Test10.Data.Migrations
{
    public partial class Inicijalna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumRodenja",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Prezime",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Poruka",
                columns: table => new
                {
                    IdPoruka = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IgracPosiljateljId = table.Column<string>(nullable: true),
                    IgracPrimateljId = table.Column<string>(nullable: true),
                    TekstPoruke = table.Column<string>(maxLength: 280, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poruka", x => x.IdPoruka);
                    table.ForeignKey(
                        name: "FK_Poruka_AspNetUsers_IgracPosiljateljId",
                        column: x => x.IgracPosiljateljId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Poruka_AspNetUsers_IgracPrimateljId",
                        column: x => x.IgracPrimateljId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeniskiKlub",
                columns: table => new
                {
                    IdTeniskiKlub = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adresa = table.Column<string>(maxLength: 50, nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    BrojTerena = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeniskiKlub", x => x.IdTeniskiKlub);
                    table.ForeignKey(
                        name: "FK_TeniskiKlub_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Oglas",
                columns: table => new
                {
                    IdOglas = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(maxLength: 140, nullable: true),
                    TeniskiKlubId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglas", x => x.IdOglas);
                    table.ForeignKey(
                        name: "FK_Oglas_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Oglas_TeniskiKlub_TeniskiKlubId",
                        column: x => x.TeniskiKlubId,
                        principalTable: "TeniskiKlub",
                        principalColumn: "IdTeniskiKlub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teren",
                columns: table => new
                {
                    IdTeren = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Podloga = table.Column<string>(maxLength: 50, nullable: true),
                    Prostor = table.Column<string>(maxLength: 50, nullable: true),
                    TeniskiKlubId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teren", x => x.IdTeren);
                    table.ForeignKey(
                        name: "FK_Teren_TeniskiKlub_TeniskiKlubId",
                        column: x => x.TeniskiKlubId,
                        principalTable: "TeniskiKlub",
                        principalColumn: "IdTeniskiKlub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    IdRezervacija = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumVrijeme = table.Column<DateTime>(type: "datetime", nullable: false),
                    IgracId = table.Column<string>(nullable: true),
                    TerenId = table.Column<int>(nullable: false),
                    UpraviteljId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.IdRezervacija);
                    table.ForeignKey(
                        name: "FK_Rezervacija_AspNetUsers_IgracId",
                        column: x => x.IgracId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Teren_TerenId",
                        column: x => x.TerenId,
                        principalTable: "Teren",
                        principalColumn: "IdTeren",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervacija_AspNetUsers_UpraviteljId",
                        column: x => x.UpraviteljId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Oglas_ApplicationUserId",
                table: "Oglas",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Oglas_TeniskiKlubId",
                table: "Oglas",
                column: "TeniskiKlubId");

            migrationBuilder.CreateIndex(
                name: "IX_Poruka_IgracPosiljateljId",
                table: "Poruka",
                column: "IgracPosiljateljId");

            migrationBuilder.CreateIndex(
                name: "IX_Poruka_IgracPrimateljId",
                table: "Poruka",
                column: "IgracPrimateljId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_IgracId",
                table: "Rezervacija",
                column: "IgracId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_TerenId",
                table: "Rezervacija",
                column: "TerenId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_UpraviteljId",
                table: "Rezervacija",
                column: "UpraviteljId");

            migrationBuilder.CreateIndex(
                name: "IX_TeniskiKlub_ApplicationUserId",
                table: "TeniskiKlub",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teren_TeniskiKlubId",
                table: "Teren",
                column: "TeniskiKlubId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Oglas");

            migrationBuilder.DropTable(
                name: "Poruka");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "Teren");

            migrationBuilder.DropTable(
                name: "TeniskiKlub");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DatumRodenja",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Prezime",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
