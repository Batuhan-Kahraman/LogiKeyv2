using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class AracGelirGiderController : Controller
    {
        AracGelirManager aracGelirManager = new AracGelirManager(new EFAracGelirRepository());
        AracGiderManager aracGiderManager = new AracGiderManager(new EFAracGiderRepository());
        AracManager aracManager = new AracManager(new EFAracRepository());
        public IActionResult AracGelirListesi()
        {
            var combinedQuery = from aracgelir in aracGelirManager.GetAllList(x => x.Durum == 1)
                                join arac in aracManager.GetAllList((y => y.Durum == true)) on aracgelir.Arac_ID equals arac.ID
                                select new AracGelirGider { AracGelir = aracgelir, Arac = arac };

            List<AracGelirGider> combinedList = combinedQuery.ToList();
            
            return View(combinedList);
        }
        public IActionResult AracGiderListesi()
        {
            var combinedQuery1 = from aracgider in aracGiderManager.GetAllList(x => x.Durum == 1)
                                join arac in aracManager.GetAllList((y => y.Durum == true)) on aracgider.Arac_ID equals arac.ID
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
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        aracGelir.Durum = 1;
                        aracGelir.EkleyenKullanici_ID = 1;//değişçek
                        aracGelir.DuzenleyenKullanıcı_ID = 1;//değişçek
                        aracGelir.Firma_ID = 1;//değişçek
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
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        aracGider.Durum = 1;
                        aracGider.EkleyenKullanici_ID = 1;//değişçek
                        aracGider.DuzenleyenKullanıcı_ID = 1;//değişçek
                        aracGider.Firma_ID = 1;//değişçek
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
