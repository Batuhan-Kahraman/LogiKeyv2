using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Firma
    {

          [Key]
        public int Firma_ID { get; set; }
            public int FirmaModul_ID { get; set; }
            public int Firma_Tipi { get; set; }
            public string? Firma_Unvan { get; set; }
            public string? Firma_TCNO_VKNO { get; set; }
            public string? Firma_VergiDairesi { get; set; }
            public int Firma_AdresIL_ID { get; set; }
            public int Firma_AdresILCE_ID { get; set; }
            public string? Firma_Adres { get; set; }
            public string? Firma_WebSitesi { get; set; }
            public string? Firma_YetkiliAdi { get; set; }
            public string? Firma_YetkiliSoyadi { get; set; }
            public string? Firma_YetkiliEposta { get; set; }
            public string? Firma_CepTel { get; set; }
            public byte Firma_Durum { get; set; }
            public int EkleyenKullanici_ID { get; set; }
            public DateTime OlusturmaTarihi { get; set; }
            public DateTime DuzenlemeTarihi { get; set; }
 
    }
}
