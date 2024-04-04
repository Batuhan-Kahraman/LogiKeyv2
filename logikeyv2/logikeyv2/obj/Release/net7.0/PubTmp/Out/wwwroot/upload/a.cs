using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class AracTur
    {
        [Key]
        public int AracTurID { get; set; }
        [Required]
        public int AracTipID { get; set; }
        [Required] public string AracTurAdi { get; set; }

        [Required]
        public bool Durum { get; set; }
        [Required]
        public int EkleyenKullanici { get;set; }
        [Required]
        public DateTime OlusturmaTarihi { get; set; }
        [Required]
        public DateTime DuzenlemeTarihi { get; set; }
    }
}
