using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class tanimlamalar3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirmaID",
                table: "YakitTipi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FirmaID",
                table: "Sahiplik",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FirmaID",
                table: "Model",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarkaID",
                table: "Model",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FirmaID",
                table: "Marka",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FirmaID",
                table: "Grup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FirmaID",
                table: "Durumlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FirmaID",
                table: "AracTur",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AracTurID",
                table: "AracTip",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FirmaID",
                table: "AracTip",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FirmaID",
                table: "AkuTipi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirmaID",
                table: "YakitTipi");

            migrationBuilder.DropColumn(
                name: "FirmaID",
                table: "Sahiplik");

            migrationBuilder.DropColumn(
                name: "FirmaID",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "MarkaID",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "FirmaID",
                table: "Marka");

            migrationBuilder.DropColumn(
                name: "FirmaID",
                table: "Grup");

            migrationBuilder.DropColumn(
                name: "FirmaID",
                table: "Durumlar");

            migrationBuilder.DropColumn(
                name: "FirmaID",
                table: "AracTur");

            migrationBuilder.DropColumn(
                name: "AracTurID",
                table: "AracTip");

            migrationBuilder.DropColumn(
                name: "FirmaID",
                table: "AracTip");

            migrationBuilder.DropColumn(
                name: "FirmaID",
                table: "AkuTipi");
        }
    }
}
