using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class StokController : BaseController
    {
        StokManager StokManager = new StokManager(new EFStokRepository());
        StokKategoriManager StokKategoriManager = new StokKategoriManager(new EFStokKategoriRepository());
        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Stok> liste = StokManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<StokKategori> kat = StokKategoriManager.GetAllList(x=>x.Durum==true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            ViewBag.StokKategori = kat;

            return View(liste);
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
                        Stok item = new Stok();
                        item.Durum = true;
                        item.StokKategoriID = int.Parse(form["StokKategoriID"]);
                        item.StokAdi = form["StokAdi"];
                        item.StokKodu = form["StokKodu"];
                        item.Aciklama = form["Aciklama"];
                        item.Adet = int.Parse(form["Adet"]);
                        item.BirimFiyat = int.Parse(form["BirimFiyat"]);
                        item.Tarih = DateTime.Parse(form["Tarih"]);
                        item.FaturaNo = form["FaturaNo"];
                        /*item.GiderTipiID = int.Parse(form["GiderTipiID"]);
                        item.TedarikciID = int.Parse(form["TedarikciID"]);
                        */

                        if (form["TedarikciID"] != "" && !string.IsNullOrEmpty(form["TedarikciID"]))
                        {
                            item.TedarikciID = int.Parse(form["TedarikciID"]);
                        }
                        else
                        {
                            item.TedarikciID = 0;
                        }
                        
                        if (form["GiderTipiID"] != "" && !string.IsNullOrEmpty(form["GiderTipiID"]))
                        {
                            item.GiderTipiID = int.Parse(form["GiderTipiID"]);
                        }
                        else
                        {
                            item.GiderTipiID = 0;
                        }



                        item.FirmaID = FirmaID;
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.OlusturanId = KullaniciID;
                        item.DuzenleyenID = KullaniciID;
                        StokManager.TAdd(item);
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
        public IActionResult StokKategoriEkle(IFormCollection form)
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        StokKategori item = new StokKategori();
                        item.Durum = true;
                        item.StokKategoriAdi = form["StokKategoriAdi"];
                        item.FirmaID = FirmaID;
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.OlusturanId = KullaniciID;
                        item.DuzenleyenID = KullaniciID;
                        StokKategoriManager.TAdd(item);
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
                        Stok item = StokManager.GetByID(int.Parse(form["ID"])); 
                        item.StokKategoriID = int.Parse(form["StokKategoriID"]);
                        item.StokAdi = form["StokAdi"];
                        item.StokKodu = form["StokKodu"];
                        item.Adet = int.Parse(form["Adet"]);
                        item.BirimFiyat = int.Parse(form["BirimFiyat"]);
                        item.Aciklama = form["Aciklama"];

                        item.Tarih = DateTime.Parse(form["Tarih"]);
                        item.FaturaNo = form["FaturaNo"];
                        if (form["TedarikciID"] != "" && !string.IsNullOrEmpty(form["TedarikciID"]))
                        {
                            item.TedarikciID = int.Parse(form["TedarikciID"]);
                        }
                        else
                        {
                            item.TedarikciID = 0;
                        }

                        if (form["GiderTipiID"] != "" && !string.IsNullOrEmpty(form["GiderTipiID"]))
                        {
                            item.GiderTipiID = int.Parse(form["GiderTipiID"]);
                        }
                        else
                        {
                            item.GiderTipiID = 0;
                        }


                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        StokManager.TUpdate(item);
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
                        Stok item = StokManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        StokManager.TUpdate(item);
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
