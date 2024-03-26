using BusinessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class GuzergahController : Controller
    {
        GuzergahManager guzergahManager = new GuzergahManager(new EFGuzergahRepository());
        Okul_GuzergahManager Okul_GuzergahManager= new Okul_GuzergahManager(new EFOkul_GuzergahRepository());
        KullanicilarManager kullanicilarManager = new KullanicilarManager(new EFKullanicilarRepository());
        AracManager aracManager= new AracManager(new EFAracRepository());
        CariManager cariManager = new CariManager(new EFCariRepository());
        public IActionResult Index()
        {
            var combinedQuery = from guzergah in guzergahManager.GetAllList(x => x.Durum == true)
                                join arac in aracManager.GetAllList((y => y.Durum == true)) on guzergah.AracPlaka_ID equals arac.ID
                                join sofor in kullanicilarManager.GetAllList((y => y.Kullanici_Durum == 1)) on guzergah.SoforCari_ID equals sofor.Kullanici_ID
                                join hostes in kullanicilarManager.GetAllList((y => y.Kullanici_Durum == 1)) on guzergah.HostesCari_ID equals hostes.Kullanici_ID

                                select new OkulGuzergahModel { Guzergah = guzergah, Arac = arac, Sofor = sofor, Hostes = hostes };

            List<OkulGuzergahModel> combinedList = combinedQuery.ToList();
            return View(combinedList);
        }
        public IActionResult GuzergahEkle()
        {
           
            List<Kullanicilar> servisSoforleri = kullanicilarManager.GetAllList((y => y.Kullanici_Durum == 1 && y.KullaniciGrup_ID == 5));
            ViewBag.Sofor = servisSoforleri;
            List<Arac> arac = aracManager.GetAllList((y => y.Durum == true));
            ViewBag.Arac = arac;
            List<Kullanicilar> Hostes = kullanicilarManager.GetAllList((y => y.Kullanici_Durum == 1 && y.KullaniciGrup_ID ==4 ));
            ViewBag.Hostes = Hostes;
            List<Cari> okullar = cariManager.GetAllList((y => y.Durum == 1 && y.Cari_GrupID == 13));
            ViewBag.Okullar = okullar;
            return View();
        }
        [HttpGet]
        public IActionResult OgrenciListesi(int okulID)
        {

            List<Cari> liste = cariManager.GetAllList(y => y.Durum == 1 && y.Cari_GrupID == 13);
            return Json(liste);
        }
    }
}
