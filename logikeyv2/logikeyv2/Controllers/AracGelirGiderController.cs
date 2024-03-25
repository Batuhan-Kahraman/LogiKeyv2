using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class AracGelirGiderController : Controller
    {
        AracGelirManager aracGelirManager = new AracGelirManager(new EFAracGelirRepository());
        AracGiderManager aracGiderManager = new AracGiderManager(new EFAracGiderRepository());
        AracManager aracManager = new AracManager(new EFAracRepository());
        public IActionResult AracGelirListesi()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            var combinedQuery = from aracgelir in aracGelirManager.GetAllList(x => x.Durum == 1 && x.Firma_ID == FirmaID)
                                join arac in aracManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID)) on aracgelir.Arac_ID equals arac.ID
                                select new AracGelirGider { AracGelir = aracgelir, Arac = arac };

            List<AracGelirGider> combinedList = combinedQuery.ToList();
            
            return View(combinedList);
        }
        public IActionResult AracGiderListesi()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            var combinedQuery1 = from aracgider in aracGiderManager.GetAllList(x => x.Durum == 1 && x.Firma_ID == FirmaID)
                                join arac in aracManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID)) on aracgider.Arac_ID equals arac.ID
                                select new AracGelirGider { AracGider = aracgider, Arac = arac };

            List<AracGelirGider> combinedList = combinedQuery1.ToList();
            return View(combinedList);
        }
        public IActionResult AracGelirEkle()
        {
            return View();
        }
        public IActionResult AracGiderEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AracGelirEkle(AracGelir aracGelir)
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        aracGelir.Durum = 1;
                        aracGelir.EkleyenKullanici_ID = KullaniciID;
                        aracGelir.DuzenleyenKullanıcı_ID = KullaniciID;
                        aracGelir.Firma_ID = FirmaID;
                        aracGelir.OlusturmaTarihi = DateTime.UtcNow;
                        aracGelir.DuzenlemeTarihi = DateTime.UtcNow;
                        aracGelirManager.TAdd(aracGelir);
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
            return View(aracGelir);
        }
        [HttpPost]
        public IActionResult AracGiderEkle(AracGider aracGider)
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        aracGider.Durum = 1;
                        aracGider.EkleyenKullanici_ID = KullaniciID;
                        aracGider.DuzenleyenKullanıcı_ID = KullaniciID;
                        aracGider.Firma_ID = FirmaID;
                        aracGider.OlusturmaTarihi = DateTime.UtcNow;
                        aracGider.DuzenlemeTarihi = DateTime.UtcNow;
                        aracGiderManager.TAdd(aracGider);
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
            return View(aracGider);
        }
    }
}
