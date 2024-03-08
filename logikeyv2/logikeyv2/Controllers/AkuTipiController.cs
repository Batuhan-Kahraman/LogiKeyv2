using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class AkuTipiController : Controller
    {
        AkuTipiManager AkuTipiManager = new AkuTipiManager(new EFAkaryakitTasimaRepository());


        public IActionResult Index()
        {
            List<AkuTipi> liste = AkuTipiManager.GetAllList(x => x.Durum == true);
            return View(liste);
        }

        [HttpPost]
        public IActionResult Ekle(IFormCollection form)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        AkuTipi item = new AkuTipi();
                        item.Durum = true;
                        item.Adi = form["Adi"];
                        item.FirmaID = 1;//değişçek
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.OlusturanId = 1;//değişcek
                        item.DuzenleyenID = 1;//değişcek
                        AkuTipiManager.TAdd(item);
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                        return RedirectToAction("Index");
                    }
                }
            }
        }

        [HttpPost]
        public IActionResult Duzenle(IFormCollection form)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        AkuTipi item = AkuTipiManager.GetByID(int.Parse(form["ID"]));
                        item.Adi = form["Adi"];
                        item.FirmaID = 1;//değişçek
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = 1;//değişcek
                        AkuTipiManager.TUpdate(item);
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                        return RedirectToAction("Index");
                    }
                }
            }
        }

        [HttpPost]
        public IActionResult Sil(IFormCollection form)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        AkuTipi item = AkuTipiManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        AkuTipiManager.TUpdate(item);
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                        return RedirectToAction("Index");
                    }
                }
            }

        }
    }
}
