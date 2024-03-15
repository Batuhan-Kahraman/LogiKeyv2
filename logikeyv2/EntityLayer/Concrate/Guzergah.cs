using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Guzergah
    {
        [Key]
        public int Guzergah_ID { get; set; }
        public int AracPlaka_ID { get; set; }
        public int SoforCari_ID { get; set; }
        public int HostesCari_ID { get; set; }

        public string Guzergah_Adi { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime DuzenlemeTarihi { get; set; }
        public bool Durum { get; set; }
        public int EkleyenKullaniciID { get; set; }
        public int FirmaID { get; set; }
        public int DuzenleyenKullaniciID { get; set; }


    }

}
