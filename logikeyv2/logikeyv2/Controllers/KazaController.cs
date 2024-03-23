using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class KazaController : Controller
    {
        KazaManager KazaManager = new KazaManager(new EFKazaRepository());
        AracManager AracManager=new AracManager(new EFAracRepository());
        KullanicilarManager SurucuManager=new KullanicilarManager(new EFKullanicilarRepository());


        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<KazaViewModel> viewModel = KazaManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID)
                .GroupJoin(AracManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID),
                    kaza => kaza.AracID,
                    arac => arac.ID,
                    (kaza, aracGroup) => new { kaza, aracGroup })
                .SelectMany(
                    result => result.aracGroup.DefaultIfEmpty(),
                    (result, arac) => new { result.kaza, arac }
                )
                .GroupJoin(
                    SurucuManager.GetAllList(x => x.Kullanici_Durum == 1 && x.Firma_ID == FirmaID),
                    result => result.kaza.SurucuID,
                    surucu => surucu.Kullanici_ID,
                    (result, surucuGroup) => new { result.kaza, result.arac, surucuGroup }
                )
                .SelectMany(
                    result => result.surucuGroup.DefaultIfEmpty(),

        (result, surucu) => new KazaViewModel
        {
            Kaza = result.kaza,
            Plaka = result.arac != null ? result.arac.Plaka:"",
            Surucu = surucu != null ? surucu.Kullanici_Isim+" "+surucu.Kullanici_Soyisim : ""
        }
    )
    .ToList();

            return View(viewModel);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Kaza kaza)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        kaza.Durum = true;

                        kaza.FirmaID = FirmaID;
                        kaza.OlusturmaTarihi = DateTime.Now;
                        kaza.DuzenlemeTarihi = DateTime.Now;
                        kaza.OlusturanId = KullaniciID;
                        kaza.DuzenleyenID = KullaniciID;
                        KazaManager.TAdd(kaza);
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

        public IActionResult Duzenle(int KazaID)
        {
            Kaza kaza = KazaManager.GetByID(KazaID);
            return View(kaza);
        }
        [HttpPost]
        public IActionResult Duzenle(Kaza kaza)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Kaza item = KazaManager.GetByID(kaza.ID);
                        item.AracID = kaza.AracID;
                        item.SurucuID = kaza.SurucuID;
                        item.KazaTarihi = kaza.KazaTarihi;
                        item.KazaTuruID=kaza.KazaTuruID;
                        item.KarsiPlaka = kaza.KarsiPlaka;
                        item.KarsiSurucuTC = kaza.KarsiSurucuTC;
                        item.KarsiAracRuhsatSahibi = kaza.KarsiAracRuhsatSahibi;
                        item.KarsiAracSurucuAdiSoyadi = kaza.KarsiAracSurucuAdiSoyadi;
                        item.KarsiAracCepTelefonu = kaza.KarsiAracCepTelefonu;
                        item.SurucumuzKusurOrani = kaza.SurucumuzKusurOrani;
                        item.KarsiSurucuKusurOrani = kaza.KarsiSurucuKusurOrani;
                        item.KazaRaporNo = kaza.KazaRaporNo;
                        item.SigortaAcenteAdi = kaza.SigortaAcenteAdi;
                        item.SigortaAcenteYetkilisi = kaza.SigortaAcenteYetkilisi;
                        item.SigortaAcenteTelefonu = kaza.SigortaAcenteTelefonu;
                        item.HasarFaturaFirmasi = kaza.HasarFaturaFirmasi;
                        item.HasarFaturaTarihi = kaza.HasarFaturaTarihi;
                        item.GeriOdemeTarihi = kaza.GeriOdemeTarihi;
                        item.HasarTutari=kaza.HasarTutari;
                        item.SigortaninOdedigiTutar = kaza.SigortaninOdedigiTutar;
                        item.OdenemeyenTutar = kaza.OdenemeyenTutar;
                        item.ServisFirmaAdi = kaza.ServisFirmaAdi;
                        item.ServisFirmaYetkilisi = kaza.ServisFirmaYetkilisi;
                        item.ServisFirmaTelefonu = kaza.ServisFirmaTelefonu;
                        item.ServisFirmaAdres = kaza.ServisFirmaAdres;
                        item.Notlar = kaza.Notlar;

                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        KazaManager.TUpdate(item);
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
                        Kaza item = KazaManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        KazaManager.TUpdate(item);
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
