using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class DuyuruController : Controller
    {
        DuyuruManager duyuruManager = new DuyuruManager(new EFDuyuruRepository());
        BildirimManager bildirimManager = new BildirimManager(new EFBildirimRepository());

        public IActionResult Index()
        {

            List<DuyuruViewModel> viewModel = duyuruManager.GetAllList(x => x.Durum == true)
               .Join(
               bildirimManager.GetAllList(x => x.Durum == true),
               duyuru => duyuru.BildirimID,
               bildirim => bildirim.ID,
               (duyuru, bildirim) => new DuyuruViewModel
               {
                   DuyuruID = duyuru.ID,
                   BildirimID = duyuru.BildirimID,
                   Icerik = duyuru.Icerik,
                   KategoriAdi  = bildirim.KategoriAdi,
               }
               ).ToList();

            List<Bildirim> bildirimler = bildirimManager.GetAllList(x => x.Durum == true);
            ViewBag.AracTur = bildirimler;
            return View(viewModel);
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
                        Duyuru item = new Duyuru();
                        item.Durum = true;
                        item.BildirimID =int.Parse(form["BildirimID"]);
                        item.Icerik = form["Icerik"];
                        item.FirmaID = 1;//değişçek
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.OlusturanId = 1;//değişcek
                        item.DuzenleyenID = 1;//değişcek
                        duyuruManager.TAdd(item);
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
                        Duyuru item = duyuruManager.GetByID(int.Parse(form["ID"]));

                        item.BildirimID = int.Parse(form["BildirimID"]);
                        item.Icerik = form["Icerik"];
                        item.FirmaID = 1;//değişçek
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = 1;//değişcek
                        duyuruManager.TUpdate(item);
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
                        Duyuru item = duyuruManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        duyuruManager.TUpdate(item);
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
