using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShoeShop.Migrations
{
    public partial class Removed_ShoeId_Prop_From_OrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Shoes_ShoeId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ShoeId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ShoeId",
                table: "OrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "ShoeName",
                table: "OrderDetails",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShoeName",
                table: "OrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "ShoeId",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ShoeId",
                table: "OrderDetails",
                column: "ShoeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Shoes_ShoeId",
                table: "OrderDetails",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
