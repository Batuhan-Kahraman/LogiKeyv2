using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class TasinacakUrun
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Adi { get; set; }
        public int Un_ID { get; set; }

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
