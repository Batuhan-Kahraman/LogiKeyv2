using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class UnListesiController : Controller
    {
        UnListesiManager unListesiManager = new UnListesiManager(new EFUnListesiRepository());


        public IActionResult Index()
        {
            List<UnListesi> liste = unListesiManager.GetAllList(x => x.Durum == 1);
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
                        UnListesi item = new UnListesi();
                        item.Durum = 1;
                        item.Un_Isim = form["Un_Isim"];
                        item.Un_No = form["Un_No"];
                        item.Un_BakanlikKodu = form["Un_BakanlikKodu"];
                        item.Un_Sinif = form["Un_Sinif"];
                        item.Un_SiniflandirmaKodu = form["Un_SiniflandirmaKodu"];

                        item.Firma_ID= 1;//değişçek
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.EkleyenKullanici_ID=1;//değişcek
                        item.DuzenleyenKullanici_ID = 1;//değişcek
                        unListesiManager.TAdd(item);
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
                        UnListesi item = new UnListesi();
                        item.Durum = 1;
                        item.Un_Isim = form["Adi"];
                        item.Un_No = form["UnNo"];
                        item.Un_BakanlikKodu = form["UnBakanlikNo"];
                        item.Un_Sinif = form["UnSinif"];
                        item.Un_SiniflandirmaKodu = form["UnSiniflandirmaKodu"];

                        item.Firma_ID = 1;//değişçek
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.EkleyenKullanici_ID = 1;//değişcek
                        item.DuzenleyenKullanici_ID = 1;//değişcek
                        unListesiManager.TUpdate(item);
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
                        UnListesi item = unListesiManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = 0;
                        unListesiManager.TUpdate(item);
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
