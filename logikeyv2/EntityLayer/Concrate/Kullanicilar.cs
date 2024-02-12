using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Kullanicilar
    {
        [Key]
        public int Kullanici_ID { get; set; }
        public int KullaniciGrup_ID { get; set; }
        public string? Kullanici_Isim { get; set; }
        public string? Kullanici_Soyisim { get; set; }
        public string? Kullanici_Eposta { get; set; }
        public string? Kullanici_Sifre { get; set; }
        public string? Kullanici_Telefon { get; set; }
        public byte Kullanici_Durum { get; set; }
        public DateTime Kullanici_OlusturmaTarihi { get; set; }
        public DateTime Kullanici_DuzenlemeTarihi { get; set; }
        public int Firma_ID { get; set; }
        public int EkleyenKullanici_ID { get; set; }

   
    }

}
