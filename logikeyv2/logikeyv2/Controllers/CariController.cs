using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.EntityFrameworks;
using DocumentFormat.OpenXml.Office2010.Excel;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class CariController : Controller
    {
        CariManager cariManager = new CariManager(new EFCariRepository());
        CariGrupManager cariGrupManager = new CariGrupManager(new EFCariGrupRepository());
        AdresOzellikTanimlamaManager adresManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());

        AkaryakitTasimaManager akaryakitTasimaManager = new AkaryakitTasimaManager(new EFAkaryakitTasimaRepository());
        AkaryakitTasimaDetayManager akaryakitTasimaDetayManager = new AkaryakitTasimaDetayManager(new EFAkaryakitTasimaDetayRepository());
        AkaryakitTasimaDetayUrunManager akaryakitTasimaDetayUrunManager = new AkaryakitTasimaDetayUrunManager(new EFAkaryakitTasimaDetayUrunRepository());
        AkaryakitFaturaManager akaryakitFaturaManager = new AkaryakitFaturaManager(new EFAkaryakitFaturaRepository());
    

        public IActionResult Index()
        {
            List<Cari> liste = cariManager.GetAllList(x => x.Durum == 1);
           
            return View(liste);

        }
        public IActionResult Ekle()
        {
            var CariGrupList = cariGrupManager.GetAllList(x => x.Durum == 1 );
            ViewBag.CariGrup = CariGrupList;
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Cari cari)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        cari.Durum = 1;
                        cari.FaturaDurum = false;
                        cari.EkleyenKullanici_ID = 1;//değişçek
                        cari.DuzenleyenKullanici_ID = 1;//değişçek
                        cari.Firma_ID = 1;//değişçek
                        cari.Olusturma_Tarihi = DateTime.UtcNow;
                        cari.Duzenleme_Tarihi = DateTime.UtcNow;
                        cariManager.TAdd(cari);
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
            return RedirectToAction("Index");
        }


        public IActionResult CariHareket(int CariID)
        {

            List<AkaryakitFatura> faturaList = akaryakitFaturaManager.GetAllList(x => x.FaturaKesenID == CariID);
            

            ViewBag.Fatura = faturaList;
            ViewBag.CariID = CariID;


            return View();
        }
    }
}
