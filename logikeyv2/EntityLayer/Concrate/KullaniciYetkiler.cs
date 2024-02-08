using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class KullaniciYetkiler
    {
        [Key]
        public int Yetki_ID { get; set; }
        public int FirmaModul_ID { get; set; }
        public int Izinler_ID { get; set; }
        public int KullaniciGruplari_ID { get; set; }
    }

}
