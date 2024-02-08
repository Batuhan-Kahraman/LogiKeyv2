using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class aracEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IseBaslamaNot",
                table: "Arac",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IseBaslamaSuresi",
                table: "Arac",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IseBaslamaTarihi",
                table: "Arac",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KiralamaSuresi",
                table: "Arac",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "KiralamaTarihi",
                table: "Arac",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "KiralamaTutari",
                table: "Arac",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IseBaslamaNot",
                table: "Arac");

            migrationBuilder.DropColumn(
                name: "IseBaslamaSuresi",
                table: "Arac");

            migrationBuilder.DropColumn(
                name: "IseBaslamaTarihi",
                table: "Arac");

            migrationBuilder.DropColumn(
                name: "KiralamaSuresi",
                table: "Arac");

            migrationBuilder.DropColumn(
                name: "KiralamaTarihi",
                table: "Arac");

            migrationBuilder.DropColumn(
                name: "KiralamaTutari",
                table: "Arac");
        }
    }
}
