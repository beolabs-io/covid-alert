using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid.Database.Migrations
{
    public partial class UpdateMatchesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_UserXGuid",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UserXId",
                table: "Matches");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserXGuid",
                table: "Matches",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserXGuid",
                table: "Matches",
                column: "UserXGuid",
                principalTable: "Users",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_UserXGuid",
                table: "Matches");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserXGuid",
                table: "Matches",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserXId",
                table: "Matches",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserXGuid",
                table: "Matches",
                column: "UserXGuid",
                principalTable: "Users",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
