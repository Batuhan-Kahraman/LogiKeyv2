using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class AdresOzellikTanimlama
    {
            [Key]
            public int Adres_ID { get; set; }
            public string? ILCE_KODU { get; set; }
            public string? IL_KODU { get; set; }
            public string? Il { get; set; }
            public string? Ilce { get; set; }
            public string? Posta_Kodu { get; set; }
        

    }
}
