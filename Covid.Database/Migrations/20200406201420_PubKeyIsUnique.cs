using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid.Database.Migrations
{
    public partial class PubKeyIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PubKey",
                table: "Users",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PubKey",
                table: "Users",
                column: "PubKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_PubKey",
                table: "Users");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PubKey",
                table: "Users",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
