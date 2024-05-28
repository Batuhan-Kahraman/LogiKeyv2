using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class OgrenciTahsilat
    {
        [Key]
        public int Id { get; set; }
        public int OgrenciId { get; set; }
        public DateTime AnlasmaTarihi { get; set; }
        public decimal AnlasilanTutar { get; set; }
        public int TaksitSayisi { get; set; }
        public DateTime VadeBaslangicTarihi { get; set; }
        public decimal KalanBorcTutar { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime DuzenlemeTarihi { get; set; }
        public bool Durum { get; set; }
        public int EkleyenKullaniciID { get; set; }
        public int FirmaID { get; set; }
        public int DuzenleyenKullaniciID { get; set; }

    }
}
