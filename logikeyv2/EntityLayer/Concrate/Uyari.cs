using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Uyari
    {
        [Key]
        public int UyariID { get; set; }
        public int UyariTipID { get; set; }
        public int? SurucuID { get; set; }
        public int? AracID { get; set; }
        public string? Aciklama { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public int? GunSayisi { get; set; }
        public int? Fiyat { get; set; }
        public DateTime? UyariTarihi { get; set; }   
        public string? UyariYapilsinMi { get; set;}//Evet-Hayır
        public int? RaporGunSayisi { get; set;}


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
