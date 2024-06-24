using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class YurtDisiTasimaEvraklar
    {
        [Key]
        public int ID { get; set; }
        [Required] 
        public int YurtDisiTasimaID { get; set; }
        [Required] 
        public int EvrakID { get; set; }
        public DateTime? EvraklarVerilisTarihi { get; set; }
        public DateTime? EvraklarGecerlilikTarihi { get; set; }
        
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
