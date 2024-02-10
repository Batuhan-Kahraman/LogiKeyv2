using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class MasrafCeza
    {
        [Key]
        public int ID { get; set; }

        public int Tur { get; set; }//1:masraf 2:ceza


        public int? AracID {  get; set; }
        public int? SurucuID {  get; set; }
        public int? TedarikciID {  get; set; }//carideki tedarikçi grubu
        public DateTime? FaturaTarihi { get; set; }
        public string? FaturaNo { get; set; }
        public string? AracKM {  get; set; }
        public int? MasrafTipiID {  get; set; }
        public string? MasrafDetay {  get; set; }
        public bool? KdvDahilMi { get; set; }
        public int? Miktar { get; set; }   
        public float? BirimFiyat { get; set; }
        public float Tutar { get; set; }    
        public float? KDV { get; set; }
        public int? YakitTipiID { get; set; }
        public double? Iskonto { get; set; }
        public double? ToplamTutar { get; set; }
        public string? Notlar { get; set; }




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
