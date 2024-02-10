using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class CariGrupController : Controller
    {
        CariGrupManager CariGrupManager = new CariGrupManager(new EFCariGrupRepository());


        public IActionResult Index()
        {
            List<CariGrup> liste = CariGrupManager.GetAllList(x => x.Durum == 1);
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
                        CariGrup item = new CariGrup();
                        item.Durum = 1;
                        item.CariGrup_Adi = form["Adi"];
                        item.Firma_ID = 1;//değişçek
                        item.Olusturma_Tarihi = DateTime.Now;
                        item.Duzenleme_Tarihi = DateTime.Now;
                        item.EkleyenKullanici_ID = 1;//değişcek
                        CariGrupManager.TAdd(item);
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
                        CariGrup item = CariGrupManager.GetByID(int.Parse(form["ID"]));
                        item.CariGrup_Adi = form["Adi"];
                        item.Firma_ID = 1;//değişçek
                        item.Olusturma_Tarihi = DateTime.Now;
                        item.Duzenleme_Tarihi = DateTime.Now;
                        item.EkleyenKullanici_ID = 1;//değişcek
                        CariGrupManager.TUpdate(item);
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
                        CariGrup item = CariGrupManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = 0;
                        CariGrupManager.TUpdate(item);
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
