using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class CariUcretlendirme
    {
        [Key]
        public int Ucretlendirme_ID { get; set; }
        public string? Ucretlendirme_Adi { get; set; }
        public int Firma_ID { get; set; }
        public int EkleyenKullanici_ID { get; set; }
        public int DuzenleyenKullanici_ID { get; set; }
        public DateTime Olusturma_Tarihi { get; set; }
        public DateTime Duzenlenme_Tarihi { get; set; }
        public bool Durum { get; set; }

    }
}
