using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace logikeyv2.Controllers
{
    public class AracController : Controller
    {
        AracManager aracManager = new AracManager(new EFAracRepository());
        SahiplikManager sahiplikManager = new SahiplikManager(new EFSahiplikRepository());
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        AracTipManager aracTipManager = new AracTipManager(new EFAracTipRepository());
        MarkaManager markaManager = new MarkaManager(new EFMarkaRepository());
        ModelManager modelManager = new ModelManager(new EFModelRepository());

        public IActionResult Index()
        {
            List<AracViewModel> viewModel = aracManager.GetAllList(x => x.Durum == true)
                .GroupJoin(aracTurManager.GetAllList(x => x.Durum == true),
                    arac => arac.AracTurID,
                    tur => tur.ID,
                    (arac, turGroup) => new { arac, turGroup })
                .SelectMany(
                    result => result.turGroup.DefaultIfEmpty(),
                    (result, tur) => new { result.arac, tur }
                )
                .GroupJoin(
                    aracTipManager.GetAllList(x => x.Durum == true),
                    result => result.arac.AracTipID,
                    tip => tip.ID,
                    (result, tipGroup) => new { result.arac, result.tur, tipGroup }
                )
                .SelectMany(
                    result => result.tipGroup.DefaultIfEmpty(),
                    (result, tip) => new { result.arac, result.tur, tip }
                )
                .GroupJoin(
                    markaManager.GetAllList(x => x.Durum == true),
                    result => result.arac.MarkaID,
                    marka => marka.ID,
                    (result, markaGroup) => new { result.arac, result.tur, result.tip, markaGroup }
                )
                .SelectMany(
                    result => result.markaGroup.DefaultIfEmpty(),
                    (result, marka) => new { result.arac, result.tur, result.tip, marka }
                )
                .GroupJoin(
                    modelManager.GetAllList(x => x.Durum == true),
                    result => result.arac.ModelID,
                    model => model.ID,
                    (result, modelGroup) => new { result.arac, result.tur, result.tip, result.marka, modelGroup }
                )
                .SelectMany(
                    result => result.modelGroup.DefaultIfEmpty(),

        (result, model) => new AracViewModel
        {
            Arac = result.arac,
            Plaka = result.arac.Plaka,
            Marka = result.marka != null ? result.marka.Adi : "",
            Model = model != null ? model.Adi:"",
            AracTur = result.tur != null ? result.tur.Adi : "",
            AracTip = result.tip != null ? result.tip.Adi:""
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
        public IActionResult Ekle(Arac arac)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        arac.Durum = true;
                        arac.FirmaID = 1;//değişçek
                        arac.OlusturmaTarihi = DateTime.Now;
                        arac.DuzenlemeTarihi = DateTime.Now;
                        arac.OlusturanId = 1;//değişcek
                        arac.DuzenleyenID = 1;//değişcek
                        aracManager.TAdd(arac);
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
            return View(arac);
        }

        public IActionResult Duzenle(int AracID)
        {
            Arac arac=aracManager.GetByID(AracID);
            return View(arac);
        }

        [HttpPost]
        public IActionResult Duzenle(Arac arac)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Arac item = aracManager.GetByID(arac.ID);
                        item.Plaka=arac.Plaka;
                        item.SahiplikID = arac.SahiplikID;
                        item.GrupID = arac.GrupID;
                        item.AracTurID = arac.AracTurID;
                        item.AracTipID= arac.AracTipID;
                        item.BakimPeriyodu = arac.BakimPeriyodu;
                        item.Goz1=arac.Goz1;
                        item.Goz2=arac.Goz2;
                        item.Goz3=arac.Goz3;
                        item.Goz4=arac.Goz4;
                        item.Goz5=arac.Goz5;
                        item.Goz6=arac.Goz6;
                        item.MarkaID=arac.MarkaID;
                        item.ModelID= arac.ModelID;
                        item.Yil=arac.Yil;
                        item.SirketID=arac.SirketID;
                        item.BunyeGirisTarih = arac.BunyeGirisTarih;
                        item.BunyeGirisFiyat = arac.BunyeGirisFiyat;
                        item.BunyeGirisNot = arac.BunyeGirisNot;
                        item.BunyeCikisTarih = arac.BunyeCikisTarih;
                        item.BunyeCikisFiyat = arac.BunyeCikisFiyat;
                        item.BunyeCikisNot = arac.BunyeCikisNot;
                        item.HGS=arac.HGS;
                        item.TTS=arac.TTS;
                        item.DepoSensorID=arac.DepoSensorID;
                        item.Ekipman=arac.Ekipman;
                        item.Notlar=arac.Notlar;
                        item.MotorSeriNo=arac.MotorSeriNo;
                        item.SaseNo=arac.SaseNo;
                        item.MotorGucu=arac.MotorGucu;
                        item.YakitTipiID=arac.YakitTipiID;
                        item.YakitDepo=arac.YakitDepo;
                        item.YakitSarfiyat = arac.YakitSarfiyat;
                        item.BosAgirlik=arac.BosAgirlik;
                        item.AzamiYukluAgirlik = arac.AzamiYukluAgirlik;
                        item.IstiapHaddi=arac.IstiapHaddi;
                        item.KasaEn=arac.KasaEn;
                        item.KasaBoy=arac.KasaBoy;
                        item.KasaYukseklik=arac.KasaYukseklik;
                        item.EuroPalet = arac.EuroPalet;
                        item.LastikTip1 = arac.LastikTip1;
                        item.LastikTip2 = arac.LastikTip2;
                        item.LastikTip3 = arac.LastikTip3;
                        item.AkuTipID=arac.AkuTipID;
                        item.AkuAmperBirim = arac.AkuAmperBirim;
                        item.AkuAdet=arac.AkuAdet;
                        item.MotorYagTipi=arac.MotorYagTipi;
                        item.MotorYagListesi=arac.MotorYagListesi;
                        item.MotorYagPeriyot=arac.MotorYagPeriyot;
                        item.HidrolikYagTipi= arac.HidrolikYagTipi;
                        item.HidrolikYagListesi = arac.HidrolikYagListesi;
                        item.HidrolikYagTipi = arac.HidrolikYagTipi;
                        item.HidrolikYagPeriyot = arac.HidrolikYagPeriyot;
                        item.GresYagTipi = arac.GresYagTipi;
                        item.GresYagKg = arac.GresYagKg;
                        item.HavaFiltreAdet=arac.HavaFiltreAdet;
                        item.HavaFiltreKm=arac.HavaFiltreKm;
                        item.MazotFiltre=arac.MazotFiltre;
                        item.MazotFiltreAdet= arac.MazotFiltreAdet;
                        item.YagFiltre=arac.YagFiltre;
                        item.YagFiltreAdet=arac.YagFiltreAdet;
                        item.RuhsatSahibi=arac.RuhsatSahibi;
                        item.TescilTarihi=arac.TescilTarihi;
                        item.TescilSiraNo=arac.TescilSiraNo;
                        item.TescilSeriNo=arac.TescilSeriNo;
                        item.TrafigeCikisTarihi = arac.TrafigeCikisTarihi;
                        item.VerildigiIlceID=arac.VerildigiIlceID;
                        item.VerildigiIlID=arac.VerildigiIlID;
                        item.RuhsatSonKullanma = arac.RuhsatSonKullanma;
                        item.MotorluAracSeriNo=arac.MotorluAracSeriNo;
                        item.TaksimetreSeriNo=arac.TaksimetreSeriNo;
                        item.TakografSeriNo = arac.TakografSeriNo;
                        item.GarantiBaslangicKm = arac.GarantiBaslangicKm;
                        item.GarantiBaslangicTarih = arac.GarantiBaslangicTarih;
                        item.GarantiBitisTarih = arac.GarantiBitisTarih;
                        item.GarantiBitisKm = arac.GarantiBitisKm;
                        item.GarantiKmLimit=arac.GarantiKmLimit;
                        item.IseBaslamaNot = arac.IseBaslamaNot;
                        item.IseBaslamaSuresi = arac.IseBaslamaSuresi;
                        item.IseBaslamaTarihi = arac.IseBaslamaTarihi;
                        item.KiralamaSuresi = arac.KiralamaSuresi;
                        item.KiralamaTutari = arac.KiralamaTutari;
                        item.KiralamaTarihi = arac.KiralamaTarihi;



                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = 1;//değişcek
                        aracManager.TUpdate(item);
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
            return View(arac);
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
                        Arac item = aracManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        aracManager.TUpdate(item);
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
