using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Drawing.Charts;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class UyariTipController : BaseController
    {
        UyariTipManager uyariTipManager = new UyariTipManager(new EFUyariTipRepository());

        Context context = new Context();

        public IActionResult Index()
        {

            ViewBag.Mesaj=TempData["Mesaj"];
            ViewBag.MesajTipi = TempData["MesajTipi"];

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<UyariTip> uyariTip = uyariTipManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            return View(uyariTip);
        }


        [HttpPost]
        public IActionResult Ekle(UyariTip uyariTip)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        uyariTip.FirmaID = FirmaID;
                        uyariTip.OlusturanId = KullaniciID;
                        uyariTip.DuzenleyenID = KullaniciID;
                        uyariTip.Durum = true;
                        uyariTip.OlusturmaTarihi = DateTime.Now;
                        uyariTip.DuzenlemeTarihi = DateTime.Now;
                        uyariTipManager.TAdd(uyariTip);
                        transaction.Commit();
                        TempData["Mesaj"] = "Uyarı Tipi Eklendi.";
                        TempData["MesajTipi"] = "success";
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        transaction.Rollback();
                        TempData["Mesaj"] = "Uyarı Tipi Eklenemedi.";
                        TempData["MesajTipi"] = "warning";
                        return View(uyariTip);
                    }
                }
            }
        }


        [HttpPost]
        public IActionResult Duzenle(UyariTip uyariTip)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int uyariTipID = uyariTip.UyariTipID;
                        var item = uyariTipManager.GetByID(uyariTipID);

                        if (item != null)
                        {
                            uyariTip.DuzenleyenID = KullaniciID;
                            item.Ad=uyariTip.Ad;
                            
                            item.DuzenlemeTarihi = DateTime.Now;

                            uyariTipManager.TUpdate(item);

                        }

                        transaction.Commit();
                        TempData["Mesaj"] = "Uyarı Tipi Düzenlendi.";
                        TempData["MesajTipi"] = "success";
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        transaction.Rollback();
                        TempData["Mesaj"] = "Uyarı Tipi Düzenlenemedi.";
                        TempData["MesajTipi"] = "warning";
                        return View(uyariTip);
                    }
                }
            }
        }

        public IActionResult Sil(int UyariTipID)
        {
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var item = uyariTipManager.GetByID(UyariTipID);
                    try
                    {
                        if (item != null)
                        {
                            item.Durum = false;
                            item.DuzenleyenID = KullaniciID;
                            item.DuzenlemeTarihi = DateTime.Now;
                            uyariTipManager.TUpdate(item);
                        }
                        transaction.Commit();
                        TempData["Mesaj"] = "Uyarı Tipi Silindi.";
                        TempData["MesajTipi"] = "success";
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        transaction.Rollback();
                        TempData["Mesaj"] = "Uyarı Tipi Silinemedi.";
                        TempData["MesajTipi"] = "warning";
                        return RedirectToAction("Index");
                    }

                }
            }
        }


    }
}
