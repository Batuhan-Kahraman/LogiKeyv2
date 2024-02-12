using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class SurucuController : Controller
    {
        SurucuManager surucuManager = new SurucuManager(new EFSurucuRepository());
        EhliyetSinifiManager ehliyetSinifiManager = new EhliyetSinifiManager(new EFEhliyetSinifiRepository());
        SurucuPozisyonManager surucuPozisyonManager = new SurucuPozisyonManager(new EFSurucuPozisyonRepository());
        public IActionResult Index()
        {
            List<SurucuViewModel> viewModel = surucuManager.GetAllList(x => x.Durum == true)
                .GroupJoin(ehliyetSinifiManager.GetAllList(x => x.Durum == true),
                    surucu => surucu.EhliyetSinifiID,
                    ehliyetSinifi => ehliyetSinifi.ID,
                    (surucu, ehliyetSinifiGroup) => new { surucu, ehliyetSinifiGroup })
                .SelectMany(
                    result => result.ehliyetSinifiGroup.DefaultIfEmpty(),
                    (result, ehliyetSinifi) => new { result.surucu, ehliyetSinifi }
            )
            .GroupJoin(
                    surucuPozisyonManager.GetAllList(x => x.Durum == true),
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
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Surucu surucu)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        surucu.Durum = true;
                        surucu.FirmaID = 1;//değişçek
                        surucu.OlusturmaTarihi = DateTime.Now;
                        surucu.DuzenlemeTarihi = DateTime.Now;
                        surucu.OlusturanId = 1;//değişcek
                        surucu.DuzenleyenID = 1;//değişcek
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
        public IActionResult Duzenle(int SurucuID)
        {
            Surucu arac = surucuManager.GetByID(SurucuID);
            return View(arac);
        }

        [HttpPost]
        public IActionResult Duzenle(Surucu surucu)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Surucu item = surucuManager.GetByID(surucu.ID);
                        item.SurucuPozisyonID = surucu.SurucuPozisyonID;
                        item.Sifre= surucu.Sifre;
                        item.Isim=surucu.Isim;
                        item.Soyisim=surucu.Soyisim;
                        item.TC=surucu.TC;
                        item.GirisTarihi=surucu.GirisTarihi;
                        item.CikisTarihi = surucu.CikisTarihi;
                        item.CikisNedeni = surucu.CikisNedeni;
                        item.DurumID=surucu.DurumID;
                        item.DogumTarihi = surucu.DogumTarihi;
                        item.Eposta=surucu.Eposta;
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

                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = 1;//değişcek
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
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Surucu item = surucuManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
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
