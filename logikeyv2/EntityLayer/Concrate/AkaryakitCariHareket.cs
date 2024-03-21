using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class AkaryakitCariHareket
    {
        [Key]
        public int ID { get; set; }
        public int FaturaID { get; set; }
        public int OdemeID { get; set; }
        public int CariID { get; set; }
        public int PlakaID { get; set; }
        public int Tutar { get; set; }

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
