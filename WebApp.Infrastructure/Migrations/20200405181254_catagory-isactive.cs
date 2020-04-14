﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Infrastructure.Migrations
{
    public partial class catagoryisactive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Catagories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Catagories");
        }
    }
}
