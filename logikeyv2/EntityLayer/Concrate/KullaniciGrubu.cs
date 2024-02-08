using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class KullaniciGrubu
    {
        [Key]
        public int KullaniciGrup_ID { get; set; }
        public string? KullaniciGrup_Adi { get; set; }
        public int Firma_ID { get; set; }
        public byte KullaniciGrup_Durum { get; set; }
        public int EkleyenKullanici_ID { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime DuzenlemeTarihi { get; set; }

        
    }

}
