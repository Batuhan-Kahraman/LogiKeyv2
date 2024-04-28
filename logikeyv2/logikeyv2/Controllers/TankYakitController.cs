using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Migrations;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class TankYakitController : BaseController
    {
        IstasyondanYakitVerManager istasyondanYakitVerManager = new IstasyondanYakitVerManager(new EFIstasyondanYakitVerRepository());
        TankaYakitEkleManager tankaYakitEkleManager = new TankaYakitEkleManager(new EFTankaYakitEkleRepository());
        TanktanYakitVerManager tanktanYakitVerManager = new TanktanYakitVerManager(new EFTanktanYakitVerRepository());
        TankManager tankManager = new TankManager(new EFTankRepository());
        AracManager aracManager = new AracManager(new EFAracRepository());
        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<IstasyondanYakitVer> istasyondanYakitVerList = istasyondanYakitVerManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            List<TankaYakitEkle> tankaYakitEkleList = tankaYakitEkleManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            List<TanktanYakitVer> tanktanYakitVerList = tanktanYakitVerManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            ViewBag.istasyondanYakitVerList = istasyondanYakitVerList;
            ViewBag.tankaYakitEkleList = tankaYakitEkleList;
            ViewBag.tanktanYakitVerList = tanktanYakitVerList;
            return View();
        }

        // istasyondan yakıt ver
        public IActionResult IstasyondanYakitVerEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult IstasyondanYakitVerEkle(IstasyondanYakitVer item)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        item.Durum = true;
                        item.FirmaID = FirmaID;
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.OlusturanId = KullaniciID;
                        item.DuzenleyenID = KullaniciID;
                        istasyondanYakitVerManager.TAdd(item);

                        Arac arac = aracManager.GetByID(item.AracID);
                        int? mevcutYakit = arac.MevcutYakit;


                        mevcutYakit += item.Miktar;
                        item.Miktar = item.Miktar;
                        arac.MevcutYakit = mevcutYakit;

                        arac.DuzenlemeTarihi = DateTime.Now;
                        arac.DuzenleyenID = KullaniciID;
                        aracManager.TUpdate(arac);

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

        public IActionResult IstasyondanYakitVerDuzenle(int ID)
        {
            IstasyondanYakitVer item = istasyondanYakitVerManager.GetByID(ID);
            return View(item);
        }
        [HttpPost]
        public IActionResult IstasyondanYakitVerDuzenle(IstasyondanYakitVer kayit)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        IstasyondanYakitVer item = istasyondanYakitVerManager.GetByID(kayit.ID);
                        Arac arac = aracManager.GetByID(item.AracID);
                        int? mevcutYakit = arac.MevcutYakit;


                        mevcutYakit -= item.Miktar;
                        item.Miktar = kayit.Miktar;
                        arac.MevcutYakit = mevcutYakit + kayit.Miktar;

                        arac.DuzenlemeTarihi = DateTime.Now;
                        arac.DuzenleyenID = KullaniciID;
                        aracManager.TUpdate(arac);


                        item.FaturaNo = kayit.FaturaNo;
                        item.Surucu1ID = kayit.Surucu1ID;
                        item.Surucu2ID = kayit.Surucu2ID;
                        item.AracID = kayit.AracID;
                        item.AracKm = kayit.AracKm;
                        item.LtFiyat = kayit.LtFiyat;
                        item.IstasyonID = kayit.IstasyonID;
                        item.LtFiyat = kayit.LtFiyat;
                        item.YakitAltTipID = kayit.YakitAltTipID;
                        item.YakitTipID = kayit.YakitTipID;
                        item.MuhasebeKod = kayit.MuhasebeKod;

                        item.Durum = true;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        istasyondanYakitVerManager.TUpdate(item);
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
        public IActionResult IstasyondanYakitVerSil(IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        IstasyondanYakitVer item = istasyondanYakitVerManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        istasyondanYakitVerManager.TUpdate(item);

                        Arac arac = aracManager.GetByID(item.AracID);
                        int? mevcutYakit = arac.MevcutYakit;


                        mevcutYakit -= item.Miktar;
                        arac.MevcutYakit = mevcutYakit;

                        arac.DuzenlemeTarihi = DateTime.Now;
                        arac.DuzenleyenID = KullaniciID;
                        aracManager.TUpdate(arac);

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



        // tanka yakit ekle
        public IActionResult TankaYakitEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TankaYakitEkle(TankaYakitEkle item)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        Tank tank = tankManager.GetByID(item.TankID);
                        int? mevcutYakit = tank.MevcutYakit;


                        mevcutYakit += item.Miktar;
                        if (mevcutYakit <= tank.Kapasite)
                        {
                            item.Miktar = item.Miktar;
                            tank.MevcutYakit = mevcutYakit;
                        }
                        else
                        {

                            item.Miktar = tank.Kapasite - tank.MevcutYakit ?? 0;
                            tank.MevcutYakit = tank.Kapasite;
                        }

                        item.Durum = true;
                        item.FirmaID = FirmaID;
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.OlusturanId = KullaniciID;
                        item.DuzenleyenID = KullaniciID;
                        tankaYakitEkleManager.TAdd(item);


                        tank.DuzenlemeTarihi = DateTime.Now;
                        tank.DuzenleyenID = KullaniciID;
                        tankManager.TUpdate(tank);
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

        public IActionResult TankaYakitDuzenle(int ID)
        {
            TankaYakitEkle item = tankaYakitEkleManager.GetByID(ID);
            return View(item);
        }
        [HttpPost]
        public IActionResult TankaYakitDuzenle(TankaYakitEkle kayit)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {


                        Tank tank = tankManager.GetByID(kayit.TankID);
                        int? mevcutYakit = tank.MevcutYakit;



                        TankaYakitEkle item = tankaYakitEkleManager.GetByID(kayit.ID);
                        mevcutYakit -= item.Miktar;

                        mevcutYakit += kayit.Miktar;
                        if (mevcutYakit <= tank.Kapasite)
                        {
                            item.Miktar = kayit.Miktar;
                            tank.MevcutYakit = mevcutYakit;
                        }
                        else
                        {

                            item.Miktar = tank.Kapasite - tank.MevcutYakit ?? 0;
                            tank.MevcutYakit = tank.Kapasite;
                        }


                        item.FaturaNo = kayit.FaturaNo;
                        item.Aciklama = kayit.Aciklama;
                        item.TankID = kayit.TankID;
                        item.TedarikciID = kayit.TedarikciID;
                        item.YakitAltTipID = kayit.YakitAltTipID;
                        item.YakitTipID = kayit.YakitTipID;
                        item.AracKm = kayit.AracKm;
                        item.LtFiyat = kayit.LtFiyat;
                        item.Durum = true;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        tankaYakitEkleManager.TUpdate(item);

                        tank.DuzenlemeTarihi = DateTime.Now;
                        tank.DuzenleyenID = KullaniciID;
                        tankManager.TUpdate(tank);

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
        public IActionResult TankaYakitSil(IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        TankaYakitEkle item = tankaYakitEkleManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        tankaYakitEkleManager.TUpdate(item);


                        Tank tank = tankManager.GetByID(item.TankID);
                        tank.MevcutYakit = tank.MevcutYakit - item.Miktar;
                        tank.DuzenlemeTarihi = DateTime.Now;
                        tank.DuzenleyenID = KullaniciID;
                        tankManager.TUpdate(tank);


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


        // tanktan yakit ver
        public IActionResult TanktanYakitVerEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TanktanYakitVerEkle(TanktanYakitVer item)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Arac arac = aracManager.GetByID(item.AracID);
                        int? aracMevutYakit = arac.MevcutYakit;

                        Tank tank = tankManager.GetByID(item.TankID);
                        int? tankMevcutYakit = tank.MevcutYakit;


                        tankMevcutYakit -= item.Miktar;
                        if (tankMevcutYakit >= 0)
                        {
                            item.Miktar = item.Miktar;
                            tank.MevcutYakit = tankMevcutYakit;
                        }
                        else
                        {

                            item.Miktar = tank.Kapasite ?? 0;
                            tank.MevcutYakit = 0;
                        }

                        item.Durum = true;
                        item.FirmaID = FirmaID;
                        item.OlusturmaTarihi = DateTime.Now;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.OlusturanId = KullaniciID;
                        item.DuzenleyenID = KullaniciID;
                        tanktanYakitVerManager.TAdd(item);

                        tank.DuzenlemeTarihi = DateTime.Now;
                        tank.DuzenleyenID = KullaniciID;
                        tankManager.TUpdate(tank);

                        aracMevutYakit += item.Miktar;
                        arac.MevcutYakit = aracMevutYakit;
                        arac.DuzenlemeTarihi = DateTime.Now;
                        arac.DuzenleyenID = KullaniciID;
                        aracManager.TUpdate(arac);

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

        public IActionResult TanktanYakitVerDuzenle(int ID)
        {
            TanktanYakitVer item = tanktanYakitVerManager.GetByID(ID);
            return View(item);
        }
        [HttpPost]
        public IActionResult TanktanYakitVerDuzenle(TanktanYakitVer kayit)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        TanktanYakitVer item = tanktanYakitVerManager.GetByID(kayit.ID);

                        Arac arac = aracManager.GetByID(item.AracID);
                        int? aracMevutYakit = arac.MevcutYakit;

                        aracMevutYakit -= item.Miktar;

                        Tank tank = tankManager.GetByID(item.TankID);
                        int? tankMevcutYakit = tank.MevcutYakit;

                        tankMevcutYakit += item.Miktar;

                        item.Miktar = kayit.Miktar;

                        tankMevcutYakit -= item.Miktar;
                        if (tankMevcutYakit >= 0)
                        {
                            item.Miktar = item.Miktar;
                            tank.MevcutYakit = tankMevcutYakit;
                        }
                        else
                        {
                            item.Miktar = tank.Kapasite ?? 0;
                            tank.MevcutYakit = 0;
                        }

                        

                        item.Aciklama = kayit.Aciklama;
                        item.TankID = kayit.TankID;
                        item.YakitAltTipID = kayit.YakitAltTipID;
                        item.YakitTipID = kayit.YakitTipID;
                        item.Surucu1ID = kayit.Surucu1ID;
                        item.Surucu2ID = kayit.Surucu2ID;
                        item.AracID = kayit.AracID;
                        item.AracKm = kayit.AracKm;
                        item.LtFiyat = kayit.LtFiyat;
                        item.FaturaNo = kayit.FaturaNo;

                        item.Durum = true;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        tanktanYakitVerManager.TUpdate(item);


                        tank.DuzenlemeTarihi = DateTime.Now;
                        tank.DuzenleyenID = KullaniciID;
                        tankManager.TUpdate(tank);



                        aracMevutYakit += item.Miktar;
                        arac.MevcutYakit = aracMevutYakit;
                        arac.DuzenlemeTarihi = DateTime.Now;
                        arac.DuzenleyenID = KullaniciID;
                        aracManager.TUpdate(arac);

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
        public IActionResult TanktanYakitVerSil(IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        TanktanYakitVer item = tanktanYakitVerManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        tanktanYakitVerManager.TUpdate(item);

                        Tank tank = tankManager.GetByID(item.TankID);
                        tank.MevcutYakit = tank.MevcutYakit + item.Miktar;
                        tank.DuzenlemeTarihi = DateTime.Now;
                        tank.DuzenleyenID = KullaniciID;
                        tankManager.TUpdate(tank);

                        Arac arac=aracManager.GetByID(item.AracID);
                        arac.MevcutYakit = arac.MevcutYakit - item.Miktar;
                        arac.DuzenlemeTarihi = DateTime.Now;
                        arac.DuzenleyenID = KullaniciID;
                        aracManager.TUpdate(arac);


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
