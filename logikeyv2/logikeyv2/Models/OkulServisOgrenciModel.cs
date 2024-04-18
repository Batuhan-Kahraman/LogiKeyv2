using EntityLayer.Concrate;

namespace logikeyv2.Models
{
    public class OkulServisOgrenciModel
    {
        public OgrenciGuzergah? OgrenciGuzergah { get; set; }
        public Guzergah? Guzergah { get; set; }
        public Arac? Arac { get; set; }

        public List<Cari>? Ogrenci { get; set; }
    }
}
