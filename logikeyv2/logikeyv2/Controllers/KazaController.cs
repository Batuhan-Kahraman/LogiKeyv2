using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Migrations;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class KazaController : BaseController
    {
        KazaManager KazaManager = new KazaManager(new EFKazaRepository());
        AracManager AracManager=new AracManager(new EFAracRepository());
        KullanicilarManager SurucuManager=new KullanicilarManager(new EFKullanicilarRepository());

        public async Task<string> DosyaYukle(IFormFile formFile)
        {
            var extent = Path.GetExtension(formFile.FileName);
            var randomName = $"{Guid.NewGuid()}{extent}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\upload", randomName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return randomName;
        }


        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<KazaViewModel> viewModel = KazaManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2))
                .GroupJoin(AracManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2)),
                    kaza => kaza.AracID,
                    arac => arac.ID,
                    (kaza, aracGroup) => new { kaza, aracGroup })
                .SelectMany(
                    result => result.aracGroup.DefaultIfEmpty(),
                    (result, arac) => new { result.kaza, arac }
                )
                .GroupJoin(
                    SurucuManager.GetAllList(x => x.Kullanici_Durum == 1 && (x.Firma_ID == FirmaID || x.Firma_ID == -2)),
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
        public async Task<IActionResult> Ekle(Kaza kaza, IFormFile KazaRaporu, IFormFile KarsiTarafTrafikSigortasi, IFormFile KarsiTarafKaskoPolicesi, IFormFile KarsiTarafRuhsat, IFormFile KarsiTarafEhliyet, IFormFile KazaResimleri)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (KazaRaporu != null && KazaRaporu.Length > 0)
                        {
                            kaza.KazaRaporu = await DosyaYukle(KazaRaporu);
                        }
                        if (KarsiTarafTrafikSigortasi != null && KarsiTarafTrafikSigortasi.Length > 0)
                        {
                            kaza.KarsiTarafTrafikSigortasi = await DosyaYukle(KarsiTarafTrafikSigortasi);
                        }
                        if (KarsiTarafKaskoPolicesi != null && KarsiTarafKaskoPolicesi.Length > 0)
                        {
                            kaza.KarsiTarafKaskoPolicesi = await DosyaYukle(KarsiTarafKaskoPolicesi);
                        }
                        if (KarsiTarafRuhsat != null && KarsiTarafRuhsat.Length > 0)
                        {
                            kaza.KarsiTarafRuhsat = await DosyaYukle(KarsiTarafRuhsat);
                        }
                        if (KarsiTarafEhliyet != null && KarsiTarafEhliyet.Length > 0)
                        {
                            kaza.KarsiTarafEhliyet = await DosyaYukle(KarsiTarafEhliyet);
                        }
                        if (KazaResimleri != null && KazaResimleri.Length > 0)
                        {
                            kaza.KazaResimleri = await DosyaYukle(KazaResimleri);
                        }


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
        public async Task<IActionResult> Duzenle(Kaza kaza, IFormFile KazaRaporu, IFormFile KarsiTarafTrafikSigortasi, IFormFile KarsiTarafKaskoPolicesi, IFormFile KarsiTarafRuhsat, IFormFile KarsiTarafEhliyet, IFormFile KazaResimleri)
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

                        if (KazaRaporu != null && KazaRaporu.Length > 0)
                        {
                            item.KazaRaporu = await DosyaYukle(KazaRaporu);
                        }
                        if (KarsiTarafTrafikSigortasi != null && KarsiTarafTrafikSigortasi.Length > 0)
                        {
                            item.KarsiTarafTrafikSigortasi = await DosyaYukle(KarsiTarafTrafikSigortasi);
                        }
                        if (KarsiTarafKaskoPolicesi != null && KarsiTarafKaskoPolicesi.Length > 0)
                        {
                            item.KarsiTarafKaskoPolicesi = await DosyaYukle(KarsiTarafKaskoPolicesi);
                        }
                        if (KarsiTarafRuhsat != null && KarsiTarafRuhsat.Length > 0)
                        {
                            item.KarsiTarafRuhsat = await DosyaYukle(KarsiTarafRuhsat);
                        }
                        if (KarsiTarafEhliyet != null && KarsiTarafEhliyet.Length > 0)
                        {
                            item.KarsiTarafEhliyet = await DosyaYukle(KarsiTarafEhliyet);
                        }
                        if (KazaResimleri != null && KazaResimleri.Length > 0)
                        {
                            item.KazaResimleri = await DosyaYukle(KazaResimleri);
                        }
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
