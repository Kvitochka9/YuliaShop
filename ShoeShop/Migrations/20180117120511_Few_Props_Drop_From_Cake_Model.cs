using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShoeShop.Migrations
{
    public partial class Few_Props_Drop_From_Shoe_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllergyInfo",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "ImageThumbnailUrl",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Shoes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllergyInfo",
                table: "Shoes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageThumbnailUrl",
                table: "Shoes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Shoes",
                nullable: false,
                defaultValue: false);
        }
    }
}
