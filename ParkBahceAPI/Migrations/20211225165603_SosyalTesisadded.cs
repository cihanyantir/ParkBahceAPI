using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkBahceAPI.Migrations
{
    public partial class SosyalTesisadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SosyalTesiss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Doluluk = table.Column<int>(type: "int", nullable: false),
                    MilletBahcesiId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SosyalTesiss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SosyalTesiss_MilletBahcesis_MilletBahcesiId",
                        column: x => x.MilletBahcesiId,
                        principalTable: "MilletBahcesis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SosyalTesiss_MilletBahcesiId",
                table: "SosyalTesiss",
                column: "MilletBahcesiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SosyalTesiss");
        }
    }
}
