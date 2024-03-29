using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class FaturaOkul
    {
        [Key]
        public int OkulFaturaBilgiID { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Eposta { get; set; }
        public string TelefonNo { get; set; }
        public string TcKimlikNo { get; set; }
        public int ilID { get; set; }
        public int ilceID { get; set; }
        public string Adres { get; set; }
        public string BankaAdi { get; set; }
        public string IbanNo { get; set; }
        public int CariOgrenciID { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime DuzenlemeTarihi { get; set; }
        public bool Durum { get; set; }
        public int EkleyenKullaniciID { get; set; }
        public int FirmaID { get; set; }
        public int DuzenleyenKullaniciID { get; set; }
    }
}
