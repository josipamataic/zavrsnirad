using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Test10.Data.Migrations
{
    public partial class UpraviteljIgrac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oglas_AspNetUsers_ApplicationUserId",
                table: "Oglas");

            migrationBuilder.DropForeignKey(
                name: "FK_TeniskiKlub_AspNetUsers_ApplicationUserId",
                table: "TeniskiKlub");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "TeniskiKlub",
                newName: "UpraviteljId");

            migrationBuilder.RenameIndex(
                name: "IX_TeniskiKlub_ApplicationUserId",
                table: "TeniskiKlub",
                newName: "IX_TeniskiKlub_UpraviteljId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Oglas",
                newName: "IgracId");

            migrationBuilder.RenameIndex(
                name: "IX_Oglas_ApplicationUserId",
                table: "Oglas",
                newName: "IX_Oglas_IgracId");

            migrationBuilder.AlterColumn<string>(
                name: "Opis",
                table: "Oglas",
                maxLength: 140,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 140,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Oglas_AspNetUsers_IgracId",
                table: "Oglas",
                column: "IgracId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeniskiKlub_AspNetUsers_UpraviteljId",
                table: "TeniskiKlub",
                column: "UpraviteljId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oglas_AspNetUsers_IgracId",
                table: "Oglas");

            migrationBuilder.DropForeignKey(
                name: "FK_TeniskiKlub_AspNetUsers_UpraviteljId",
                table: "TeniskiKlub");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UpraviteljId",
                table: "TeniskiKlub",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TeniskiKlub_UpraviteljId",
                table: "TeniskiKlub",
                newName: "IX_TeniskiKlub_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "IgracId",
                table: "Oglas",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Oglas_IgracId",
                table: "Oglas",
                newName: "IX_Oglas_ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Opis",
                table: "Oglas",
                maxLength: 140,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 140);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AddForeignKey(
                name: "FK_Oglas_AspNetUsers_ApplicationUserId",
                table: "Oglas",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeniskiKlub_AspNetUsers_ApplicationUserId",
                table: "TeniskiKlub",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
