using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Infrastructure.Migrations
{
    public partial class coverimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CatagoryBanglaName",
                table: "ContentViewMobile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "ContentViewMobile",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MobileCoverImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileCoverImages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileCoverImages");

            migrationBuilder.DropColumn(
                name: "CatagoryBanglaName",
                table: "ContentViewMobile");

            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "ContentViewMobile");
        }
    }
}
