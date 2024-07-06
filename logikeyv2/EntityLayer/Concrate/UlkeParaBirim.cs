using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class UlkeParaBirim
    {
        [Key]
        public int ID { get; set; }
        public string? ulke { get; set; }
        public string? parabirim { get; set; }
        public string? currency { get; set; }
        public string? simge { get; set; }
        public int Firma_ID { get; set; }
        public int EkleyenKullanici_ID { get; set; }
        public int DuzenleyenKullanici_ID { get; set; }
        public DateTime Olusturma_Tarihi { get; set; }
        public DateTime Duzenleme_Tarihi { get; set; }
        public byte Durum { get; set; }
    }
}
