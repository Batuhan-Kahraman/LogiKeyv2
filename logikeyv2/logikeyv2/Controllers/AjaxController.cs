using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using Tesseract;
using static System.Net.Mime.MediaTypeNames;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class AjaxController : Controller
    {
        #region tanimlamalar
        SahiplikManager sahiplikManager = new SahiplikManager(new EFSahiplikRepository());
        GrupManager grupManager = new GrupManager(new EFGrupRepository());
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        AracTipManager aracTipManager = new AracTipManager(new EFAracTipRepository());
        MarkaManager markaManager = new MarkaManager(new EFMarkaRepository());
        ModelManager modelManager = new ModelManager(new EFModelRepository());
        YakitTipiManager yakitTipiManager = new YakitTipiManager(new EFYakitTipiRepository());
        AkuTipiManager akuTipiManager = new AkuTipiManager(new EFAkuTipiRepository());
        SurucuPozisyonManager surucuPozisyonManager = new SurucuPozisyonManager(new EFSurucuPozisyonRepository());
        EhliyetSinifiManager ehliyetSinifiManager = new EhliyetSinifiManager(new EFEhliyetSinifiRepository());
        KullanicilarManager surucuManager = new KullanicilarManager(new EFKullanicilarRepository());
        KazaTuruManager kazaTuruManager = new KazaTuruManager(new EFKazaTuruRepository());
        AracManager aracManager = new AracManager(new EFAracRepository());
        MasrafTipiManager masrafTipiManager = new MasrafTipiManager(new EFMasrafTipiRepository());

        LastikTipiManager lastikTipiManager = new LastikTipiManager(new EFLastikTipiRepository());
        AdresOzellikTanimlamaManager adresManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());
        UnListesiManager unListesiManager = new UnListesiManager(new EFUnListesiRepository());
        TasinacakUrunManager tasinacakUrunManager = new TasinacakUrunManager(new EFTasinacakUrunRepository());
        AkaryakitTasimaManager  akaryakitTasimaManager = new AkaryakitTasimaManager(new EFAkaryakitTasimaRepository());
        NormalTasimaManager  normalTasimaManager = new NormalTasimaManager(new EFNormalTasimaRepository());
        #endregion
        CariManager cariManager = new CariManager(new EFCariRepository());
        OgrenciModuluManager ogrenciModuluManager = new OgrenciModuluManager(new EFOgrenciModuluRepository());
        [HttpGet]
        public IActionResult SahiplikListe()
        {
            List<Sahiplik> liste = sahiplikManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
       
        [HttpGet]
        public IActionResult GrupListe()
        {
            List<Grup> liste = grupManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AracTurListe()
        {
            List<AracTur> liste = aracTurManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AracTipListe(int AracTurID)
        {
            List<AracTip> liste = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID == AracTurID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult MarkaListe()
        {
            List<Marka> liste = markaManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult ModelListe(int MarkaID)
        {
            List<Model> liste = modelManager.GetAllList(x => x.Durum == true && x.MarkaID == MarkaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult YakitTipiListe()
        {
            List<YakitTipi> liste = yakitTipiManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AkuTipiListe()
        {
            List<AkuTipi> liste = akuTipiManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult SurucuPozisyonListe()
        {
            List<SurucuPozisyon> liste = surucuPozisyonManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult EhliyetSinifiListe()
        {
            List<EhliyetSinifi> liste = ehliyetSinifiManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult KazaTuruListe()
        {
            List<KazaTuru> liste = kazaTuruManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult SurucuListe()
        {
            List<Kullanicilar> liste = surucuManager.GetAllList(x => x.Kullanici_Durum == 1);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult PlakaListe()
        {
            List<Arac> liste = aracManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult MasrafTipiListe()
        {
            List<MasrafTipi> liste = masrafTipiManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult LastikTipListe()
        {
            List<LastikTipi> liste = lastikTipiManager.GetAllList(x => x.Durum == true);
            return Json(liste);
        }


        [HttpGet]
        public IActionResult IlceListe(string IlKodu)
        {
            List<AdresOzellikTanimlama> liste = adresManager.GetAllList(x => x.IL_KODU == IlKodu);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult GozSayisiBul(int AracID)
        {
            Arac liste = aracManager.GetAllList(x => x.ID == AracID).SingleOrDefault();
            return Json(liste);
        }
        [HttpGet]
        public IActionResult DorseListe()
        {
            List<Arac> liste = aracManager.GetAllList(x => x.AracTurID == 4);
            return Json(liste);
        }
        
        [HttpGet]
        public IActionResult UrunUnSec(int UrunID)
        {
            TasinacakUrun urun=tasinacakUrunManager.GetByID(UrunID);
            UnListesi liste = unListesiManager.GetAllList(x => x.Un_ID == urun.Un_ID).SingleOrDefault();
            return Json(liste);
        }

        [HttpGet]
        public IActionResult TasimaKontrol(int AracID)
        {
            AkaryakitTasima liste = akaryakitTasimaManager.GetAllList(x=>x.AracID==AracID && x.Durum==true).LastOrDefault();
            return Json(liste);
        }
        
        [HttpGet]
        public IActionResult NormalTasimaKontrol(int AracID)
        {
            NormalTasima liste = normalTasimaManager.GetAllList(x=>x.AracID==AracID && x.Durum==true).LastOrDefault();
            return Json(liste);
        }


    }

}
