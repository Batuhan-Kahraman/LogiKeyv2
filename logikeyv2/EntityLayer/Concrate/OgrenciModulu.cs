using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class OgrenciModulu
    {
        public int ID { get; set; }
        public int CariOkul_ID { get; set; }
        public int CariOgrenci_ID { get; set; }

        public DateTime KayitBaslangicTarihi { get; set; }
        public DateTime KayitBitisTarihi { get; set; }
        public byte Servis_Kullanim_Durumu { get; set; }
        //public int Sofer_ID { get; set; }
        public string? Veli1Adi { get; set; }
        public string? Veli1Soyadi { get; set; }
        public string? Veli1TelefonNo { get; set; }
        public string? Veli2Adi { get; set; }
        public string? Veli2Soyadi { get; set; }
        public string? Veli2TelefonNo { get; set; }
        public string? Veli3Adi { get; set; }
        public string? Veli3Soyadi { get; set; }
        public string? Veli3TelefonNo { get; set; }
        public byte Durum { get; set; }
        

    }
}
