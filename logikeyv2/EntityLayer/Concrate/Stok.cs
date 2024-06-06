using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Stok
    {
        [Key]
        public int ID { get; set; }
        public int StokKategoriID { get; set; }

        public string? StokKodu { get; set; }
        public string? StokAdi { get; set; }
        public int Adet { get; set; }
        public int BirimFiyat { get; set; }
        public string? Aciklama { get; set; }
        public DateTime? Tarih { get; set; }
        public string? FaturaNo { get; set; }
        public int? TedarikciID {  get; set; }
        public int? GiderTipiID {  get; set; }


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
