using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class SurucuController : BaseController
    {
        KullanicilarManager surucuManager = new KullanicilarManager(new EFKullanicilarRepository());
        EhliyetSinifiManager ehliyetSinifiManager = new EhliyetSinifiManager(new EFEhliyetSinifiRepository());
        SurucuPozisyonManager surucuPozisyonManager = new SurucuPozisyonManager(new EFSurucuPozisyonRepository());
        KullaniciGrubuManager kullaniciGrubuManager = new KullaniciGrubuManager(new EFKullaniciGrubuRepository());
       AdresOzellikTanimlamaManager adresOzellikTanimlamaManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());
        public IActionResult Index()
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<SurucuViewModel> viewModel = surucuManager.GetAllList(x => x.Kullanici_Durum == 1 && x.Firma_ID == FirmaID)
                .GroupJoin(ehliyetSinifiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID),
                    surucu => surucu.EhliyetSinifiID,
                    ehliyetSinifi => ehliyetSinifi.ID,
                    (surucu, ehliyetSinifiGroup) => new { surucu, ehliyetSinifiGroup })
                .SelectMany(
                    result => result.ehliyetSinifiGroup.DefaultIfEmpty(),
                    (result, ehliyetSinifi) => new { result.surucu, ehliyetSinifi }
            )
            .GroupJoin(
                    surucuPozisyonManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID),
                    result => result.surucu.SurucuPozisyonID,
                    surucuPozisyon => surucuPozisyon.ID,
                    (result, surucuPozisyonGroup) => new { result.surucu, result.ehliyetSinifi, surucuPozisyonGroup }
                )
                .SelectMany(
                    result => result.surucuPozisyonGroup.DefaultIfEmpty(),

        (result, surucuPozisyon) => new SurucuViewModel
        {
            Surucu = result.surucu,
            EhliyetSinifi = result.ehliyetSinifi != null ? result.ehliyetSinifi.Adi : "",
            Pozisyon = surucuPozisyon != null ? surucuPozisyon.Adi : "",
        }
    )
    .ToList();

            return View(viewModel);
        }

        public IActionResult Ekle()
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<KullaniciGrubu> KullaniciGrubu = kullaniciGrubuManager.GetAllList((y => y.KullaniciGrup_Durum == 1 && y.Firma_ID == FirmaID));
            ViewBag.KullaniciGrubu = KullaniciGrubu;
            List<SurucuPozisyon> SurucuPozisyon = surucuPozisyonManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID));
            ViewBag.SurucuPozisyon = SurucuPozisyon;
            List<EhliyetSinifi> ehliyetSinifi = ehliyetSinifiManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID));
            ViewBag.EhliyetSinifi = ehliyetSinifi;
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Kullanicilar surucu)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        surucu.Kullanici_Durum = 1;
                        surucu.Firma_ID = FirmaID;
                        surucu.Kullanici_OlusturmaTarihi = DateTime.Now;
                        surucu.Kullanici_DuzenlemeTarihi = DateTime.Now;
                        surucu.EkleyenKullanici_ID = KullaniciID;
                        surucu.DuzenleyenID = KullaniciID;
                        surucuManager.TAdd(surucu);
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
            return View(surucu);
        }
        public JsonResult GetIlceler(string il)
        {
            List<AdresOzellikTanimlama> adres = adresOzellikTanimlamaManager.GetAllList((y => y.Il== il));
            var ilceler = adres.Select(a => new { a.Adres_ID, a.Ilce }).ToList();

            return Json(ilceler);
        }
        public IActionResult Duzenle(int SurucuID)
        {
            Kullanicilar arac = surucuManager.GetByID(SurucuID);
            return View(arac);
        }

        [HttpPost]
        public IActionResult Duzenle(Kullanicilar surucu)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Kullanicilar item = surucuManager.GetByID(surucu.Kullanici_ID);
                        item.SurucuPozisyonID = surucu.SurucuPozisyonID;
                        item.Kullanici_Sifre= surucu.Kullanici_Sifre;
                        item.Kullanici_Isim=surucu.Kullanici_Isim;
                        item.Kullanici_Soyisim=surucu.Kullanici_Soyisim;
                        item.TC=surucu.TC;
                        item.GirisTarihi=surucu.GirisTarihi;
                        item.CikisTarihi = surucu.CikisTarihi;
                        item.CikisNedeni = surucu.CikisNedeni;
                        item.DurumID=surucu.DurumID;
                        item.DogumTarihi = surucu.DogumTarihi;
                        item.Kullanici_Eposta=surucu.Kullanici_Eposta;
                        item.KanGrubu=surucu.KanGrubu;
                        item.CepTelefonu = surucu.CepTelefonu;
                        item.Adres=surucu.Adres;
                        item.Notlar=surucu.Notlar;
                        item.EhliyetSinifiID = surucu.EhliyetSinifiID;
                        item.EhliyetVerilisTarihi = surucu.EhliyetVerilisTarihi;
                        item.EhliyetVerilisYeri = surucu.EhliyetVerilisYeri;
                        item.EhliyetSonaErisTarihi = surucu.EhliyetSonaErisTarihi;
                        item.EhliyetResmi = surucu.EhliyetResmi;
                        item.BankaAdi= surucu.BankaAdi;
                        item.BankaIban = surucu.BankaIban;
                        item.MeslekiYeterlilikGecTarih = surucu.MeslekiYeterlilikGecTarih;
                        item.PsikoTeknikGecTarih = surucu.PsikoTeknikGecTarih;
                        item.PsikoTeknikKurumu = surucu.PsikoTeknikKurumu;
                        item.BGecerlilikTarih=surucu.BGecerlilikTarih;
                        item.BEGecerlilikTarih = surucu.BEGecerlilikTarih;
                        item.C1EGecerlilikTarih = surucu.C1EGecerlilikTarih;
                        item.C1GecerlilikTarih = surucu.C1GecerlilikTarih;
                        item.CGecerlilikTarih = surucu.CGecerlilikTarih;
                        item.D1GecerlilikTarih = surucu.D1GecerlilikTarih;
                        item.D1EGecerlilikTarih = surucu.D1EGecerlilikTarih;
                        item.FGecerlilikTarih = surucu.FGecerlilikTarih;
                        item.Maas = surucu.Maas;
                        item.Kullanici_DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        surucuManager.TUpdate(item);
                        TempData["Msg"] = "İşlem başarılı.";
                        TempData["Bgcolor"] = "green";

                        return View(surucu);
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
                        Kullanicilar item = surucuManager.GetByID(int.Parse(form["ID"]));
                        item.Kullanici_Durum = 0;
                        item.Firma_ID = FirmaID;
                        item.Kullanici_DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        surucuManager.TUpdate(item);
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
