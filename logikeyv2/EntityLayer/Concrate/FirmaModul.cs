using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class FirmaModul
    {
        [Key]
        public int FirmaModul_ID { get; set; }
        public int Firma_ID { get; set; }
        public int Modul_ID { get; set; }
    }

}
