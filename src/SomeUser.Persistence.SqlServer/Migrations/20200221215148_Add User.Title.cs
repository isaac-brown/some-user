using Microsoft.EntityFrameworkCore.Migrations;

namespace SomeUser.Persistence.SqlServer.Migrations
{
    public partial class AddUserTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "User");
        }
    }
}
