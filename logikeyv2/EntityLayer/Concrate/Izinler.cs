using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Izinler
    {
        [Key]
        public int Izin_ID { get; set; }
        public string? Izin_Adi { get; set; }
        public byte Izin_Ekleme_Sil { get; set; }
        public byte Izin_Listeleme { get; set; }
        public byte Izin_Guncelleme { get; set; }
        public byte Durum { get; set; }
        public int EkleyenKullanici_ID { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime DuzenlemeTarihi { get; set; }

      
    }

}
