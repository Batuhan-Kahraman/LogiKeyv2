using EntityLayer.Concrate;

namespace logikeyv2.Models
{
    public class OkulOgrenciModel
    {
        public Kullanicilar Surucu { get; set; }
        public Cari Okul { get; set; }
        public Cari Ogrenci { get; set; }

        public OgrenciModulu OgrenciModulu { get; set; }

    }
}
