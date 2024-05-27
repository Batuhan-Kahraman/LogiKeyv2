using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Migrations;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class TasimaController : BaseController
    {
        AracManager aracManager = new AracManager(new EFAracRepository());
        KullanicilarManager surucuManager = new KullanicilarManager(new EFKullanicilarRepository());
        TasinacakUrunManager tasinacakUrunManager = new TasinacakUrunManager(new EFTasinacakUrunRepository());
        TasimaManager tasimaManager = new TasimaManager(new EFTasimaRepository());
        UnListesiManager unListesiManager = new UnListesiManager(new EFUnListesiRepository());
        CariManager cariManager = new CariManager(new EFCariRepository());
        CariUcretlendirmeManager ucretlendirme = new CariUcretlendirmeManager(new EFCariUcretlendirmeRepository());
        AracTipManager aracTipManager = new AracTipManager(new EFAracTipRepository());
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        BirimlerManager birimlerManager = new BirimlerManager(new EFBirimlerRepository());
        TasimaTipiManager tasimaTipiManager = new TasimaTipiManager(new EFTasimaTipiRepository());
        AdresOzellikTanimlamaManager adresManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());

        public IActionResult Index()
        {
            return View();
           
        }
        public IActionResult TasimaEkle()
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Arac> aracListesi = aracManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<Kullanicilar> surucuListesi = surucuManager.GetAllList(x => x.Kullanici_Durum == 1 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<TasinacakUrun> tasinacakUrun = tasinacakUrunManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<UnListesi> UnListesi = unListesiManager.GetAllList(x => x.Durum == 1 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<Cari> CariListesi = cariManager.GetAllList(x => x.Durum == 1 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<CariUcretlendirme> Ucretlendirme = ucretlendirme.GetAllList(x => x.Durum == true && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<AracTip> aracTip = aracTipManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<AracTur> aracTur = aracTurManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<Birimler> birimler = birimlerManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<TasimaTipi> tasimaTipi = tasimaTipiManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));

            List<Arac> cekiciPlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID==1 && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<Arac> dorsePlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID==4 && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<AracTip> dorseListesi = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID==4 && (x.FirmaID == FirmaID || x.FirmaID == -2));


            List<Cari> AliciListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID==8 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));

            ViewBag.Araclar = aracListesi;
            ViewBag.CekiciPlaka = cekiciPlaka;
            ViewBag.DorsePlaka = dorsePlaka;
            ViewBag.DorseListe = dorseListesi;
            ViewBag.Suruculer = surucuListesi;
            ViewBag.TasinacakUrun = tasinacakUrun;
            ViewBag.UnListesi = UnListesi;
            ViewBag.CariListesi = CariListesi;
            ViewBag.aracTip = aracTip;
            ViewBag.aracTur = aracTur;
            ViewBag.birimler = birimler;
            ViewBag.tasimaTipi = tasimaTipi;
            ViewBag.AliciListesi = AliciListesi;
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;


            return View();
        }
        [HttpPost]
        public IActionResult TasimaEkle(Tasima tasima)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        tasima.AracTip_ID = 1;
                        tasima.Durum = 1;
                        tasima.EkleyenKullanici_ID = KullaniciID;
                        tasima.DuzenleyenKullanıcı_ID = KullaniciID;
                        tasima.Firma_ID = FirmaID;
                        tasima.OlusturmaTarihi = DateTime.UtcNow;
                        tasima.DuzenlemeTarihi = DateTime.UtcNow;
                        tasimaManager.TAdd(tasima);
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";
                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction("TasimaEkle", "Tasima");

        }
    }
}
