using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Kaza
    {
        [Key]
        public int ID { get; set; }
        public int? AracID { get; set; }
        public int? SurucuID { get; set; }
        public DateTime? KazaTarihi { get; set; }
        public int? KazaTuruID { get; set; }
        public string? KarsiPlaka {  get; set; }
        public string? KarsiSurucuTC { get; set; }
        public string? KarsiAracRuhsatSahibi {  get; set; }
        public string? KarsiAracSurucuAdiSoyadi {  get; set; }
        public string? KarsiAracCepTelefonu {  get; set; }
        public string? SurucumuzKusurOrani {  get; set; }
        public string? KarsiSurucuKusurOrani {  get; set; }
        public string? KazaRaporNo {  get; set; }
        public string? SigortaAcenteAdi {  get; set; }
        public string? SigortaAcenteYetkilisi {  get; set; }
        public string? SigortaAcenteTelefonu {  get; set; }
        public string? HasarFaturaFirmasi {  get; set; }
        public string? HasarFaturaTarihi {  get; set; }
        public DateTime? GeriOdemeTarihi {  get; set; }
        public float? HasarTutari {  get; set; }
        public float? SigortaninOdedigiTutar {  get; set; }
        public float? OdenemeyenTutar { get; set; }
        public string? ServisFirmaAdi {  get; set; }
        public string? ServisFirmaYetkilisi {  get; set; }
        public string? ServisFirmaTelefonu {  get; set; }
        public string? ServisFirmaAdres {  get; set; }
        public string? Notlar {  get; set; }
        public string? KazaRaporu { get; set; }//resim
        public string? KarsiTarafTrafikSigortasi { get; set; }//resim
        public string? KarsiTarafKaskoPolicesi { get; set; }//resim
        public string? KarsiTarafRuhsat { get; set; }//resim
        public string? KarsiTarafEhliyet { get; set; }//resim
        public string? KazaResimleri { get; set; }//resimler 5 adet



        [Required]
        public int FirmaID { get; set; }
        [Required]
        public bool Durum { get; set; }
        [Required]
        public int OlusturanId { get; set; }
        [Required]
        public DateTime OlusturmaTarihi { get; set; }
        [Required]
        public int DuzenleyenID { get; set; }
        [Required]
        public DateTime DuzenlemeTarihi { get; set; }
    }
}
