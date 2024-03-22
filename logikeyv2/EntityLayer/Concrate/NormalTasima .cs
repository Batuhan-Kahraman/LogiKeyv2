using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class NormalTasima
    {
        [Key]
        public int ID { get; set; }
        public int AracID { get; set; }
        public int TasimaTipiID { get; set; }
        public int AracTurID { get; set; }
        public int Kullanici1ID { get; set; }
        public int Kullanici2ID { get; set; }
        public int Kullanici3ID { get; set; }
        public int CekiciPlakaID { get; set; }
        public int DorseID { get; set; }
        public int DorsePlakaID { get; set; }
     
        public int ToplamYuklenenMiktar { get; set; }



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
