using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class OgrenciGuzergah
    {
        [Key]
        public int Guzergah_OgrenciID { get; set; }
        public int OgrenciID { get; set; }
        public int OgrenciSiraNo { get; set; }
        public int Guzergah_ID { get; set; }
        public int OkulGuzergah_ID { get; set; }
        public string OkulaGidisGunleri { get; set; }
        public string OkulaGidisSaatleri { get; set; }
        public string OkuldanDonusSaatleri { get; set; }
        public string OkuldanDonusGunleri { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime DuzenlemeTarihi { get; set; }
        public bool Durum { get; set; }
        public int EkleyenKullaniciID { get; set; }
        public int FirmaID { get; set; }
        public int DuzenleyenKullaniciID { get; set; }
    }
}

      