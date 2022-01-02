using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkBahceAPI.Migrations
{
    public partial class SosyalTesisPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "SosyalTesiss",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "SosyalTesiss");
        }
    }
}
