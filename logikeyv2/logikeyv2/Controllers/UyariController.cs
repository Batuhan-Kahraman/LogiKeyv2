using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class UyariController : BaseController
    {
        UyariManager uyariManager = new UyariManager(new EFUyariRepository());

        Context context = new Context();

        public IActionResult Index()
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Uyari> uyari = uyariManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            return View(uyari);
        }

        public IActionResult Ekle()
        {
            ViewBag.Mesaj = TempData["Mesaj"];
            ViewBag.MesajTipi = TempData["MesajTipi"];
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Uyari uyari)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        uyari.FirmaID = FirmaID;
                        uyari.OlusturanId = KullaniciID;
                        uyari.DuzenleyenID = KullaniciID;
                        uyari.Durum = true;
                        uyari.OlusturmaTarihi = DateTime.Now;
                        uyari.DuzenlemeTarihi = DateTime.Now;
                        uyariManager.TAdd(uyari);
                        transaction.Commit();
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                    }
                }
            }
                        return RedirectToAction("Index");
        }

        public IActionResult Duzenle(int UyariID)
        {

            ViewBag.Mesaj = TempData["Mesaj"];
            ViewBag.MesajTipi = TempData["MesajTipi"];
            Uyari uyari = uyariManager.GetByID(UyariID);
            return View(uyari);
        }
        [HttpPost]
        public IActionResult Duzenle(Uyari uyari)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int uyariID = uyari.UyariID;
                        var item = uyariManager.GetByID(uyariID);

                        if (item != null)
                        {

                            item.SurucuID = uyari.SurucuID;
                            item.Aciklama= uyari.Aciklama;
                            item.AracID= uyari.AracID;
                            item.BaslangicTarihi = uyari.BaslangicTarihi;
                            item.BitisTarihi = uyari.BitisTarihi;
                            item.Fiyat= uyari.Fiyat;
                            item.GunSayisi= uyari.GunSayisi;
                            item.RaporGunSayisi = uyari.RaporGunSayisi;
                            item.UyariTarihi = uyari.UyariTarihi;
                            item.UyariTipID = uyari.UyariTipID;
                            item.UyariYapilsinMi = uyari.UyariYapilsinMi;
                            item.DuzenleyenID = KullaniciID;

                            item.DuzenlemeTarihi = DateTime.Now;

                            uyariManager.TUpdate(item);

                        }

                        transaction.Commit();
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        return View(uyari);
                    }
                }
            }
        }
        [HttpPost]
        public IActionResult Sil(IFormCollection form)
        {
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    Uyari item = uyariManager.GetByID(int.Parse(form["ID"]));
                    try
                    {
                        if (item != null)
                        {
                            item.Durum = false;

                            item.DuzenleyenID = KullaniciID;
                            item.DuzenlemeTarihi = DateTime.Now;
                            uyariManager.TUpdate(item);
                        }
                        transaction.Commit();
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        return RedirectToAction("Index");
                    }

                }
            }
        }


    }
}
