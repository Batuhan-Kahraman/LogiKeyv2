using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdresOzellikTanimlama",
                columns: table => new
                {
                    AdresID = table.Column<int>(name: "Adres_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ILCEKODU = table.Column<string>(name: "ILCE_KODU", type: "nvarchar(max)", nullable: true),
                    ILKODU = table.Column<string>(name: "IL_KODU", type: "nvarchar(max)", nullable: true),
                    Il = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ilce = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostaKodu = table.Column<string>(name: "Posta_Kodu", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdresOzellikTanimlama", x => x.AdresID);
                });

            migrationBuilder.CreateTable(
                name: "Firma",
                columns: table => new
                {
                    FirmaID = table.Column<int>(name: "Firma_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaModulID = table.Column<int>(name: "FirmaModul_ID", type: "int", nullable: false),
                    FirmaTipi = table.Column<int>(name: "Firma_Tipi", type: "int", nullable: false),
                    FirmaUnvan = table.Column<string>(name: "Firma_Unvan", type: "nvarchar(max)", nullable: true),
                    FirmaTCNOVKNO = table.Column<string>(name: "Firma_TCNO_VKNO", type: "nvarchar(max)", nullable: true),
                    FirmaVergiDairesi = table.Column<string>(name: "Firma_VergiDairesi", type: "nvarchar(max)", nullable: true),
                    FirmaAdresILID = table.Column<int>(name: "Firma_AdresIL_ID", type: "int", nullable: false),
                    FirmaAdresILCEID = table.Column<int>(name: "Firma_AdresILCE_ID", type: "int", nullable: false),
                    FirmaAdres = table.Column<string>(name: "Firma_Adres", type: "nvarchar(max)", nullable: true),
                    FirmaWebSitesi = table.Column<string>(name: "Firma_WebSitesi", type: "nvarchar(max)", nullable: true),
                    FirmaYetkiliAdi = table.Column<string>(name: "Firma_YetkiliAdi", type: "nvarchar(max)", nullable: true),
                    FirmaYetkiliSoyadi = table.Column<string>(name: "Firma_YetkiliSoyadi", type: "nvarchar(max)", nullable: true),
                    FirmaYetkiliEposta = table.Column<string>(name: "Firma_YetkiliEposta", type: "nvarchar(max)", nullable: true),
                    FirmaCepTel = table.Column<string>(name: "Firma_CepTel", type: "nvarchar(max)", nullable: true),
                    FirmaDurum = table.Column<byte>(name: "Firma_Durum", type: "tinyint", nullable: false),
                    EkleyenKullaniciID = table.Column<int>(name: "EkleyenKullanici_ID", type: "int", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firma", x => x.FirmaID);
                });

            migrationBuilder.CreateTable(
                name: "FirmaModul",
                columns: table => new
                {
                    FirmaModulID = table.Column<int>(name: "FirmaModul_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaID = table.Column<int>(name: "Firma_ID", type: "int", nullable: false),
                    ModulID = table.Column<int>(name: "Modul_ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmaModul", x => x.FirmaModulID);
                });

            migrationBuilder.CreateTable(
                name: "Izinler",
                columns: table => new
                {
                    IzinID = table.Column<int>(name: "Izin_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IzinAdi = table.Column<string>(name: "Izin_Adi", type: "nvarchar(max)", nullable: true),
                    IzinEklemeSil = table.Column<byte>(name: "Izin_Ekleme_Sil", type: "tinyint", nullable: false),
                    IzinListeleme = table.Column<byte>(name: "Izin_Listeleme", type: "tinyint", nullable: false),
                    IzinGuncelleme = table.Column<byte>(name: "Izin_Guncelleme", type: "tinyint", nullable: false),
                    Durum = table.Column<byte>(type: "tinyint", nullable: false),
                    EkleyenKullaniciID = table.Column<int>(name: "EkleyenKullanici_ID", type: "int", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izinler", x => x.IzinID);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciGrubu",
                columns: table => new
                {
                    KullaniciGrupID = table.Column<int>(name: "KullaniciGrup_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciGrupAdi = table.Column<string>(name: "KullaniciGrup_Adi", type: "nvarchar(max)", nullable: true),
                    FirmaID = table.Column<int>(name: "Firma_ID", type: "int", nullable: false),
                    KullaniciGrupDurum = table.Column<byte>(name: "KullaniciGrup_Durum", type: "tinyint", nullable: false),
                    EkleyenKullaniciID = table.Column<int>(name: "EkleyenKullanici_ID", type: "int", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciGrubu", x => x.KullaniciGrupID);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    KullaniciID = table.Column<int>(name: "Kullanici_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciGrupID = table.Column<int>(name: "KullaniciGrup_ID", type: "int", nullable: false),
                    KullaniciIsim = table.Column<string>(name: "Kullanici_Isim", type: "nvarchar(max)", nullable: true),
                    KullaniciSoyisim = table.Column<string>(name: "Kullanici_Soyisim", type: "nvarchar(max)", nullable: true),
                    KullaniciEposta = table.Column<string>(name: "Kullanici_Eposta", type: "nvarchar(max)", nullable: true),
                    KullaniciSifre = table.Column<string>(name: "Kullanici_Sifre", type: "nvarchar(max)", nullable: true),
                    KullaniciTelefon = table.Column<string>(name: "Kullanici_Telefon", type: "nvarchar(max)", nullable: true),
                    KullaniciDurum = table.Column<bool>(name: "Kullanici_Durum", type: "bit", nullable: false),
                    KullaniciOlusturmaTarihi = table.Column<DateTime>(name: "Kullanici_OlusturmaTarihi", type: "datetime2", nullable: false),
                    KullaniciDuzenlemeTarihi = table.Column<DateTime>(name: "Kullanici_DuzenlemeTarihi", type: "datetime2", nullable: false),
                    FirmaID = table.Column<int>(name: "Firma_ID", type: "int", nullable: false),
                    EkleyenKullaniciID = table.Column<int>(name: "EkleyenKullanici_ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.KullaniciID);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciYetkiler",
                columns: table => new
                {
                    YetkiID = table.Column<int>(name: "Yetki_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaModulID = table.Column<int>(name: "FirmaModul_ID", type: "int", nullable: false),
                    IzinlerID = table.Column<int>(name: "Izinler_ID", type: "int", nullable: false),
                    KullaniciGruplariID = table.Column<int>(name: "KullaniciGruplari_ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciYetkiler", x => x.YetkiID);
                });

            migrationBuilder.CreateTable(
                name: "Moduller",
                columns: table => new
                {
                    ModulID = table.Column<int>(name: "Modul_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModulAdi = table.Column<string>(name: "Modul_Adi", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moduller", x => x.ModulID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdresOzellikTanimlama");

            migrationBuilder.DropTable(
                name: "Firma");

            migrationBuilder.DropTable(
                name: "FirmaModul");

            migrationBuilder.DropTable(
                name: "Izinler");

            migrationBuilder.DropTable(
                name: "KullaniciGrubu");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "KullaniciYetkiler");

            migrationBuilder.DropTable(
                name: "Moduller");
        }
    }
}
