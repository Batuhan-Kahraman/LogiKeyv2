using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Windows.UI.Xaml.Controls;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class GuzergahController : BaseController
    { 
        GuzergahManager guzergahManager = new GuzergahManager(new EFGuzergahRepository());
        Okul_GuzergahManager Okul_GuzergahManager= new Okul_GuzergahManager(new EFOkul_GuzergahRepository());
        KullanicilarManager kullanicilarManager = new KullanicilarManager(new EFKullanicilarRepository());
        AracManager aracManager= new AracManager(new EFAracRepository());
        CariManager cariManager = new CariManager(new EFCariRepository());
        OgrenciModuluManager ogrenciModuluManager = new OgrenciModuluManager(new EFOgrenciModuluRepository());
        OgrenciGuzergahManager ogrenciGuzergahManager = new OgrenciGuzergahManager(new EFOgrenciGuzergahRepository());

        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            var combinedQuery = from guzergah in guzergahManager.GetAllList(x => x.Durum == true && x.FirmaID== FirmaID)
                                join arac in aracManager.GetAllList((y => y.Durum == true && y.FirmaID==FirmaID)) on guzergah.AracPlaka_ID equals arac.ID
                                join sofor in kullanicilarManager.GetAllList((y => y.Kullanici_Durum == 1 && y.Firma_ID == FirmaID)) on guzergah.SoforCari_ID equals sofor.Kullanici_ID
                                join hostes in kullanicilarManager.GetAllList((y => y.Kullanici_Durum == 1 && y.Firma_ID == FirmaID)) on guzergah.HostesCari_ID equals hostes.Kullanici_ID

                                select new OkulGuzergahModel { Guzergah = guzergah, Arac = arac, Sofor = sofor, Hostes = hostes };

            List<OkulGuzergahModel> combinedList = combinedQuery.ToList();
            return View(combinedList);
        }
        public IActionResult GuzergahEkle()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");

            List<Kullanicilar> servisSoforleri = kullanicilarManager.GetAllList((y => y.Kullanici_Durum == 1 && y.KullaniciGrup_ID == 5 && y.Firma_ID==FirmaID));
            ViewBag.Sofor = servisSoforleri;
            List<Arac> arac = aracManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID));
            ViewBag.Arac = arac;
            List<Kullanicilar> Hostes = kullanicilarManager.GetAllList((y => y.Kullanici_Durum == 1 && y.KullaniciGrup_ID ==4 && y.Firma_ID == FirmaID));
            ViewBag.Hostes = Hostes;
            List<Cari> okullar = cariManager.GetAllList((y => y.Durum == 1 && y.Cari_GrupID == 13 && y.Firma_ID==FirmaID));
            ViewBag.Okullar = okullar;
            return View();
        }
        [HttpPost]
        public IActionResult GuzergahEkle(Guzergah guzergah, string okullar, string ogrenciler, string gidisGunleri, string gidisSaatleri, string donusGunleri, string donusSaatleri, string ogrenciSiraNo)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        string[] okulDegerleri = okullar.Split(',');
                        string[] ogrenciDegerleri = ogrenciler.Split(',');
                        string[] siraNo= ogrenciSiraNo.Split(";");
                        string[] GidisGunleri = gidisGunleri.Split(";");
                        string[] GidisSaatleri = gidisSaatleri.Split(";");
                        string[] DonusGunleri = donusGunleri.Split(";");
                        string[] DonusSaatleri = donusSaatleri.Split(";");

                        var guzergahBul = guzergahManager.GetAllList(x => x.FirmaID == FirmaID && x.Guzergah_Adi == guzergah.Guzergah_Adi).FirstOrDefault();
                        if (guzergahBul == null)
                        {
                            guzergah.Durum = true;
                            guzergah.OlusturmaTarihi = DateTime.UtcNow;
                            guzergah.DuzenlemeTarihi = DateTime.UtcNow;
                            guzergah.FirmaID = FirmaID;
                            guzergah.DuzenleyenKullaniciID = KullaniciID;
                            guzergahManager.TAdd(guzergah);
                            var okulsirasi = 1;

                            var eklenenGuzergah = guzergahManager.GetAllList(x => x.FirmaID == FirmaID && x.Guzergah_Adi == guzergah.Guzergah_Adi).FirstOrDefault();

                            foreach (string okulDegeri in okulDegerleri)
                            {
                                Okul_Guzergah okul_Guzergah = new Okul_Guzergah();
                                int okulID = int.Parse(okulDegeri.Trim());
                                okul_Guzergah.Durum = true;
                                okul_Guzergah.OlusturmaTarihi = DateTime.UtcNow;
                                okul_Guzergah.DuzenlemeTarihi = DateTime.UtcNow;
                                okul_Guzergah.Okul_ID = okulID;
                                okul_Guzergah.DuzenleyenKullaniciID = KullaniciID;
                                okul_Guzergah.EkleyenKullaniciID = KullaniciID;
                                okul_Guzergah.Guzergah_ID = eklenenGuzergah.Guzergah_ID;
                                okul_Guzergah.FirmaID = FirmaID;
                                okul_Guzergah.Okul_Sira = okulsirasi;
                                Okul_GuzergahManager.TAdd(okul_Guzergah);
                                int dizi = 0;
                                
                                var okul_GuzergahID = Okul_GuzergahManager.GetAllList(x => x.FirmaID == FirmaID && x.Okul_ID == okulID && x.Okul_Sira == okulsirasi && x.EkleyenKullaniciID == KullaniciID).FirstOrDefault();
                                
                                foreach (string ogrenciID in ogrenciDegerleri)
                                {
                                    int ogrenciIDleri = int.Parse(ogrenciID.Trim());

                                    var ogrenci = ogrenciModuluManager.GetAllList(x => x.CariOkul_ID == okulID && okul_Guzergah.Okul_Sira == okulsirasi && x.CariOgrenci_ID == ogrenciIDleri).FirstOrDefault();


                                    if (ogrenci!=null)
                                    {
                                        OgrenciGuzergah ogrenciGuzergah = new OgrenciGuzergah();
                                        ogrenciGuzergah.Durum = true;
                                        ogrenciGuzergah.OlusturmaTarihi = DateTime.UtcNow;
                                        ogrenciGuzergah.DuzenlemeTarihi = DateTime.UtcNow;
                                        ogrenciGuzergah.FirmaID = FirmaID;
                                        ogrenciGuzergah.EkleyenKullaniciID = KullaniciID;
                                        ogrenciGuzergah.DuzenleyenKullaniciID = KullaniciID;
                                        ogrenciGuzergah.OkulGuzergah_ID = okul_GuzergahID.OkulGuzergah_ID;
                                        ogrenciGuzergah.OgrenciID = ogrenciIDleri;
                                        ogrenciGuzergah.Guzergah_ID = eklenenGuzergah.Guzergah_ID;
                                        ogrenciGuzergah.OkuldanDonusSaatleri = (DonusSaatleri[dizi]).ToString();
                                        ogrenciGuzergah.OkulaGidisGunleri = (GidisGunleri[dizi]).ToString();
                                        ogrenciGuzergah.OkulaGidisSaatleri = (GidisSaatleri[dizi]).ToString();
                                        ogrenciGuzergah.OkuldanDonusGunleri = (DonusGunleri[dizi]).ToString();
                                        ogrenciGuzergah.OgrenciSiraNo = (ogrenciSiraNo[dizi]).ToString();



                                        ogrenciGuzergahManager.TAdd(ogrenciGuzergah);
                                        dizi++;
                                    }
                                   
                                }
                                okulsirasi++;
                            }



                            TempData["Msg"] = "İşlem başarılı.";
                            TempData["Bgcolor"] = "green";
                        }
                        else
                        {
                            TempData["Msg"] = "İşlem başarısız. Bu güzergah adı daha önce kullanıldı.";
                            TempData["Bgcolor"] = "red";
                        }
                       
                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult GuzergahDuzenle(int ID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");

            List<Kullanicilar> servisSoforleri = kullanicilarManager.GetAllList((y => y.Kullanici_Durum == 1 && y.KullaniciGrup_ID == 5 && y.Firma_ID == FirmaID));
            ViewBag.Sofor = servisSoforleri;
            List<Arac> arac = aracManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID));
            ViewBag.Arac = arac;
            List<Kullanicilar> Hostes = kullanicilarManager.GetAllList((y => y.Kullanici_Durum == 1 && y.KullaniciGrup_ID == 4 && y.KullaniciGrup_ID == 5 && y.Firma_ID == FirmaID));
            ViewBag.Hostes = Hostes;
            List<Cari> okullar = cariManager.GetAllList((y => y.Durum == 1 && y.Cari_GrupID == 13 && y.Firma_ID == FirmaID));
            ViewBag.Okullar = okullar;
            Guzergah guzergah = guzergahManager.GetByID(ID);
            return View(guzergah);
        }
        [HttpPost]
        public IActionResult GuzergahDuzenle(Guzergah guzergah, string okullar, string ogrenciler, string gidisGunleri, string gidisSaatleri, string donusGunleri, string donusSaatleri, string ogrenciSiraNo)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        string[] okulDegerleri = okullar.Split(',');
                        string[] ogrenciDegerleri = ogrenciler.Split(',');
                        string[] siraNo = ogrenciSiraNo.Split(";");
                        string[] GidisGunleri = gidisGunleri.Split(";");
                        string[] GidisSaatleri = gidisSaatleri.Split(";");
                        string[] DonusGunleri = donusGunleri.Split(";");
                        string[] DonusSaatleri = donusSaatleri.Split(";");

                        Guzergah item = guzergahManager.GetByID(guzergah.Guzergah_ID);
                        item.SoforCari_ID = guzergah.SoforCari_ID;
                        item.AracPlaka_ID = guzergah.AracPlaka_ID;
                        item.DuzenlemeTarihi = DateTime.UtcNow;
                        item.DuzenleyenKullaniciID = KullaniciID;
                        item.Guzergah_Adi = guzergah.Guzergah_Adi;
                        item.HostesCari_ID = guzergah.HostesCari_ID;

                        guzergahManager.TUpdate(item);
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";
                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                    }
                }
            }
            return RedirectToAction("Index");
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
                        Guzergah guzergah = guzergahManager.GetByID(int.Parse(form["ID"]));
                        guzergah.Durum = false;
                        guzergah.DuzenlemeTarihi= DateTime.Now;
                        guzergah.EkleyenKullaniciID = KullaniciID;
                        guzergahManager.TUpdate(guzergah);
                        
                        
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

        [HttpGet]
        public IActionResult OgrenciListesi(int okulID)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            var ogrenciModulu = ogrenciModuluManager.GetAllList(x => x.CariOkul_ID == okulID && x.Durum==1);
            var ogrenciIDler = ogrenciModulu.Select(om => om.CariOgrenci_ID).ToList();
            List<Cari> ogrenciler = cariManager.GetAllList(y => y.Durum == 1 && y.Cari_GrupID == 14 && y.Firma_ID == FirmaID && ogrenciIDler.Contains(y.Cari_ID));
            return Json(ogrenciler);
        }


        public IActionResult OkulServisOgrencileri()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");

            var combinedQuery = (from Guzergah in guzergahManager.GetAllList(x => x.Durum == true && x.SoforCari_ID == KullaniciID)
                                 join OgrenciGuzergah in ogrenciGuzergahManager.GetAllList(y => y.Durum == true && y.FirmaID == FirmaID) on Guzergah.Guzergah_ID equals OgrenciGuzergah.Guzergah_ID
                                 join Ogrenci in cariManager.GetAllList(y => y.Durum == 1 && y.Cari_GrupID == 14 && y.Firma_ID == FirmaID) on OgrenciGuzergah.OgrenciID equals Ogrenci.Cari_ID
                                 join Ogrencimodulu in ogrenciModuluManager.GetAllList(x => x.Durum == 1) on Ogrenci.Cari_ID equals Ogrencimodulu.CariOgrenci_ID
                                 join okul in cariManager.GetAllList(y => y.Durum == 1 && y.Cari_GrupID == 13 && y.Firma_ID == FirmaID) on Ogrencimodulu.CariOkul_ID equals okul.Cari_ID
                                 select new OkulOgrenciModel { OgrenciModulu = Ogrencimodulu, Okul = okul, Ogrenci = Ogrenci })
                                .GroupBy(item => item.Ogrenci.Cari_ID)
                                .Select(group => group.First());






            List<OkulOgrenciModel> combinedList = combinedQuery.ToList();
            return View(combinedList);
        }

        public IActionResult OkulServisOgrenciCizelge(int OgrenciID)
        {



            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            var combinedQuery = from OgrenciGuzergah in ogrenciGuzergahManager.GetAllList(x => x.Durum == true && x.OgrenciID == OgrenciID)
                                join Ogrenci in cariManager.GetAllList((y => y.Durum == 1 && y.Firma_ID == FirmaID)) on OgrenciGuzergah.OgrenciID equals Ogrenci.Cari_ID

                                join Guzergah in guzergahManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID)) on OgrenciGuzergah.Guzergah_ID equals Guzergah.Guzergah_ID

                                join Arac in aracManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID)) on Guzergah.AracPlaka_ID equals Arac.ID
                                select new OkulServisOgrenciCizelgeModel { OgrenciGuzergah = OgrenciGuzergah, Guzergah = Guzergah, Arac = Arac, Ogrenci= Ogrenci };

            List<OkulServisOgrenciCizelgeModel> combinedList = combinedQuery.ToList();
            return View(combinedList);
        }

    }
}
