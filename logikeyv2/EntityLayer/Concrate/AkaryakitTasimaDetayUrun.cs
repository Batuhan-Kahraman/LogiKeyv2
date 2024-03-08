using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class AkaryakitTasimaDetayUrun
    {
        [Key]
        public int ID { get; set; }
       
        public int AkaryakitTasimaID { get; set; }
        public int AkaryakitTasimaDetayID { get; set; }
        public int UnID { get; set; }
        public int UrunID { get; set; }
        public int YuklemeMiktari { get; set; }
        public int OdemeYapanCariGrup { get; set; }
        public int OdemeYapanCariID { get; set; }
        public int Ucretlendirme { get; set; }
        public int BirimSeferFiyati { get; set; }



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
