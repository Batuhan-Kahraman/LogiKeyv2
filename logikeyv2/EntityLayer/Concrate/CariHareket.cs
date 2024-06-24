using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class CariHareket
    {
        [Key]
        public int ID { get; set; }
        public int CariID { get; set; }
        public int? AkaryakitFaturaID { get; set; }
        public double? AkaryakitFaturaTutar { get; set; }
        public int? NormalFaturaID { get; set; }
        public double? NormalFaturaTutar { get; set; }
        public int? YurtDisiFaturaID { get; set; }
        public double? YurtDisiFaturaTutar { get; set; }
        public string? OdemeYontemi { get; set; }
        public string? KrediKartNo { get; set; }
        public double? KrediTutar { get; set; }
        public DateTime? KrediTarih { get; set; }
        public string? CekSeriNo { get; set; }
        public DateTime? CekVadeTarihi { get; set; }
        public double? CekTutar { get; set; }
        public DateTime? SenetTarihi { get; set; }
        public double? SenetTutar { get; set; }
        public int? HavaleBankaID { get; set; }
        public DateTime? HavaleTarih { get; set; }
        public double? HavaleTutar { get; set; }

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
