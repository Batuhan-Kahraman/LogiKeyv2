using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Surucu
    {
        [Key]
        public int ID { get; set; }
        
        public int? SurucuPozisyonID {  get; set; }
        public string? Sifre { get; set; }

        public string? Isim { get; set; }
        public string? Soyisim { get; set; }
        public string TC { get; set; }
        public DateTime? GirisTarihi { get; set; }
        public DateTime? CikisTarihi { get; set; }
        public int? CikisNedeni { get; set; }
        public int? DurumID { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string? Eposta { get; set; }
        public string? KanGrubu { get; set; }
        public string? CepTelefonu { get; set; }
        public string? Adres { get; set; }
        public int? IlID { get; set; }
        public int? IlceID { get; set; }
        public string? Notlar { get; set; }
        public int? EhliyetSinifiID { get; set; }
        public DateTime? EhliyetVerilisTarihi { get; set; }
        public string? EhliyetVerilisYeri { get; set; }
        public DateTime? EhliyetSonaErisTarihi { get; set; }
        public string? EhliyetResmi { get; set; }
        public string? BankaAdi { get; set; }
        public string? BankaIban { get; set; }
        public DateTime? MeslekiYeterlilikGecTarih { get; set; }
        public string? PsikoTeknikKurumu { get; set; }
        public DateTime? PsikoTeknikGecTarih { get; set; }

        public DateTime? BGecerlilikTarih { get; set; }
        public DateTime? BEGecerlilikTarih { get; set; }
        public DateTime? CGecerlilikTarih { get; set; }
        public DateTime? C1GecerlilikTarih { get; set; }
        public DateTime? C1EGecerlilikTarih { get; set; }
        public DateTime? CEGecerlilikTarih { get; set; }
        public DateTime? D1GecerlilikTarih { get; set; }
        public DateTime? D1EGecerlilikTarih { get; set; }
        public DateTime? FGecerlilikTarih { get; set; }


        [Required]
        public int FirmaID { get; set; }
        public bool Durum { get; set; }
        [Required]
        public int OlusturanId { get;set; }
        [Required]
        public DateTime OlusturmaTarihi { get; set; }
        [Required]
        public int DuzenleyenID { get; set; }
        [Required]
        public DateTime DuzenlemeTarihi { get; set; }
    }
}
