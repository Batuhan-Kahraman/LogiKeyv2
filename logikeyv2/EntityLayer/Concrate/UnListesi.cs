using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
  
    public class UnListesi
    {
        [Key]
        public int Un_ID { get; set; }
        public string? Un_BakanlikKodu { get; set; }
        public string? Un_No { get; set; }
        public string? Un_Isim { get; set; }
        public string? Un_Sinif { get; set; }
        public string? Un_SiniflandirmaKodu { get; set; }
        public int Firma_ID { get; set; }
        public int EkleyenKullanici_ID { get; set; }
        public int DuzenleyenKullanici_ID { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime DuzenlemeTarihi { get; set; }
        public byte Durum { get; set; }
    }

}
