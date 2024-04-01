using EntityLayer.Concrate;

namespace logikeyv2.Models
{
    public class OgrenciTahsilatViewModel
    {
        public OgrenciTahsilat ogrenciTahsilat { get; set; }
        public OgrenciTahsilatBilgileri ogrenciTahsilatBilgileri { get; set; }
        public Cari ogrenci { get; set; }
        public Cari? okul { get; set; }
    }
}
