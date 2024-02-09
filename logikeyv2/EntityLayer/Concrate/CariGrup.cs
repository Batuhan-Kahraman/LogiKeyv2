using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class CariGrup
    {
        [Key]
        public int CariGrup_ID { get; set; }
        public string? CariGrup_Adi { get; set; }
        public int Firma_ID { get; set; }
        public int EkleyenKullanici_ID { get; set; }
        public DateTime Olusturma_Tarihi { get; set; }
        public DateTime Duzenleme_Tarihi { get; set; }
        public bool Durum { get; set; }
    }
}
