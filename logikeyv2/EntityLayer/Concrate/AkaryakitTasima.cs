using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class AkaryakitTasima
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
        public int Goz1Kapasite { get; set; }
        public int Goz1UrunID { get; set; }
        public int Goz2Kapasite { get; set; }
        public int Goz2UrunID { get; set; }
        public int Goz3Kapasite { get; set; }
        public int Goz3UrunID { get; set; }
        public int Goz4Kapasite { get; set; }
        public int Goz4UrunID { get; set; }
        public int Goz5Kapasite { get; set; }
        public int Goz5UrunID { get; set; }
        public int Goz6Kapasite { get; set; }
        public int Goz6UrunID { get; set; }
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
