using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class YurtDisiTasimaDetay
    {
        [Key]
        public int ID { get; set; }
       
        public int YurtDisiTasimaID { get; set; }
        public int GondericiID { get; set; }
        public int YurtDisiTasimaTipiID { get; set; }
        public DateTime GumruklemeTarihi { get; set; }
        public int GondericiYuklemeUlke { get; set; }
        public string GondericiYuklemeIl { get; set; }
        public string GondericiYuklemeIlce { get; set; }
        public string GondericiYuklemeAcikAdres { get; set; }
        public DateTime GondericiYuklemeTarihSaat { get; set; }
        public int AliciID { get; set; }
        public int AliciIndirilenUlke { get; set; }
        public string AliciIndirilenIl { get; set; }
        public string AliciIndirilenIlce { get; set; }
        public string AliciIndirilenAcikAdres { get; set; }
        public DateTime AliciIndirilenTarihSaat { get; set; }
        public int NakliyeTutarKDVHaric { get; set; }
        public int NakliyeKDV { get; set; }
        public int NakliyeToplam { get; set; }
        public int NakliyeFiyat { get; set; }
        public int NakliyeKDVOrani { get; set; }
        public int NakliyeParaBirimiID { get; set; }

        [Required]
        public int FirmaID { get; set; }
        public bool Durum { get; set; }
        [Required]
        public int OlusturanId { get;set; }
        [Required]
        public DateTime OlusturmaTarihi { get; set; }
        [Required]
        public int DuzenleyenID { get; set; }
        [Required]
        public DateTime DuzenlemeTarihi { get; set; }
    }
}
