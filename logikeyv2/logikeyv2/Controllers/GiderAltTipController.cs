using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class GiderAltTipController : BaseController
    {
        GiderAltTipManager GiderAltTipManager = new GiderAltTipManager(new EFGiderAltTipRepository());
        GiderTipManager GiderTipManager = new GiderTipManager(new EFGiderTipRepository());


        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<GiderTipiViewModel> viewModel = GiderTipManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID)
                .Join(
                GiderAltTipManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID),
                giderTip=>giderTip.ID,
                giderAltTip => giderAltTip.GiderTipID,
                (giderTip, giderAltTip) => new GiderTipiViewModel
                {
                    GiderTipID = giderTip.ID,
                    GiderAltTipID = giderAltTip.ID,
                    GiderTipAdi = giderTip.Adi,
                    GiderAltTipAdi = giderAltTip.Adi,
                }
                ).ToList();
            List<GiderTip> turListe = GiderTipManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            ViewBag.GiderTip = turListe;
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
                        GiderAltTip item = new GiderAltTip();
                        item.Durum = true;
                        item.Adi = form["Adi"];
                        item.GiderTipID = int.Parse(form["GiderTipID"]);
                        item.FirmaID = FirmaID;
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.OlusturanId = KullaniciID;
                        item.DuzenleyenID = KullaniciID;
                        GiderAltTipManager.TAdd(item);
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
                        GiderAltTip item = GiderAltTipManager.GetByID(int.Parse(form["ID"]));
                        item.Adi = form["Adi"];
                        item.GiderTipID = int.Parse(form["GiderTipID"]);
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        GiderAltTipManager.TUpdate(item);
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
                        GiderAltTip item = GiderAltTipManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.DuzenleyenID = KullaniciID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        GiderAltTipManager.TUpdate(item);
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
