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
        public int GondericiYuklemeIlID { get; set; }
        public int GondericiYuklemeIlceID { get; set; }
        public DateTime GondericiYuklemeTarihSaat { get; set; }
        public int AliciID { get; set; }
        public int AliciIndirilenIlID { get; set; }
        public int AliciIndirilenIlceID { get; set; }
        public DateTime AliciIndirilenTarihSaat { get; set; }
        public int NakliyeTutarKDVHaric { get; set; }
        public int NakliyeKDV { get; set; }
        public int NakliyeToplam { get; set; }
        public int NakliyeFiyat { get; set; }

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
