using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrate
{
    public class AracGelir
{
    [Key]
    public int aracGelir_ID { get; set; }

    public decimal Gelir { get; set; }

    public decimal Saatlik_Kiralama_Tutari { get; set; }

    public decimal Gunluk_Kiralama_Tutari { get; set; }

    public decimal Aylık_Kiralama_Tutarı { get; set; }

    public decimal Tutar_Periyot { get; set; }

    public int Arac_ID { get; set; }

    public int Firma_ID { get; set; }

    public int EkleyenKullanici_ID { get; set; }

    public int DuzenleyenKullanıcı_ID { get; set; }

    public DateTime OlusturmaTarihi { get; set; }

    public DateTime DuzenlemeTarihi { get; set; }

    public byte Durum { get; set; }
}
}
