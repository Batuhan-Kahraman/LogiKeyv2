using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class CariOdemeHareketleriController : BaseController
    {
        CariHareketManager cariHareketManager = new CariHareketManager(new EFCariHareketRepository());
        CariManager cariManager = new CariManager(new EFCariRepository());
        public IActionResult Index(int CariID=0)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<CariHareket> list;
            if (CariID==0)
                list = cariHareketManager.GetAllList(x=>x.Durum==true && x.FirmaID==FirmaID);
            else
                list= cariHareketManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.CariID==CariID);
            ViewBag.CariID = CariID;
            return View(list);
        }
        
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(CariHareket cariHareket)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        cariHareket.Durum = true;
                        cariHareket.FirmaID = FirmaID;
                        cariHareket.OlusturmaTarihi = DateTime.Now;
                        cariHareket.DuzenlemeTarihi = DateTime.Now;
                        cariHareket.OlusturanId = KullaniciID;
                        cariHareket.DuzenleyenID = KullaniciID;
                        cariHareketManager.TAdd(cariHareket);


                        Helper.CariBorcAlacakKalanHesapla(cariHareket.CariID);

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


        public IActionResult Duzenle(int ID)
        {
            CariHareket cariHareket = cariHareketManager.GetByID(ID);
            return View(cariHareket);
        }
        [HttpPost]
        public IActionResult Duzenle(CariHareket cariHareket)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        CariHareket item = cariHareketManager.GetByID(cariHareket.ID);
                        item.AkaryakitFaturaID = cariHareket.AkaryakitFaturaID;
                        item.AkaryakitFaturaTutar = cariHareket.AkaryakitFaturaTutar;
                        item.HavaleTutar = cariHareket.HavaleTutar;
                        item.HavaleBankaID = cariHareket.HavaleBankaID;
                        item.HavaleTarih = cariHareket.HavaleTarih;
                        item.CekSeriNo = cariHareket.CekSeriNo;
                        item.CekTutar = cariHareket.CekTutar;
                        item.CekVadeTarihi = cariHareket.CekVadeTarihi;
                        item.SenetTarihi = cariHareket.SenetTarihi;
                        item.SenetTutar = cariHareket.SenetTutar;
                        item.KrediKartNo = cariHareket.KrediKartNo;
                        item.KrediTutar = cariHareket.KrediTutar;
                        item.CariID = cariHareket.CariID;
                        item.Durum = true;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        cariHareketManager.TUpdate(item);



                        Helper.CariBorcAlacakKalanHesapla(cariHareket.CariID);
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
                        CariHareket item=cariHareketManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        cariHareketManager.TUpdate(item);


                        Helper.CariBorcAlacakKalanHesapla(item.CariID);

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
