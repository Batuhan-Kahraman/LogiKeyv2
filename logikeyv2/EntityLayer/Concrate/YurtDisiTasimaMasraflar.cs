using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class YurtDisiTasimaMasraflar
    {
        [Key]
        public int ID { get; set; }
        [Required] 
        public int YurtDisiTasimaID { get; set; }
        public int YurtDisiTasimaDetayID { get; set; }
        [Required] 
        public int MasrafID { get; set; }
        public double? Fiyat { get; set; }
        public int? ParaBirimID { get; set; }
        public int? HedefKurID { get; set; }
        public double? HedefKurFiyat { get; set; }
        public DateTime MasrafTarihi { get; set; }

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
