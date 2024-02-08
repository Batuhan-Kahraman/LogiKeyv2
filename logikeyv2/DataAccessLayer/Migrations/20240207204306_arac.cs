using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class arac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arac",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plaka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SahiplikID = table.Column<int>(type: "int", nullable: true),
                    GrupID = table.Column<int>(type: "int", nullable: true),
                    AracTurID = table.Column<int>(type: "int", nullable: true),
                    AracTipID = table.Column<int>(type: "int", nullable: true),
                    BakimPeriyodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Goz1 = table.Column<int>(type: "int", nullable: true),
                    Goz2 = table.Column<int>(type: "int", nullable: true),
                    Goz3 = table.Column<int>(type: "int", nullable: true),
                    Goz4 = table.Column<int>(type: "int", nullable: true),
                    Goz5 = table.Column<int>(type: "int", nullable: true),
                    Goz6 = table.Column<int>(type: "int", nullable: true),
                    MarkaID = table.Column<int>(type: "int", nullable: true),
                    ModelID = table.Column<int>(type: "int", nullable: true),
                    Yil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SirketID = table.Column<int>(type: "int", nullable: true),
                    BunyeGirisTarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BunyeGirisFiyat = table.Column<float>(type: "real", nullable: true),
                    BunyeGirisNot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BunyeCikisTarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BunyeCikisFiyat = table.Column<float>(type: "real", nullable: true),
                    BunyeCikisNot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HGS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepoSensorID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekipman = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notlar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotorSeriNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotorGucu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YakitTipiID = table.Column<int>(type: "int", nullable: true),
                    YakitDepo = table.Column<int>(type: "int", nullable: true),
                    YakitSarfiyat = table.Column<int>(type: "int", nullable: true),
                    BosAgirlik = table.Column<float>(type: "real", nullable: true),
                    AzamiYukluAgirlik = table.Column<float>(type: "real", nullable: true),
                    IstiapHaddi = table.Column<float>(type: "real", nullable: true),
                    KasaEn = table.Column<float>(type: "real", nullable: true),
                    KasaBoy = table.Column<float>(type: "real", nullable: true),
                    KasaYukseklik = table.Column<float>(type: "real", nullable: true),
                    EuroPalet = table.Column<int>(type: "int", nullable: true),
                    LastikTip1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastikTip2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastikTip3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AkuTipID = table.Column<int>(type: "int", nullable: true),
                    AkuAmperBirim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AkuAdet = table.Column<int>(type: "int", nullable: true),
                    MotorYagTipi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotorYagListesi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotorYagPeriyot = table.Column<float>(type: "real", nullable: true),
                    HidrolikYagTipi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HidrolikYagListesi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HidrolikYagPeriyot = table.Column<float>(type: "real", nullable: true),
                    GresYagTipi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GresYagKg = table.Column<float>(type: "real", nullable: true),
                    HavaFiltreKm = table.Column<int>(type: "int", nullable: true),
                    HavaFiltreAdet = table.Column<int>(type: "int", nullable: true),
                    MazotFiltreKm = table.Column<int>(type: "int", nullable: true),
                    MazotFiltreAdet = table.Column<int>(type: "int", nullable: true),
                    YagFiltreKm = table.Column<int>(type: "int", nullable: true),
                    YagFiltreAdet = table.Column<int>(type: "int", nullable: true),
                    RuhsatSahibi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TescilTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TescilSiraNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TescilSeriNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrafigeCikisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VerildigiIlID = table.Column<int>(type: "int", nullable: true),
                    VerildigiIlceID = table.Column<int>(type: "int", nullable: true),
                    RuhsatSonKullanma = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MotorluAracSeriNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaksimetreSeriNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TakografSeriNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GarantiBaslangicTarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GarantiBitisTarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GarantiBaslangicKm = table.Column<int>(type: "int", nullable: true),
                    GarantiBitisKm = table.Column<int>(type: "int", nullable: true),
                    GarantiKmLimit = table.Column<int>(type: "int", nullable: true),
                    FirmaID = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    OlusturanId = table.Column<int>(type: "int", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenleyenID = table.Column<int>(type: "int", nullable: false),
                    DuzenlemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arac", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LastikTipi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirmaID = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    OlusturanId = table.Column<int>(type: "int", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenleyenID = table.Column<int>(type: "int", nullable: false),
                    DuzenlemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastikTipi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MasrafTipi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirmaID = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    OlusturanId = table.Column<int>(type: "int", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenleyenID = table.Column<int>(type: "int", nullable: false),
                    DuzenlemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasrafTipi", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arac");

            migrationBuilder.DropTable(
                name: "LastikTipi");

            migrationBuilder.DropTable(
                name: "MasrafTipi");
        }
    }
}
