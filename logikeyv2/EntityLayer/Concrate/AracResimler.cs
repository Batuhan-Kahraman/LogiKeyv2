using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class AracResimler
    {
        [Key]
        public int ID { get; set; }
        public int AracID { get; set; }
        public string resim { get; set; }
    }
}
