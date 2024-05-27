using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class DuyuruController : BaseController
    {
        DuyuruManager duyuruManager = new DuyuruManager(new EFDuyuruRepository());
        BildirimManager bildirimManager = new BildirimManager(new EFBildirimRepository());

        public IActionResult Index()
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<DuyuruViewModel> viewModel = duyuruManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID ==-2))
               .Join(
               bildirimManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2)),
               duyuru => duyuru.BildirimID,
               bildirim => bildirim.ID,
               (duyuru, bildirim) => new DuyuruViewModel
               {
                   DuyuruID = duyuru.ID,
                   BildirimID = duyuru.BildirimID,
                   Icerik = duyuru.Icerik,
                   KategoriAdi  = bildirim.KategoriAdi,
                   FirmaID=duyuru.FirmaID
               }
               ).ToList();

            List<Bildirim> bildirimler = bildirimManager.GetAllList(x => x.Durum == true &&( x.FirmaID == FirmaID || x.FirmaID==-2));
            ViewBag.AracTur = bildirimler;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Ekle(IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
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
                        item.FirmaID = FirmaID;
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.OlusturanId = KullaniciID;
                        item.DuzenleyenID = KullaniciID;
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
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Duyuru item = duyuruManager.GetByID(int.Parse(form["ID"]));

                        item.BildirimID = int.Parse(form["BildirimID"]);
                        item.Icerik = form["Icerik"];
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
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
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Duyuru item = duyuruManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
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
