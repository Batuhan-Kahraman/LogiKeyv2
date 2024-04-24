using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class IstasyondanYakitVer
    {
        [Key]
        public int ID { get; set; }
        [Required] 
        public int IstasyonID { get; set; }
        public string? FaturaNo {  get; set; }
        public int YakitTipID { get; set; }
        public int Miktar { get; set; }
        public float LtFiyat { get; set; }
        public float Iskonto { get; set; }
        public float IskontoTutar { get; set; }
        public float KdvliTutar { get; set; }
        public float KdvsizTutar { get; set; }
        public int KDV { get; set; }
        public int AracID { get; set; }
        public int DorseID { get; set; }
        public int Surucu1ID { get; set; }
        public int Surucu2ID { get; set; }
        public string? AracKm { get; set; }

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
