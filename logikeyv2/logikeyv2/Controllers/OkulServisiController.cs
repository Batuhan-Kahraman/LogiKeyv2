using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Migrations;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Patagames.Ocr.Enums;
using Patagames.Ocr;
using Tesseract;
using System.Reflection.Metadata;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Windows.Media.Ocr;
using Windows.Storage;
using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using Google.Cloud.Vision.V1;
using Google.Protobuf;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Protobuf;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class OkulServisiController : Controller
    {
        AracManager aracManager = new AracManager(new EFAracRepository());
        SahiplikManager sahiplikManager = new SahiplikManager(new EFSahiplikRepository());
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        AracTipManager aracTipManager = new AracTipManager(new EFAracTipRepository());
        MarkaManager markaManager = new MarkaManager(new EFMarkaRepository());
        ModelManager modelManager = new ModelManager(new EFModelRepository());
        AracResimlerManager aracResimlerManager = new AracResimlerManager(new EFAracResimRepository());
        AdresOzellikTanimlamaManager adresManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());
        GrupManager grupManager = new GrupManager(new EFGrupRepository());
        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<AracViewModel> viewModel = aracManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID)
                .GroupJoin(aracTurManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID),
                    arac => arac.AracTurID,
                    tur => tur.ID,
                    (arac, turGroup) => new { arac, turGroup })
                .SelectMany(
                    result => result.turGroup.DefaultIfEmpty(),
                    (result, tur) => new { result.arac, tur }
                )
                .GroupJoin(
                    aracTipManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID),
                    result => result.arac.AracTipID,
                    tip => tip.ID,
                    (result, tipGroup) => new { result.arac, result.tur, tipGroup }
                )
                .SelectMany(
                    result => result.tipGroup.DefaultIfEmpty(),
                    (result, tip) => new { result.arac, result.tur, tip }
                )
                .GroupJoin(
                    markaManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID),
                    result => result.arac.MarkaID,
                    marka => marka.ID,
                    (result, markaGroup) => new { result.arac, result.tur, result.tip, markaGroup }
                )
                .SelectMany(
                    result => result.markaGroup.DefaultIfEmpty(),
                    (result, marka) => new { result.arac, result.tur, result.tip, marka }
                )
                .GroupJoin(
                    modelManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID),
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
            Model = model != null ? model.Adi : "",
            AracTur = result.tur != null ? result.tur.Adi : "",
            AracTip = result.tip != null ? result.tip.Adi : ""
        }
    )
    .ToList();

            return View(viewModel);
        }
        public IActionResult Ekle()
        {
            if (TempData.ContainsKey("Arac"))
            {
                string aracJson = TempData["Arac"] as string;
                Arac arac = JsonConvert.DeserializeObject<Arac>(aracJson);
                return View(arac);
            }
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            ViewBag.Iller = iller;
            List<Sahiplik> sahiplikList = sahiplikManager.GetAllList(x => x.Durum==true &&  x.FirmaID==FirmaID && x.Adi=="Özmal" || x.Adi == "Taşeron" || x.Adi == "Kiralama");

            ViewBag.sahiplikList = sahiplikList;
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Arac arac, IFormFile ruhsat, IFormFile sigorta, IFormFile police, IFormFile sozlesme, IFormFile muayene
           , IFormFile mtv, IFormFile k1, IFormFile k2, List<IFormFile> resimler,string Grup,string aractur,string aractipi,string Marka, string Model)
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        if (ruhsat != null && ruhsat.Length > 0)
                        {
                            arac.AracRuhsat = DosyaYukle(ruhsat);
                        }
                        if (sigorta != null && sigorta.Length > 0)
                        {
                            arac.TrafikSigortasi = DosyaYukle(sigorta);
                        }
                        if (police != null && police.Length > 0)
                        {
                            arac.KaskoPolice = DosyaYukle(police);
                        }
                        if (sozlesme != null && sozlesme.Length > 0)
                        {
                            arac.IsSozlesmesi = DosyaYukle(sozlesme);
                        }
                        if (muayene != null && muayene.Length > 0)
                        {
                            arac.AraMuayene = DosyaYukle(muayene);
                        }
                        if (mtv != null && mtv.Length > 0)
                        {
                            arac.MTV = DosyaYukle(mtv);
                        }
                        if (k1 != null && k1.Length > 0)
                        {
                            arac.K1YetkiBelge = DosyaYukle(k1);
                        }
                        if (k2 != null && k2.Length > 0)
                        {
                            arac.K2YetkiBelge = DosyaYukle(k2);
                        }
                       
                        Grup aracgrup_item = new Grup();
                        aracgrup_item.Adi = Grup;
                        aracgrup_item.Durum = true;
                        aracgrup_item.OlusturanId = KullaniciID;
                        aracgrup_item.DuzenleyenID = KullaniciID;
                        aracgrup_item.OlusturmaTarihi = DateTime.UtcNow;
                        aracgrup_item.DuzenlemeTarihi = DateTime.UtcNow;
                        aracgrup_item.FirmaID = FirmaID;
                        grupManager.TAdd(aracgrup_item);
                        Grup kullaniciGrup = grupManager.GetAllList(x => x.Adi == aracgrup_item.Adi && x.OlusturmaTarihi == aracgrup_item.OlusturmaTarihi).FirstOrDefault();
                       
                        Marka aracMarka_Item = new Marka();
                        aracMarka_Item.Adi = Marka;
                        aracMarka_Item.Durum = true;
                        aracMarka_Item.OlusturanId = KullaniciID;
                        aracMarka_Item.DuzenleyenID = KullaniciID;
                        aracMarka_Item.OlusturmaTarihi = DateTime.UtcNow;
                        aracMarka_Item.DuzenlemeTarihi = DateTime.UtcNow;
                        aracMarka_Item.FirmaID = FirmaID;
                        markaManager.TAdd(aracMarka_Item);
                        Marka aracMarka = markaManager.GetAllList(x => x.Adi == aracMarka_Item.Adi && x.OlusturmaTarihi == aracMarka_Item.OlusturmaTarihi).FirstOrDefault();
                       
                        Model aracModel_Item = new Model();
                        aracModel_Item.Adi = Model;
                        aracModel_Item.Durum = true;
                        aracModel_Item.OlusturanId = KullaniciID;
                        aracModel_Item.DuzenleyenID = KullaniciID;
                        aracModel_Item.OlusturmaTarihi = DateTime.UtcNow;
                        aracModel_Item.DuzenlemeTarihi = DateTime.UtcNow;
                        aracModel_Item.FirmaID = FirmaID;
                        modelManager.TAdd(aracModel_Item);
                        Model aracModel = modelManager.GetAllList(x => x.Adi == aracModel_Item.Adi && x.OlusturmaTarihi == aracModel_Item.OlusturmaTarihi).FirstOrDefault();

                        AracTip aracTipi_Item = new AracTip();
                        aracTipi_Item.Adi = aractipi;
                        aracTipi_Item.Durum = true;
                        aracTipi_Item.OlusturanId = KullaniciID;
                        aracTipi_Item.DuzenleyenID = KullaniciID;
                        aracTipi_Item.OlusturmaTarihi = DateTime.UtcNow;
                        aracTipi_Item.DuzenlemeTarihi = DateTime.UtcNow;
                        aracTipi_Item.FirmaID = FirmaID;
                        aracTipManager.TAdd(aracTipi_Item);
                        AracTip aracTipi = aracTipManager.GetAllList(x => x.Adi == aracTipi_Item.Adi && x.OlusturmaTarihi == aracTipi_Item.OlusturmaTarihi).FirstOrDefault();

                        AracTur aracTuru_Item = new AracTur();
                        aracTuru_Item.Adi = aractur;
                        aracTuru_Item.Durum = true;
                        aracTuru_Item.OlusturanId = KullaniciID;
                        aracTuru_Item.DuzenleyenID = KullaniciID;
                        aracTuru_Item.OlusturmaTarihi = DateTime.UtcNow;
                        aracTuru_Item.DuzenlemeTarihi = DateTime.UtcNow;
                        aracTuru_Item.FirmaID = FirmaID;
                        aracTurManager.TAdd(aracTuru_Item);
                        AracTur aracTuru = aracTurManager.GetAllList(x => x.Adi == aracTuru_Item.Adi && x.OlusturmaTarihi == aracTuru_Item.OlusturmaTarihi).FirstOrDefault();

                        arac.AracTurID = aracTuru.ID;
                        arac.AracTipID = aracTipi.ID;
                        arac.ModelID = aracModel.ID;
                        arac.MarkaID = aracMarka.ID;
                        arac.GrupID = kullaniciGrup.ID;
                        arac.Durum = true;
                        arac.FirmaID = FirmaID;
                        arac.OlusturmaTarihi = DateTime.Now;
                        arac.DuzenlemeTarihi = DateTime.Now;
                        arac.OlusturanId = KullaniciID;
                        arac.DuzenleyenID = KullaniciID;
                   
                        aracManager.TAdd(arac);

                        if (resimler != null && resimler.Count() > 0)
                        {
                            foreach (var file in resimler)
                            {
                                if (file != null && file.Length > 0)
                                {
                                    AracResimler resim = new AracResimler();
                                    resim.AracID = arac.ID;
                                    resim.resim = DosyaYukle(file);
                                    aracResimlerManager.TAdd(resim);
                                }
                            }
                        }


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
            return RedirectToAction("Duzenle", new { AracID = arac.ID });

        }
        public string DosyaYukle(IFormFile file)
        {
            string randomFileName = Path.GetRandomFileName();

            // Dosyanın uzantısını al
            string fileExtension = Path.GetExtension(file.FileName);

            // Rastgele dosya adını uzantısıyla birleştirerek dosya adı oluştur
            string finalFileName = randomFileName + fileExtension;

            // Rastgele dosya adını kullanarak dosya yolu oluştur
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/" + finalFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }
            return finalFileName;
        }
        [HttpPost]
        public async Task<IActionResult> RuhsatEkleAsync(IFormFile file)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        if (file != null && file.Length > 0)
                        {
                            string randomFileName = Path.GetRandomFileName();

                            // Dosyanın uzantısını al
                            string fileExtension = Path.GetExtension(file.FileName);

                            // Rastgele dosya adını uzantısıyla birleştirerek dosya adı oluştur
                            string finalFileName = randomFileName + fileExtension;

                            // Rastgele dosya adını kullanarak dosya yolu oluştur
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ocr", finalFileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyToAsync(stream);
                            }

                            string jsonKeyPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/logikeyv2-5c36e9984c1c.json");

                            var credentialsPath = jsonKeyPath; // Anahtar dosyasının yolunu belirtin
                            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);

                            // Metni algılama işlemi

                            var imageContent = System.IO.File.ReadAllBytes(filePath);

                            var image = Image.FromBytes(imageContent);



                            // Görüntüyü dosya yolu ile yükle
                            //Image image = Image.FromFile(filePath);


                            var client = ImageAnnotatorClient.Create();

                            var response = client.DetectText(image);

                            // Algılanan metni yazdır
                            foreach (var annotation in response)
                            {
                                if (annotation.Description != null)
                                {
                                    string[] splittedStrings = annotation.Description.Split('\n');

                                    Arac arac = new Arac();
                                    arac.Plaka = splittedStrings[3];
                                    arac.TescilSiraNo = splittedStrings[6];
                                    arac.TescilTarihi = DateTime.Parse(splittedStrings[31]);
                                    arac.TescilSeriNo = splittedStrings[74];
                                    arac.RuhsatSahibi = splittedStrings[60] + splittedStrings[59];
                                    arac.SaseNo = splittedStrings[18];
                                    arac.MotorSeriNo = splittedStrings[13];

                                    string aracJson = JsonConvert.SerializeObject(arac);
                                    TempData["Arac"] = aracJson;

                                    TempData["Msg"] = "İşlem başarılı.";
                                    TempData["Bgcolor"] = "green";
                                    return RedirectToAction("Ekle", "Arac");
                                }
                            }

                        }


                        TempData["Msg"] = "İşlem başarısız.";
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                        return RedirectToAction("Ekle", "Arac");

                    }
                    catch (Exception e)
                    {
                        TempData["Msg"] = "İşlem başarısız.Hata: " + e;
                        TempData["Bgcolor"] = "red";
                        transaction.Rollback();
                        return RedirectToAction("Ekle", "Arac");
                    }
                }

            }
        }


        public IActionResult Duzenle(int AracID)
        {
            ViewBag.Resimler = aracResimlerManager.GetAllList(x => x.AracID == AracID);
            Arac arac = aracManager.GetByID(AracID);
            return View(arac);
        }

        [HttpPost]
        public IActionResult Duzenle(Arac arac, IFormFile ruhsat, IFormFile sigorta, IFormFile police, IFormFile sozlesme, IFormFile muayene
            , IFormFile mtv, IFormFile k1, IFormFile k2, List<IFormFile> resimler)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Arac item = aracManager.GetByID(arac.ID);
                        item.Plaka = arac.Plaka;
                        item.SahiplikID = arac.SahiplikID;
                        item.GrupID = arac.GrupID;
                        item.AracTurID = arac.AracTurID;
                        item.AracTipID = arac.AracTipID;
                        item.BakimPeriyodu = arac.BakimPeriyodu;
                        item.Goz1 = arac.Goz1;
                        item.Goz2 = arac.Goz2;
                        item.Goz3 = arac.Goz3;
                        item.Goz4 = arac.Goz4;
                        item.Goz5 = arac.Goz5;
                        item.Goz6 = arac.Goz6;
                        item.MarkaID = arac.MarkaID;
                        item.ModelID = arac.ModelID;
                        item.Yil = arac.Yil;
                        item.SirketID = arac.SirketID;
                        item.BunyeGirisTarih = arac.BunyeGirisTarih;
                        item.BunyeGirisFiyat = arac.BunyeGirisFiyat;
                        item.BunyeGirisNot = arac.BunyeGirisNot;
                        item.BunyeCikisTarih = arac.BunyeCikisTarih;
                        item.BunyeCikisFiyat = arac.BunyeCikisFiyat;
                        item.BunyeCikisNot = arac.BunyeCikisNot;
                        item.HGS = arac.HGS;
                        item.TTS = arac.TTS;
                        item.DepoSensorID = arac.DepoSensorID;
                        item.Ekipman = arac.Ekipman;
                        item.Notlar = arac.Notlar;
                        item.MotorSeriNo = arac.MotorSeriNo;
                        item.SaseNo = arac.SaseNo;
                        item.MotorGucu = arac.MotorGucu;
                        item.YakitTipiID = arac.YakitTipiID;
                        item.YakitDepo = arac.YakitDepo;
                        item.YakitSarfiyat = arac.YakitSarfiyat;
                        item.BosAgirlik = arac.BosAgirlik;
                        item.AzamiYukluAgirlik = arac.AzamiYukluAgirlik;
                        item.IstiapHaddi = arac.IstiapHaddi;
                        item.KasaEn = arac.KasaEn;
                        item.KasaBoy = arac.KasaBoy;
                        item.KasaYukseklik = arac.KasaYukseklik;
                        item.EuroPalet = arac.EuroPalet;
                        item.LastikTip1 = arac.LastikTip1;
                        item.LastikTip2 = arac.LastikTip2;
                        item.LastikTip3 = arac.LastikTip3;
                        item.LastikTipAdet1 = arac.LastikTipAdet1;
                        item.LastikTipAdet2 = arac.LastikTipAdet2;
                        item.LastikTipAdet3 = arac.LastikTipAdet3;
                        item.AkuTipID = arac.AkuTipID;
                        item.AkuAmper = arac.AkuAmper;
                        item.AkuAdet = arac.AkuAdet;
                        item.MotorYagTipi = arac.MotorYagTipi;
                        item.MotorYagListesi = arac.MotorYagListesi;
                        item.MotorYagPeriyot = arac.MotorYagPeriyot;
                        item.HidrolikYagTipi = arac.HidrolikYagTipi;
                        item.HidrolikYagListesi = arac.HidrolikYagListesi;
                        item.HidrolikYagTipi = arac.HidrolikYagTipi;
                        item.HidrolikYagPeriyot = arac.HidrolikYagPeriyot;
                        item.GresYagTipi = arac.GresYagTipi;
                        item.GresYagKg = arac.GresYagKg;
                        item.HavaFiltreAdet = arac.HavaFiltreAdet;
                        item.HavaFiltreKm = arac.HavaFiltreKm;
                        item.MazotFiltre = arac.MazotFiltre;
                        item.MazotFiltreAdet = arac.MazotFiltreAdet;
                        item.YagFiltre = arac.YagFiltre;
                        item.YagFiltreAdet = arac.YagFiltreAdet;
                        item.RuhsatSahibi = arac.RuhsatSahibi;
                        item.TescilTarihi = arac.TescilTarihi;
                        item.TescilSiraNo = arac.TescilSiraNo;
                        item.TescilSeriNo = arac.TescilSeriNo;
                        item.TrafigeCikisTarihi = arac.TrafigeCikisTarihi;
                        item.VerildigiIlceID = arac.VerildigiIlceID;
                        item.VerildigiIlID = arac.VerildigiIlID;
                        item.RuhsatSonKullanma = arac.RuhsatSonKullanma;
                        item.MotorluAracSeriNo = arac.MotorluAracSeriNo;
                        item.TaksimetreSeriNo = arac.TaksimetreSeriNo;
                        item.TakografSeriNo = arac.TakografSeriNo;
                        item.GarantiBaslangicKm = arac.GarantiBaslangicKm;
                        item.GarantiBaslangicTarih = arac.GarantiBaslangicTarih;
                        item.GarantiBitisTarih = arac.GarantiBitisTarih;
                        item.GarantiBitisKm = arac.GarantiBitisKm;
                        item.GarantiKmLimit = arac.GarantiKmLimit;
                        item.IseBaslamaNot = arac.IseBaslamaNot;
                        item.IseBaslamaSuresi = arac.IseBaslamaSuresi;
                        item.IseBaslamaTarihi = arac.IseBaslamaTarihi;
                        item.KiralamaSuresi = arac.KiralamaSuresi;
                        item.KiralamaTutari = arac.KiralamaTutari;
                        item.KiralamaTarihi = arac.KiralamaTarihi;



                        if (ruhsat != null && ruhsat.Length > 0)
                        {
                            item.AracRuhsat = DosyaYukle(ruhsat);
                        }
                        if (sigorta != null && sigorta.Length > 0)
                        {
                            item.TrafikSigortasi = DosyaYukle(sigorta);
                        }
                        if (police != null && police.Length > 0)
                        {
                            item.KaskoPolice = DosyaYukle(police);
                        }
                        if (sozlesme != null && sozlesme.Length > 0)
                        {
                            item.IsSozlesmesi = DosyaYukle(sozlesme);
                        }
                        if (muayene != null && muayene.Length > 0)
                        {
                            item.AraMuayene = DosyaYukle(muayene);
                        }
                        if (mtv != null && mtv.Length > 0)
                        {
                            item.MTV = DosyaYukle(mtv);
                        }
                        if (k1 != null && k1.Length > 0)
                        {
                            item.K1YetkiBelge = DosyaYukle(k1);
                        }
                        if (k2 != null && k2.Length > 0)
                        {
                            item.K2YetkiBelge = DosyaYukle(k2);
                        }


                        if (resimler != null && resimler.Count() > 0)
                        {
                            foreach (var file in resimler)
                            {
                                if (file != null && file.Length > 0)
                                {
                                    AracResimler resim = new AracResimler();
                                    resim.AracID = arac.ID;
                                    resim.resim = DosyaYukle(file);
                                    aracResimlerManager.TAdd(resim);
                                }
                            }
                        }





                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
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
                        Arac item = aracManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
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
