using EntityLayer.Concrate;

namespace logikeyv2.Models
{
    public class NormalTasimaModel
    {
        public Arac Arac { get; set; }
        public Kullanicilar Surucu { get; set; }
        public TasimaTipi TasimaTip { get; set; }
        public NormalTasima Tasima { get; set; }

        public AracTur AracTur { get; set; }

      
    }
}
