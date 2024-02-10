using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class MasrafCezaController : Controller
    {
        MasrafCezaManager MasrafCezaManager = new MasrafCezaManager(new EFMasrafCezaRepository());
        AracManager AracManager = new AracManager(new EFAracRepository());
        SurucuManager SurucuManager = new SurucuManager(new EFSurucuRepository());


        public IActionResult Index()
        {
            List<MasrafCeza> viewModel = MasrafCezaManager.GetAllList(x => x.Durum == true);
               

            return View(viewModel);
        }

        public IActionResult Ekle()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Ekle(MasrafCeza masrafCeza)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        MasrafCeza item = new MasrafCeza();
                        item.Durum = true;


                        item.FirmaID = 1;//değişçek
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.OlusturanId = 1;//değişcek
                        item.DuzenleyenID = 1;//değişcek
                        MasrafCezaManager.TAdd(item);
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

        public IActionResult Duzenle(int MasrafCezaID)
        {
            MasrafCeza masrafCeza = MasrafCezaManager.GetByID(MasrafCezaID);
            return View(masrafCeza);
        }

        [HttpPost]
        public IActionResult Duzenle(MasrafCeza masrafCeza)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        MasrafCeza item = MasrafCezaManager.GetByID(masrafCeza.ID);
                        

                        item.FirmaID = 1;//değişçek
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = 1;//değişcek
                        MasrafCezaManager.TUpdate(item);
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
                        MasrafCeza item = MasrafCezaManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        MasrafCezaManager.TUpdate(item);
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
