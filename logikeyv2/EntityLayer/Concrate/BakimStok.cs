using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class BakimStok
    {
        public int ID { get; set; }
        public int BakimID { get; set; }
        //stok
        public int? StokID { get; set; }
        public int? Miktar { get; set; }
        public double? BirimFiyat { get; set; }
        public double? ToplamFiyat { get; set; }
        //hizmet
        public DateTime? Tarih { get; set; }
        public int? TedarikciID { get; set; }
        public string? HizmetAdi { get; set; }
        public string? FaturaNo { get; set; }
        public double? FiyatKdvHaric { get; set; }
        public double? KdvTutar { get; set; }
        public double? KdvliTutar { get; set; }


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
