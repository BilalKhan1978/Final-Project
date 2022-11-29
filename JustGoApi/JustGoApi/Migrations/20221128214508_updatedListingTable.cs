using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustGoApi.Migrations
{
    public partial class updatedListingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Listings");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Listings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Listings");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Listings",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Listings",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
