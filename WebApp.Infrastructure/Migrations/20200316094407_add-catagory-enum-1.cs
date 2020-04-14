using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Infrastructure.Migrations
{
    public partial class addcatagoryenum1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Catagory",
                table: "ContentTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Catagory",
                table: "ContentTypes");

            migrationBuilder.AddColumn<int>(
                name: "Catagory",
                table: "ContentTypes",
                type: "string");
        }
    }
}
