using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Cari_OdemeYapan
    {
        [Key]
        public int Cari_ID { get; set; }
        public int OdemeYapan_ID { get; set; }
        public string? OdemeYapan_Adi { get; set; }
        public int Firma_ID { get; set; }
        public int EkleyenKullanici_ID { get; set; }
        public int DuzenleyenKullanici_ID { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime DuzenlemeTarihi { get; set; }

        public bool Durum { get; set; }

    }
}
