using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class NormalFatura
    {
        [Key]
        public int ID { get; set; }

        public string FaturaNo { get; set; }
        public int NormalTasimaID { get; set; }
        public int NormalTasimaDetayID { get; set; } 
        public int FaturaKesenID { get; set; } 
        public int FaturaKesilenID { get; set; } 
        public int NormalTasimaDetayUrunID { get; set; }  
        public int Odeme { get; set; }  


        [Required]
        public int FirmaID { get; set; }
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
