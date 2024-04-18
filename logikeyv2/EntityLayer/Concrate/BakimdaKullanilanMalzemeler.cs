using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class BakimdaKullanilanMalzemeler
    {
        public int ID { get; set; }
        public int BakimID {  get; set; }
        public int StokID { get; set; }
        public int Miktar { get;set; }
        public float Fiyat { get; set; } 
        public string? YapilmasiGereken { get; set; } 
        public int TedarikçiID { get; set; } 
        public string? FisNo {  get; set; }
        public int GiderTipID { get;set; }
        public int GiderAltTipID { get; set; }


        [Required]
        public int FirmaID { get; set; }
        [Required]
        public bool Durum { get; set; }
        [Required]
        public int OlusturanId { get; set; }
        [Required]
        public DateTime OlusturmaTarihi { get; set; }
        [Required]
        public int DuzenleyenID { get; set; }
        [Required]
        public DateTime DuzenlemeTarihi { get; set; }
    }
}
