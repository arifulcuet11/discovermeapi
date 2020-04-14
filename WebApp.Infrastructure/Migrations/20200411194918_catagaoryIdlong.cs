using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Infrastructure.Migrations
{
    public partial class catagaoryIdlong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentTypes_Catagories_CatagoryId1",
                table: "ContentTypes");

            migrationBuilder.DropIndex(
                name: "IX_ContentTypes_CatagoryId1",
                table: "ContentTypes");

            migrationBuilder.DropColumn(
                name: "CatagoryId1",
                table: "ContentTypes");

            migrationBuilder.AlterColumn<long>(
                name: "CatagoryId",
                table: "ContentTypes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypes_CatagoryId",
                table: "ContentTypes",
                column: "CatagoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentTypes_Catagories_CatagoryId",
                table: "ContentTypes",
                column: "CatagoryId",
                principalTable: "Catagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentTypes_Catagories_CatagoryId",
                table: "ContentTypes");

            migrationBuilder.DropIndex(
                name: "IX_ContentTypes_CatagoryId",
                table: "ContentTypes");

            migrationBuilder.AlterColumn<int>(
                name: "CatagoryId",
                table: "ContentTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CatagoryId1",
                table: "ContentTypes",
                type: "bigint",
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
    }
}
