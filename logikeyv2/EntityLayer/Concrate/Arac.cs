using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    [Serializable]
    public class Arac
    {
        [Key]
        public int ID { get; set; }

        /*Sahiplik*/
        public string? Plaka { get; set; }
        public int? SahiplikID { get; set; }
        public int? GrupID {  get; set; }
        public int? AracTurID { get; set; }
        public int? AracTipID { get; set; }
        public string? BakimPeriyodu {  get; set; }
        public int? Goz1 {  get; set; }
        public int? Goz2 {  get; set; }
        public int? Goz3 {  get; set; }
        public int? Goz4 {  get; set; }
        public int? Goz5 {  get; set; }
        public int? Goz6 {  get; set; }
        public int? MarkaID { get; set; }
        public int? ModelID { get; set; }
        public string? Yil {  get; set; }
        public int? SirketID { get;set; } //Cariler
        public DateTime? BunyeGirisTarih { get; set; }
        public float? BunyeGirisFiyat { get; set; }
        public string? BunyeGirisNot { get; set; }
        public DateTime? BunyeCikisTarih { get; set; }
        public float? BunyeCikisFiyat { get; set; }
        public string? BunyeCikisNot { get; set; }
        public string? HGS { get; set; }
        public string? TTS { get; set; }
        public string? DepoSensorID { get; set; }
        public string? Ekipman { get; set; }
        public string? Notlar  { get;set; }

        /*Teknik Bilgi*/
        public string? MotorSeriNo { get; set; }
        public string? SaseNo { get; set; }
        public string? MotorGucu {  get; set; }  
        public int? YakitTipiID {  get; set; }
        public int? YakitDepo { get; set; }
        public int? YakitSarfiyat { get; set; } 
        public float? BosAgirlik {  get; set; }
        public float? AzamiYukluAgirlik { get; set; }
        public float? IstiapHaddi {  get; set; }
        public float? KasaEn { get; set; }
        public float? KasaBoy { get; set; }
        public float? KasaYukseklik { get; set; }
        public int? EuroPalet { get; set; }
        public string? LastikTip1 {  get; set; }
        public string? LastikTip2 {  get; set; }
        public string? LastikTip3 {  get; set; }
        public int? LastikTipAdet1 { get; set; }
        public int? LastikTipAdet2 { get; set; }
        public int? LastikTipAdet3 { get; set; }
        public int? AkuTipID { get; set; }
        public int? AkuAmper { get; set; }
        public int? AkuAdet { get;set; }
        public string? MotorYagTipi {  get; set; }
        public string? MotorYagListesi {  get; set; }
        public float? MotorYagPeriyot { get; set; }
        public string? HidrolikYagTipi {  get; set; }
        public string? HidrolikYagListesi {  get; set; }
        public float? HidrolikYagPeriyot {  get; set; }
        public string? GresYagTipi { get; set; }
        public float? GresYagKg { get; set; }
        public int? HavaFiltreKm { get; set; }
        public int? HavaFiltreAdet { get; set; }
        public int? MazotFiltre { get; set; }
        public int? MazotFiltreAdet { get; set; }
        public int? YagFiltre { get; set; }
        public int? YagFiltreAdet { get; set; }

        /*Ruhsat*/
        public string? RuhsatSahibi {  get; set; }
        public DateTime? TescilTarihi { get; set; }
        public string? TescilSiraNo { get; set; }
        public string? TescilSeriNo { get; set; }
        public DateTime? TrafigeCikisTarihi { get; set; }
        public int? VerildigiIlID { get; set; }
        public int? VerildigiIlceID { get; set; }
        public DateTime? RuhsatSonKullanma { get; set; }
        public string? MotorluAracSeriNo { get; set; }
        public string? TaksimetreSeriNo { get; set; }
        public string? TakografSeriNo { get; set; }

        /*Garanti*/
        public DateTime? GarantiBaslangicTarih { get; set; }
        public DateTime? GarantiBitisTarih { get; set; }
        public int? GarantiBaslangicKm { get; set; }
        public int? GarantiBitisKm { get; set; }
        public int? GarantiKmLimit { get; set; }
        

        /*Kiralama --> sahiplik=kiralama ise*/
        public DateTime? KiralamaTarihi { get; set; }
        public int? KiralamaSuresi { get; set; }
        public float? KiralamaTutari { get; set; }

        /*Taşeron --> sahiplik=taşeron ise*/
        public DateTime? IseBaslamaTarihi { get; set; }
        public int? IseBaslamaSuresi { get; set; }
        public string? IseBaslamaNot { get; set; }

        //belgeler
        public string? AracRuhsat { get; set; }
        public string? TrafikSigortasi { get; set; }
        public string? KaskoPolice { get; set; }
        public string? IsSozlesmesi { get; set; }
        public string? AraMuayene { get; set; }
        public string? MTV { get; set; }
        public string? K1YetkiBelge { get; set; }
        public string? K2YetkiBelge { get; set; }

        public int? KisiKapasite { get; set; }


        public int? SurucuID { get; set; }

        public int? MevcutYakit { get; set; }

        [Required]
        public int FirmaID { get; set; }
        public bool Durum { get; set; }
        [Required]
        public int? OlusturanId { get; set; }
        [Required]
        public DateTime OlusturmaTarihi { get; set; }
        [Required]
        public int? DuzenleyenID { get; set; }
        [Required]
        public DateTime DuzenlemeTarihi { get; set; }
    }
}
