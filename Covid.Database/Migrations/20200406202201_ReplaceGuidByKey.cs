using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid.Database.Migrations
{
    public partial class ReplaceGuidByKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Users_UserGuid",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_UserXGuid",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_UserYGuid",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Matches_UserXGuid",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_UserYGuid",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Alerts_UserGuid",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserXGuid",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UserYGuid",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "Alerts");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserXKey",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserYKey",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserKey",
                table: "Alerts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserXKey",
                table: "Matches",
                column: "UserXKey");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserYKey",
                table: "Matches",
                column: "UserYKey");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserKey",
                table: "Alerts",
                column: "UserKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Users_UserKey",
                table: "Alerts",
                column: "UserKey",
                principalTable: "Users",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserXKey",
                table: "Matches",
                column: "UserXKey",
                principalTable: "Users",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserYKey",
                table: "Matches",
                column: "UserYKey",
                principalTable: "Users",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Users_UserKey",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_UserXKey",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_UserYKey",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Matches_UserXKey",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_UserYKey",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Alerts_UserKey",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserXKey",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UserYKey",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UserKey",
                table: "Alerts");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserXGuid",
                table: "Matches",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserYGuid",
                table: "Matches",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserGuid",
                table: "Alerts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Guid");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserXGuid",
                table: "Matches",
                column: "UserXGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserYGuid",
                table: "Matches",
                column: "UserYGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserGuid",
                table: "Alerts",
                column: "UserGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Users_UserGuid",
                table: "Alerts",
                column: "UserGuid",
                principalTable: "Users",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserXGuid",
                table: "Matches",
                column: "UserXGuid",
                principalTable: "Users",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserYGuid",
                table: "Matches",
                column: "UserYGuid",
                principalTable: "Users",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
