using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Migrations;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class TasimaController : Controller
    {
        AracManager aracManager = new AracManager(new EFAracRepository());
        SurucuManager surucuManager = new SurucuManager(new EFSurucuRepository());
        TasinacakUrunManager tasinacakUrunManager = new TasinacakUrunManager(new EFTasinacakUrunRepository());
        TasimaManager tasimaManager = new TasimaManager(new EFTasimaRepository());
        UnListesiManager unListesiManager = new UnListesiManager(new EFUnListesiRepository());
        CariManager cariManager = new CariManager(new EFCariRepository());
        CariUcretlendirmeManager ucretlendirme = new CariUcretlendirmeManager(new EFCariUcretlendirmeRepository());
        AracTipManager aracTipManager = new AracTipManager(new EFAracTipRepository());
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        BirimlerManager birimlerManager = new BirimlerManager(new EFBirimlerRepository());
        TasimaTipiManager tasimaTipiManager = new TasimaTipiManager(new EFTasimaTipiRepository());

        public IActionResult Index()
        {


            var combinedQuery = from tasima in tasimaManager.GetAllList(x => x.Durum == 1)
                                join arac in aracManager.GetAllList((y => y.Durum == true)) on tasima.Arac_ID equals arac.ID
                                join surucu1 in surucuManager.GetAllList((y => y.Durum == true)) on tasima.Surucu1_ID equals surucu1.ID
                                join tasinacakUrun in tasinacakUrunManager.GetAllList((y => y.Durum == true)) on tasima.TasinacakUrun_ID equals tasinacakUrun.ID
                                join cariodemeyapan in cariManager.GetAllList((y => y.Durum == 1)) on tasima.Cari_Odeme_Yapan_ID equals cariodemeyapan.Cari_ID
                                join carialici in cariManager.GetAllList((y => y.Durum == 1)) on tasima.AliciCari_ID equals carialici.Cari_ID
                                join carigonderici in cariManager.GetAllList((y => y.Durum == 1)) on tasima.GondericiCari_ID equals carigonderici.Cari_ID
                                select new TasimaModel { Tasima = tasima, Arac = arac, Surucu = surucu1, TasinacakUrun = tasinacakUrun, CariOdemeYapan = cariodemeyapan, CariAlici = carialici, CariGonderen = carigonderici };

            List<TasimaModel> combinedList = combinedQuery.ToList();
            return View(combinedList);
        }
        public IActionResult TasimaEkle()
        {
            List<Arac> aracListesi = aracManager.GetAllList(x => x.Durum == true);
            List<Surucu> surucuListesi = surucuManager.GetAllList(x => x.Durum == true);
            List<TasinacakUrun> tasinacakUrun = tasinacakUrunManager.GetAllList(x => x.Durum == true);
            List<UnListesi> UnListesi = unListesiManager.GetAllList(x => x.Durum == 1);
            List<Cari> CariListesi = cariManager.GetAllList(x => x.Durum == 1);
            List<CariUcretlendirme> Ucretlendirme = ucretlendirme.GetAllList(x => x.Durum == true);
            List<AracTip> aracTip = aracTipManager.GetAllList(x => x.Durum == true);
            List<AracTur> aracTur = aracTurManager.GetAllList(x => x.Durum == true);
            List<Birimler> birimler = birimlerManager.GetAllList(x => x.Durum == true);
            List<TasimaTipi> tasimaTipi = tasimaTipiManager.GetAllList(x => x.Durum == true);



            ViewBag.Araclar = aracListesi;
            ViewBag.Suruculer = surucuListesi;
            ViewBag.TasinacakUrun = tasinacakUrun;
            ViewBag.UnListesi = UnListesi;
            ViewBag.CariListesi = CariListesi;
            ViewBag.aracTip = aracTip;
            ViewBag.aracTur = aracTur;
            ViewBag.birimler = birimler;
            ViewBag.tasimaTipi = tasimaTipi;



            return View();
        }
        [HttpPost]
        public IActionResult TasimaEkle(Tasima tasima)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        tasima.AracTip_ID = 1;
                        tasima.Durum = 1;
                        tasima.EkleyenKullanici_ID = 1;//değişçek
                        tasima.DuzenleyenKullanıcı_ID = 1;//değişçek
                        tasima.Firma_ID = 1;//değişçek
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
