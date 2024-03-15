using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Okul_Guzergah
    {
        [Key]
        public int OkulGuzergah_ID { get; set; }
        public int Okul_Sira { get; set; }
        public int OgrenciSiraNo { get; set; }
        public int Guzergah_ID { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime DuzenlemeTarihi { get; set; }
        public bool Durum { get; set; }
        public int EkleyenKullaniciID { get; set; }
        public int FirmaID { get; set; }
        public int DuzenleyenKullaniciID { get; set; }
    }
}
