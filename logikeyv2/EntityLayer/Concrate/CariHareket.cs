using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class CariHareket
    {
        [Key]
        public int ID { get; set; }
        public int Kategori { get; set; }//1:fatura 2:ödeme
        public string TabloAdi { get; set; }//cari - araç plakası

        public int TabloID { get; set; }
        public int Tutar { get; set; }
        /*
         tablo adına göre hareketin hangi tabloya ait olduğu belli olacak.
        tablo id ile hangi kayıt ile ilgili olduğu belli olacak
         */


        [Required]
        public int FirmaID { get; set; }
        public bool Durum { get; set; }
        [Required]
        public int? OlusturanId { get; set; }
        [Required]
        public DateTime OlusturmaTarihi { get; set; }
        [Required]
        public int? DuzenleyenID { get; set; }
        [Required]
        public DateTime DuzenlemeTarihi { get; set; }
    }
}
