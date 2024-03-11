using EntityLayer.Concrate;

namespace logikeyv2.Models
{
    public class AkaryakitFaturaViewModel
    {
        public AkaryakitTasima Tasima { get; set; }
        public List<AkaryakitFatura> FaturaList { get; set; }
        public List<AkaryakitTasimaDetay> DetayList { get; set; }
        public List<AkaryakitTasimaDetayUrun> UrunList { get; set; }
        public List<Cari> FaturaKesenList { get; set; }
        public List<Cari> FaturaKesilenList { get; set; }
    }
}
