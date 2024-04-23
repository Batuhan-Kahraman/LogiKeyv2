using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Istasyon
    {
        [Key]
        public int ID { get; set; }
        public string? Kod { get; set; }
        [Required] 
        public string Adi { get; set; }
        public int TedarikciID { get; set; }
        public int BolgeID { get; set; }
        public int IlID { get; set; }
        public int IlceID { get; set; }
        public string? Aciklama { get; set; }
        [Required]
        public int FirmaID { get; set; }
        [Required]
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
