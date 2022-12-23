using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShoeShop.Migrations
{
    public partial class Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Shoes");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Shoes",
                maxLength: 255,
                nullable: false,
                defaultValue: 0);
        }
    }
}
