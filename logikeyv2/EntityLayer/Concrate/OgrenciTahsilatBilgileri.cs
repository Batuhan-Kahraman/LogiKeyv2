using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class OgrenciTahsilatBilgileri
    {
        [Key]
        public int Id { get; set; }
        public int OgrenciId { get; set; }
        public decimal Tutar { get; set; }
        public int TaksitSayisi { get; set; }
        public int? OdemeSekli { get; set; }

        public DateTime OdemeTarihi { get; set; }
        public bool OdemeDurumu { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime DuzenlemeTarihi { get; set; }
        public bool Durum { get; set; }
        public int EkleyenKullaniciID { get; set; }
        public int FirmaID { get; set; }
        public int DuzenleyenKullaniciID { get; set; }
    }
}
