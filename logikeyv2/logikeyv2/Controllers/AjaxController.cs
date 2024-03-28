using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;
using Tesseract;
using static System.Net.Mime.MediaTypeNames;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class AjaxController : BaseController
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
        [HttpGet]
        public IActionResult SahiplikListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Sahiplik> liste = sahiplikManager.GetAllList(x => x.Durum == true && x.FirmaID== FirmaID && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult GrupListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Grup> liste = grupManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AracTurListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<AracTur> liste = aracTurManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AracTipListe(int AracTurID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<AracTip> liste = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID == AracTurID && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult MarkaListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Marka> liste = markaManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult ModelListe(int MarkaID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Model> liste = modelManager.GetAllList(x => x.Durum == true && x.MarkaID == MarkaID && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult YakitTipiListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<YakitTipi> liste = yakitTipiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult AkuTipiListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<AkuTipi> liste = akuTipiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult SurucuPozisyonListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<SurucuPozisyon> liste = surucuPozisyonManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult EhliyetSinifiListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<EhliyetSinifi> liste = ehliyetSinifiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult KazaTuruListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<KazaTuru> liste = kazaTuruManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult SurucuListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Kullanicilar> liste = surucuManager.GetAllList(x => x.Kullanici_Durum == 1 && x.KullaniciGrup_ID==2);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult PlakaListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Arac> liste = aracManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult MasrafTipiListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<MasrafTipi> liste = masrafTipiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult LastikTipListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<LastikTipi> liste = lastikTipiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return Json(liste);
        }


        [HttpGet]
        public IActionResult IlceListe(string IlKodu)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<AdresOzellikTanimlama> liste = adresManager.GetAllList(x => x.IL_KODU == IlKodu);
            return Json(liste);
        }
        [HttpGet]
        public IActionResult GozSayisiBul(int AracID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            Arac liste = aracManager.GetAllList(x => x.ID == AracID && x.FirmaID == FirmaID).SingleOrDefault();
            return Json(liste);
        }
        [HttpGet]
        public IActionResult DorseListe()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Arac> liste = aracManager.GetAllList(x => x.AracTurID == 4 && x.FirmaID == FirmaID);
            return Json(liste);
        }
        
        [HttpGet]
        public IActionResult UrunUnSec(int UrunID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            TasinacakUrun urun=tasinacakUrunManager.GetByID(UrunID);
            UnListesi liste = unListesiManager.GetAllList(x => x.Un_ID == urun.Un_ID && x.Firma_ID == FirmaID).SingleOrDefault();
            return Json(liste);
        }

        [HttpGet]
        public IActionResult AkaryakitTasimaKontrol(int AracID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            AkaryakitTasima liste = akaryakitTasimaManager.GetAllList(x=>x.AracID==AracID && x.Durum== true && x.FirmaID == FirmaID).LastOrDefault();
            return Json(liste);
        }
        
        [HttpGet]
        public IActionResult NormalTasimaKontrol(int AracID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            NormalTasima liste = normalTasimaManager.GetAllList(x=>x.AracID==AracID && x.Durum== true && x.FirmaID == FirmaID).LastOrDefault();
            return Json(liste);
        }


    }

}
