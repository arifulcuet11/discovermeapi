using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Infrastructure.Migrations
{
    public partial class likeread : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalComment",
                table: "Contents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalLike",
                table: "Contents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRead",
                table: "Contents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContentLikes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LikedBy = table.Column<long>(nullable: false),
                    ContentId = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentLikes_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarkAsReads",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarkedBy = table.Column<long>(nullable: false),
                    ContentId = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    MarkedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkAsReads", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentLikes_ContentId",
                table: "ContentLikes",
                column: "ContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentLikes");

            migrationBuilder.DropTable(
                name: "MarkAsReads");

            migrationBuilder.DropColumn(
                name: "TotalComment",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "TotalLike",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "TotalRead",
                table: "Contents");
        }
    }
}
