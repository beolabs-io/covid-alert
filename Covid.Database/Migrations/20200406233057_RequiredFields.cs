using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid.Database.Migrations
{
    public partial class RequiredFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserYKey",
                table: "Matches",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserXKey",
                table: "Matches",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserKey",
                table: "Alerts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Users_UserKey",
                table: "Alerts",
                column: "UserKey",
                principalTable: "Users",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserXKey",
                table: "Matches",
                column: "UserXKey",
                principalTable: "Users",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserYKey",
                table: "Matches",
                column: "UserYKey",
                principalTable: "Users",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<string>(
                name: "UserYKey",
                table: "Matches",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserXKey",
                table: "Matches",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserKey",
                table: "Alerts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

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
    }
}
