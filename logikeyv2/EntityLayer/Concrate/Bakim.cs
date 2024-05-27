using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Bakim
    {
        public int ID { get; set; }
        public int CekiciID {  get; set; }
        public int DorseID { get; set; }
        public string? AracKm { get;set; }
        public int SurucuID {  get; set; }
        public string? ArizaNedeni { get; set; } 
        public string? YapilmasiGereken { get; set; } 
        public DateTime TarihSaat { get; set; }
        public int ServisBakimDurumID {  get; set; }
        public int ServisBakimTurID { get;set; }
        public string BakimYeri { get; set; }
        public int? PersonelID { get; set; } 
        public string? Aciklama { get; set; }

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
