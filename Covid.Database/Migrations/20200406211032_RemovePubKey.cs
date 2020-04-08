using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid.Database.Migrations
{
    public partial class RemovePubKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_PubKey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PubKey",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PubKey",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PubKey",
                table: "Users",
                column: "PubKey",
                unique: true);
        }
    }
}
