using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Infrastructure.Migrations
{
    public partial class catagaoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Catagory",
                table: "ContentTypes");

            migrationBuilder.AddColumn<int>(
                name: "CatagoryId",
                table: "ContentTypes",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CatagoryId1",
                table: "ContentTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypes_CatagoryId1",
                table: "ContentTypes",
                column: "CatagoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentTypes_Catagories_CatagoryId1",
                table: "ContentTypes",
                column: "CatagoryId1",
                principalTable: "Catagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentTypes_Catagories_CatagoryId1",
                table: "ContentTypes");

            migrationBuilder.DropIndex(
                name: "IX_ContentTypes_CatagoryId1",
                table: "ContentTypes");

            migrationBuilder.DropColumn(
                name: "CatagoryId",
                table: "ContentTypes");

            migrationBuilder.DropColumn(
                name: "CatagoryId1",
                table: "ContentTypes");

            migrationBuilder.AddColumn<int>(
                name: "Catagory",
                table: "ContentTypes",
                type: "int",
                nullable: true);
        }
    }
}
