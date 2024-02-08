using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Moduller
    {
        [Key]
        public int Modul_ID { get; set; }
        public string? Modul_Adi { get; set; }
    }

}
