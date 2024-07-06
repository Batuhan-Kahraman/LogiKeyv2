using EntityLayer.Concrate;

namespace logikeyv2.Models
{
    public class YurtDisiTasimaModel
    {
        public Arac Arac { get; set; }
        public Kullanicilar Surucu { get; set; }
        public TasimaTipi TasimaTip { get; set; }
        public YurtDisiTasima Tasima { get; set; }

        public AracTur AracTur { get; set; }

      
    }
}
