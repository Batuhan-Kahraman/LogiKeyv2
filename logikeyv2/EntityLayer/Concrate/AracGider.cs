using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrate
{
    public class AracGider
{
    [Key]
    public int aracGider_ID { get; set; }

    public decimal Tutar_Periyot { get; set; }

    public decimal Gider { get; set; }

    public decimal Gider_Periyot { get; set; }

    public decimal Ek_Donanım { get; set; }

    public decimal Hasar { get; set; }

    public decimal Surucu_Ayligi { get; set; }

    public int Arac_ID { get; set; }

    public int Firma_ID { get; set; }

    public int EkleyenKullanici_ID { get; set; }

    public int DuzenleyenKullanıcı_ID { get; set; }

    public DateTime OlusturmaTarihi { get; set; }

    public DateTime DuzenlemeTarihi { get; set; }

    public byte Durum { get; set; }
}
}
