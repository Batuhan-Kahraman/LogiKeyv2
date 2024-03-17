using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    public class MasrafCezaController : Controller
    {
        MasrafCezaManager MasrafCezaManager = new MasrafCezaManager(new EFMasrafCezaRepository());
        AracManager AracManager = new AracManager(new EFAracRepository());
        KullanicilarManager SurucuManager = new KullanicilarManager(new EFKullanicilarRepository());


        public IActionResult Index()
        {
            List<MasrafCezaViewModel> viewModel = MasrafCezaManager.GetAllList(x => x.Durum == true)
    .GroupJoin(
        AracManager.GetAllList(x => x.Durum == true),
        masraf => masraf.AracID,
        arac => arac.ID,
        (masraf, aracGroup) => new { masraf, aracGroup }
    )
    .SelectMany(
        result => result.aracGroup.DefaultIfEmpty(),
        (result, arac) => new { result.masraf, arac }
    )
    .GroupJoin(
        SurucuManager.GetAllList(x => x.Kullanici_Durum == 1),
        result => result.masraf.SurucuID,
        surucu => surucu.Kullanici_ID,
        (result, surucuGroup) => new { result.masraf, result.arac, surucuGroup }
    )
    .SelectMany(
        result => result.surucuGroup.DefaultIfEmpty(),
        (result, surucu) => new MasrafCezaViewModel
        {
            MasrafCeza = result.masraf,
            Plaka = result.arac?.Plaka,
            Surucu = surucu != null ? surucu.Kullanici_Isim + " " + surucu.Kullanici_Soyisim : ""
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
        public IActionResult Ekle(MasrafCeza masrafCeza)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        masrafCeza.Durum = true;
                        masrafCeza.FirmaID = 1;//değişçek
                        masrafCeza.OlusturmaTarihi = DateTime.Now;
                        masrafCeza.DuzenlemeTarihi = DateTime.Now;
                        masrafCeza.OlusturanId = 1;//değişcek
                        masrafCeza.DuzenleyenID = 1;//değişcek
                        MasrafCezaManager.TAdd(masrafCeza);
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

        public IActionResult Duzenle(int MasrafCezaID)
        {
            MasrafCeza masrafCeza = MasrafCezaManager.GetByID(MasrafCezaID);
            return View(masrafCeza);
        }

        [HttpPost]
        public IActionResult Duzenle(MasrafCeza masrafCeza)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        MasrafCeza item = MasrafCezaManager.GetByID(masrafCeza.ID);

                        item.AracID = masrafCeza.AracID;
                        item.SurucuID= masrafCeza.SurucuID;
                        item.TedarikciID= masrafCeza.TedarikciID;
                        item.FaturaTarihi = masrafCeza.FaturaTarihi;
                        item.FaturaNo=masrafCeza.FaturaNo;
                        item.AracKM=masrafCeza.AracKM;
                        item.MasrafTipiID = masrafCeza.MasrafTipiID;
                        item.MasrafDetay = masrafCeza.MasrafDetay;
                        item.KdvDahilMi = masrafCeza.KdvDahilMi;
                        item.Miktar=masrafCeza.Miktar;
                        item.BirimFiyat = masrafCeza.BirimFiyat;
                        item.Tutar = masrafCeza.Tutar;
                        item.KDV=masrafCeza.KDV;
                        item.YakitTipiID = masrafCeza.YakitTipiID;
                        item.Iskonto=masrafCeza.Iskonto;
                        item.ToplamTutar = masrafCeza.ToplamTutar;
                        item.Notlar = masrafCeza.Notlar;
                        


                        item.FirmaID = 1;//değişçek
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = 1;//değişcek
                        MasrafCezaManager.TUpdate(item);
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
                        MasrafCeza item = MasrafCezaManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        MasrafCezaManager.TUpdate(item);
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
