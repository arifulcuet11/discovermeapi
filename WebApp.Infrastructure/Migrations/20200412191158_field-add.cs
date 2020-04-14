using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Infrastructure.Migrations
{
    public partial class fieldadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BanglaName",
                table: "ContentTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BanglaDescription",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BanglaTitle",
                table: "Contents",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BanglaName",
                table: "Catagories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanglaName",
                table: "ContentTypes");

            migrationBuilder.DropColumn(
                name: "BanglaDescription",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "BanglaTitle",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "BanglaName",
                table: "Catagories");
        }
    }
}
