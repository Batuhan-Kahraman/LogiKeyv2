using EntityLayer.Concrate;

namespace logikeyv2.Models
{
    public class TasimaModel
    {
        public Arac Arac { get; set; }
        public Surucu Surucu { get; set; }
        public TasinacakUrun TasinacakUrun { get; set; }
        public Tasima Tasima { get; set; }

        public UnListesi UnListesi { get; set; }
        public Cari CariAlici { get; set; }
        public Cari CariGonderen { get; set; }
        public Cari CariOdemeYapan { get; set; }


        public CariUcretlendirme CariUcretlendirme { get; set; }

        public AracTip AracTip { get; set; }

      
    }
}
