using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Infrastructure.Migrations
{
    public partial class colorcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CatagoryId",
                table: "ContentViewMobile",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CatagoryName",
                table: "ContentViewMobile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "ContentTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ContentTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "Catagories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Catagories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatagoryId",
                table: "ContentViewMobile");

            migrationBuilder.DropColumn(
                name: "CatagoryName",
                table: "ContentViewMobile");

            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "ContentTypes");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "ContentTypes");

            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "Catagories");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Catagories");
        }
    }
}
