using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class KullaniciGrupController : Controller
    {
        KullaniciGrubuManager KullaniciGrubuManager = new KullaniciGrubuManager(new EFKullaniciGrubuRepository());


        public IActionResult Index()
        {
            List<KullaniciGrubu> liste = KullaniciGrubuManager.GetAllList(x => x.KullaniciGrup_Durum == 1);
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
                        KullaniciGrubu item = new KullaniciGrubu();
                        item.KullaniciGrup_Durum = 1;
                        item.KullaniciGrup_Adi = form["Adi"];
                        item.Firma_ID = 1;//değişçek
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.EkleyenKullanici_ID = 1;//değişcek
                        KullaniciGrubuManager.TAdd(item);
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
                        KullaniciGrubu item = KullaniciGrubuManager.GetByID(int.Parse(form["ID"]));
                        item.KullaniciGrup_Adi = form["Adi"];
                        item.Firma_ID = 1;//değişçek
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.EkleyenKullanici_ID = 1;//değişcek
                        KullaniciGrubuManager.TUpdate(item);
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
                        KullaniciGrubu item = KullaniciGrubuManager.GetByID(int.Parse(form["ID"]));
                        item.KullaniciGrup_Durum = 0;
                        KullaniciGrubuManager.TUpdate(item);
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
