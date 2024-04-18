using EntityLayer.Concrate;

namespace logikeyv2.Models
{
    public class OkulServisOgrenciCizelgeModel
    {
        public OgrenciGuzergah? OgrenciGuzergah { get; set; }
        public Guzergah? Guzergah { get; set; }
        public Arac? Arac { get; set; }

        public Cari? Ogrenci { get; set; }
    }
}
