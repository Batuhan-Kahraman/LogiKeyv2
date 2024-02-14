using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Tasima
    {
        [Key]
        public int Tasima_ID { get; set; }

        public int Un_ID { get; set; }
        public int TasinacakUrun_ID { get; set; }
        public int Cari_Odeme_Yapan_ID { get; set; }
        public int Ucretlendirme_ID { get; set; }
        public int Arac_ID { get; set; }
        public int Surucu1_ID { get; set; }
        public int Surucu2_ID { get; set; }
        public int Surucu3_ID { get; set; }
        public int AracTip_ID { get; set; }
        public int AracTuru_ID { get; set; }
        public int CekiciPlaka_ID { get; set; }
        public int DorseTipi_ID { get; set; }
        public int DorsePlaka_ID { get; set; }
        public decimal AracGoz1 { get; set; }
        public decimal AracGoz2 { get; set; }
        public decimal AracGoz3 { get; set; }
        public decimal AracGoz4 { get; set; }
        public decimal AracGoz5 { get; set; }
        public decimal AracGoz6 { get; set; }
        public int Birim1_ID { get; set; }
        public int Birim2_ID { get; set; }
        public int Birim3_ID { get; set; }
        public int Birim4_ID { get; set; }
        public int Birim5_ID { get; set; }
        public int Birim6_ID { get; set; }

        public decimal UrunBirimFiyat { get; set; }
        public decimal Goz1Miktar { get; set; }
        public decimal Goz2Miktar { get; set; }
        public decimal Goz3Miktar { get; set; }
        public decimal Goz4Miktar { get; set; }
        public decimal Goz5Miktar { get; set; }
        public decimal Goz6Miktar { get; set; }
        public decimal YuklemeMiktari1 { get; set; }
        public decimal YuklemeMiktari2 { get; set; }
        public decimal YuklemeMiktari3 { get; set; }
        public decimal YuklemeMiktari4 { get; set; }
        public decimal YuklemeMiktari5 { get; set; }
        public decimal YuklemeMiktari6 { get; set; }
        public int GondericiCari_ID { get; set; }
        public int AliciCari_ID { get; set; }
        public int MalYuklemeAdres_IL_ID { get; set; }
        public int MalYuklemeAdres_ILCE_ID { get; set; }
        public DateTime GondericiFirmaTarihSaat { get; set; }
        public int IndirilenAdres_IL_ID { get; set; }
        public int IndirilenAdres_ILCE_ID { get; set; }
        public DateTime AliciFirmaTarihSaat { get; set; }
        public decimal Birim_SeferFiyat { get; set; }
        public decimal ToplamYuklenenMiktar { get; set; }
        public decimal NakliyeBedelTutar_KDVsiz { get; set; }
        public decimal NakliyeBedelTutar_KDV { get; set; }
        public decimal NakliyeBedeliToplam_KDVli { get; set; }
        public int Firma_ID { get; set; }
        public int EkleyenKullanici_ID { get; set; }
        public int DuzenleyenKullanıcı_ID { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime DuzenlemeTarihi { get; set; }
        public byte Durum { get; set; }
        public int TasimaTipi_ID { get; set; }
    }
}
