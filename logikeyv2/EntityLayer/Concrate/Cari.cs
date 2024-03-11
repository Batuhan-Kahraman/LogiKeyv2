using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Cari
    {
        [Key]
        public int Cari_ID { get; set; }
        public int Cari_GrupID { get; set; }
        public int Cari_Tipi { get; set; } //1 Bireysel 2 Kurumsal
        public string? Cari_Unvan { get; set; }
        public string? Cari_TCNO_VergiNo { get; set; }
        public string? Cari_VergiDairesi { get; set; }
        public string? Cari_Kodu { get; set; }
        public string? Cari_YetkiliAdi { get; set; }
        public string? Cari_YetkiliSoyadi { get; set; }
        public string? Cari_CepNo { get; set; }
        public string? Cari_WebSitesi { get; set; }
        public string? Cari_FirmaEposta { get; set; }
        public string? Cari_FirmaTelefon { get; set; }
        public int Cari_IL_ID { get; set; }
        public int Cari_ILCE_ID { get; set; }
        public string? Cari_Adres { get; set; }
        public string? Cari_BankaAdi1 { get; set; }
        public string? Cari_BankaIBAN1 { get; set; }
        public string? Cari_BankaAdi2 { get; set; }
        public string? Cari_BankaIBAN2 { get; set; }
        public string? Cari_BankaAdi3 { get; set; }
        public string? Cari_BankaIBAN3 { get; set; }
        public int Firma_ID { get; set; }
        public int EkleyenKullanici_ID { get; set; }
        public int DuzenleyenKullanici_ID { get; set; }
        public DateTime Olusturma_Tarihi { get; set; }
        public DateTime Duzenleme_Tarihi { get; set; }

        public byte Durum { get; set; }
        public bool FaturaDurum { get; set; }
        

    }
}
