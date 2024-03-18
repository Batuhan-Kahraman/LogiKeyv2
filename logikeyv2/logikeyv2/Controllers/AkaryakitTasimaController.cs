using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.EntityFrameworks;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace logikeyv2.Controllers
{
    public class AkaryakitTasimaController : Controller
    {
        AracManager aracManager = new AracManager(new EFAracRepository());
        KullanicilarManager surucuManager = new KullanicilarManager(new EFKullanicilarRepository());
        TasinacakUrunManager tasinacakUrunManager = new TasinacakUrunManager(new EFTasinacakUrunRepository());
        TasimaManager tasimaManager = new TasimaManager(new EFTasimaRepository());
        UnListesiManager unListesiManager = new UnListesiManager(new EFUnListesiRepository());
        CariManager cariManager = new CariManager(new EFCariRepository());
        CariUcretlendirmeManager ucretlendirme = new CariUcretlendirmeManager(new EFCariUcretlendirmeRepository());
        Cari_OdemeYapanManager cari_OdemeYapanManager = new Cari_OdemeYapanManager(new EFCari_OdemeYapanRepository());
        AracTipManager aracTipManager = new AracTipManager(new EFAracTipRepository());
        AracTurManager aracTurManager = new AracTurManager(new EFAracTurRepository());
        BirimlerManager birimlerManager = new BirimlerManager(new EFBirimlerRepository());
        TasimaTipiManager tasimaTipiManager = new TasimaTipiManager(new EFTasimaTipiRepository());
        AdresOzellikTanimlamaManager adresManager = new AdresOzellikTanimlamaManager(new EFAdresOzellikTanimlamaRepository());
        AkaryakitTasimaManager akaryakitTasimaManager = new AkaryakitTasimaManager(new EFAkaryakitTasimaRepository());
        AkaryakitTasimaDetayManager akaryakitTasimaDetayManager = new AkaryakitTasimaDetayManager(new EFAkaryakitTasimaDetayRepository());
        AkaryakitTasimaDetayUrunManager akaryakitTasimaDetayUrunManager = new AkaryakitTasimaDetayUrunManager(new EFAkaryakitTasimaDetayUrunRepository());
        AkaryakitFaturaManager akaryakitFaturaManager = new AkaryakitFaturaManager(new EFAkaryakitFaturaRepository());
        AkaryakitAracTurManager akaryakitAracTurManager = new AkaryakitAracTurManager(new EFAkaryakitAracTurRepository());




        public IActionResult Index()
        {


            var combinedQuery = from tasima in akaryakitTasimaManager.GetAllList(x => x.Durum == true)
                                join arac in aracManager.GetAllList((y => y.Durum == true)) on tasima.AracID equals arac.ID
                                join surucu1 in surucuManager.GetAllList((y => y.Kullanici_Durum == 1)) on tasima.Kullanici1ID equals surucu1.Kullanici_ID
                                select new TasimaModel { Tasima = tasima, Arac = arac, Surucu = surucu1 };

            List<TasimaModel> combinedList = combinedQuery.ToList();
            return View(combinedList);
        }
        public IActionResult TasimaEkle()
        {
            List<Arac> aracListesi = aracManager.GetAllList(x => x.Durum == true);
            List<Kullanicilar> surucuListesi = surucuManager.GetAllList(x => x.Kullanici_Durum == 1);
            List<TasinacakUrun> tasinacakUrun = tasinacakUrunManager.GetAllList(x => x.Durum == true);
            List<UnListesi> UnListesi = unListesiManager.GetAllList(x => x.Durum == 1);
            List<Cari> CariListesi = cariManager.GetAllList(x => x.Durum == 1);
            List<CariUcretlendirme> Ucretlendirme = ucretlendirme.GetAllList(x => x.Durum == true);
            List<AracTip> aracTip = aracTipManager.GetAllList(x => x.Durum == true);
            List<AracTur> aracTur = aracTurManager.GetAllList(x => x.Durum == true);
            List<Birimler> birimler = birimlerManager.GetAllList(x => x.Durum == true);
            List<TasimaTipi> tasimaTipi = tasimaTipiManager.GetAllList(x => x.Durum == true);

            List<Arac> cekiciPlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 1);
            List<Arac> dorsePlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 4);
            List<AracTip> dorseListesi = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID == 4);


            List<Cari> AliciListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 8);
            List<Cari> GondericiListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 6);


            List<Cari_OdemeYapan> CariOdemeYapan = cari_OdemeYapanManager.GetAllList(x=>x.Durum== true);


            //ViewBag.Araclar = aracListesi;
            ViewBag.CekiciPlaka = cekiciPlaka;
            ViewBag.DorsePlaka = dorsePlaka;
            ViewBag.DorseListe = dorseListesi;
            ViewBag.Suruculer = surucuListesi;
            ViewBag.TasinacakUrun = tasinacakUrun;
            ViewBag.UnListesi = UnListesi;
            ViewBag.CariListesi = CariListesi;
            ViewBag.aracTip = aracTip;
            ViewBag.aracTur = aracTur;
            ViewBag.birimler = birimler;
            ViewBag.tasimaTipi = tasimaTipi;
            ViewBag.AliciListesi = AliciListesi;
            ViewBag.GondericiListesi = GondericiListesi;
            ViewBag.CariOdemeYapan = CariOdemeYapan;
            ViewBag.Ucretlendirme = Ucretlendirme;
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;

            var combinedQuery = from arac in aracManager.GetAllList((y => y.Durum == true && y.AracTurID == 13 || y.AracTurID == 1)) 
                                join tur in aracTurManager.GetAllList((y => y.Durum == true)) on arac.AracTurID equals tur.ID
                                select new
                                {
                                    AracPlakasi = arac.Plaka,
                                    AracID = arac.ID,
                                    TurID = tur.ID,
                                    AracTurAdi = tur.Adi
                                };

            ViewBag.Araclar = combinedQuery.ToList();


            List<AkaryakitAracTur> akaryakitAracTur = akaryakitAracTurManager.List();

            ViewBag.AkaryakitAracTur = akaryakitAracTur;
            return View();
        }

        [HttpPost]
        public IActionResult TasimaEkle(AkaryakitTasima akaryakitTasima, IFormCollection form)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int ToplamFiyat = 0;
                        string[] aracIDForm = form["AracID"].ToString().Split('|');
                        int aracID = int.Parse(aracIDForm[0]);
                        akaryakitTasima.AracID=aracID;
                        akaryakitTasima.Goz1UrunID = string.IsNullOrWhiteSpace(form["Goz1UrunID"]) ? 0 : int.Parse(form["Goz1UrunID"]);
                        akaryakitTasima.Goz2UrunID = string.IsNullOrWhiteSpace(form["Goz2UrunID"]) ? 0 : int.Parse(form["Goz2UrunID"]);
                        akaryakitTasima.Goz3UrunID = string.IsNullOrWhiteSpace(form["Goz3UrunID"]) ? 0 : int.Parse(form["Goz3UrunID"]);
                        akaryakitTasima.Goz4UrunID = string.IsNullOrWhiteSpace(form["Goz4UrunID"]) ? 0 : int.Parse(form["Goz4UrunID"]);
                        akaryakitTasima.Goz5UrunID = string.IsNullOrWhiteSpace(form["Goz5UrunID"]) ? 0 : int.Parse(form["Goz5UrunID"]);
                        akaryakitTasima.Goz6UrunID = string.IsNullOrWhiteSpace(form["Goz6UrunID"]) ? 0 : int.Parse(form["Goz6UrunID"]);
                        akaryakitTasima.Durum = true;
                        akaryakitTasima.FirmaID = 1;//değişçek
                        akaryakitTasima.OlusturmaTarihi = DateTime.Now;
                        akaryakitTasima.DuzenlemeTarihi = DateTime.Now;
                        akaryakitTasima.OlusturanId = 1;//değişcek
                        akaryakitTasima.DuzenleyenID = 1;//değişcek

                        akaryakitTasima.AracTurID = int.Parse(form["AracTurID"]);
                        akaryakitTasimaManager.TAdd(akaryakitTasima);
                        int kayitSayisi = int.Parse(form["KayitSayisi"]);
                        for (var i = 1; i <= kayitSayisi; i++)
                        {
                            AkaryakitTasimaDetay akaryakitTasimaDetay = new AkaryakitTasimaDetay();
                            akaryakitTasimaDetay.AkaryakitTasimaID = akaryakitTasima.ID;
                            akaryakitTasimaDetay.GondericiID = int.Parse(form["GondericiCari_ID" + i + "[]"]);
                            akaryakitTasimaDetay.GondericiYuklemeIlID = int.Parse(form["MalYuklemeAdres_IL_ID" + i + "[]"]);
                            akaryakitTasimaDetay.GondericiYuklemeIlceID = int.Parse(form["MalYuklemeAdres_ILCE_ID" + i + "[]"]);
                            akaryakitTasimaDetay.GondericiYuklemeTarihSaat = DateTime.Parse(form["GondericiFirmaTarihSaat" + i + "[]"]);
                            akaryakitTasimaDetay.AliciID = int.Parse(form["AliciCari_ID" + i + "[]"]);
                            akaryakitTasimaDetay.AliciIndirilenIlID = int.Parse(form["IndirilenAdres_IL_ID" + i + "[]"]);
                            akaryakitTasimaDetay.AliciIndirilenIlceID = int.Parse(form["IndirilenAdres_ILCE_ID" + i + "[]"]);
                            akaryakitTasimaDetay.AliciIndirilenTarihSaat = DateTime.Parse(form["AliciFirmaTarihSaat" + i + "[]"]);
                            akaryakitTasimaDetay.NakliyeTutarKDVHaric = int.Parse(form["NakliyeBedelTutar_KDVsiz" + i + "[]"]);
                            akaryakitTasimaDetay.NakliyeKDV = int.Parse(form["NakliyeBedelTutar_KDV" + i + "[]"]);
                            akaryakitTasimaDetay.NakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
                            akaryakitTasimaDetay.NakliyeFiyat = int.Parse(form["Fiyat" + i + "[]"]);
                            akaryakitTasimaDetay.Durum = true;
                            akaryakitTasimaDetay.FirmaID = 1;//değişçek
                            akaryakitTasimaDetay.OlusturmaTarihi = DateTime.Now;
                            akaryakitTasimaDetay.DuzenlemeTarihi = DateTime.Now;
                            akaryakitTasimaDetay.OlusturanId = 1;//değişcek
                            akaryakitTasimaDetay.DuzenleyenID = 1;//değişcek
                            akaryakitTasimaDetayManager.TAdd(akaryakitTasimaDetay);
                            ToplamFiyat +=akaryakitTasimaDetay.NakliyeFiyat;
                            AkaryakitFatura akaryakitFatura = new AkaryakitFatura();
                            akaryakitFatura.FaturaNo = akaryakitTasima.ID + "_" + DateTime.Now; //isteğe göre değiştirilebilir
                            akaryakitFatura.AkaryakitTasimaID = akaryakitTasima.ID;
                            akaryakitFatura.AkaryakitTasimaDetayID = akaryakitTasimaDetay.ID;
                            akaryakitFatura.Durum = true;
                            akaryakitFatura.FirmaID = 1;//değişçek
                            akaryakitFatura.OlusturmaTarihi = DateTime.Now;
                            akaryakitFatura.DuzenlemeTarihi = DateTime.Now;
                            akaryakitFatura.OlusturanId = 1;//değişcek
                            akaryakitFatura.DuzenleyenID = 1;//değişcek
                            string detayUrunId = "";
                            string faturaKesenId = "";
                            string faturaKesilenId = "";
                            for (var j = 0; j < form["Un_ID" + i + "[]"].Count(); j++)
                            {
                                AkaryakitTasimaDetayUrun akaryakitTasimaDetayUrun = new AkaryakitTasimaDetayUrun();
                                akaryakitTasimaDetayUrun.AkaryakitTasimaID = akaryakitTasima.ID;
                                akaryakitTasimaDetayUrun.AkaryakitTasimaDetayID = akaryakitTasimaDetay.ID;
                                akaryakitTasimaDetayUrun.UnID = int.Parse(form["Un_ID" + i + "[]"][j]);
                                akaryakitTasimaDetayUrun.UrunID = int.Parse(form["TasinacakUrun_ID" + i + "[]"][j]);
                                akaryakitTasimaDetayUrun.YuklemeMiktari = int.Parse(form["YuklemeMiktari" + i + "[]"][j]);
                                akaryakitTasimaDetayUrun.OdemeYapanCariGrup = int.Parse(form["Cari_Odeme_Yapan" + i + "[]"][j]);
                                akaryakitTasimaDetayUrun.OdemeYapanCariID = int.Parse(form["Cari_Odeme_YapanID" + i + "[]"][j]);
                                akaryakitTasimaDetayUrun.Ucretlendirme = int.Parse(form["Ucretlendirme_ID" + i + "[]"][j]);
                                akaryakitTasimaDetayUrun.BirimSeferFiyati = int.Parse(form["Birim_SeferFiyat" + i + "[]"][j]);
                                akaryakitTasimaDetayUrun.Durum = true;
                                akaryakitTasimaDetayUrun.FirmaID = 1;  // Değişecek
                                akaryakitTasimaDetayUrun.OlusturmaTarihi = DateTime.Now;
                                akaryakitTasimaDetayUrun.DuzenlemeTarihi = DateTime.Now;
                                akaryakitTasimaDetayUrun.OlusturanId = 1;  // Değişecek
                                akaryakitTasimaDetayUrun.DuzenleyenID = 1;  // Değişecek
                                akaryakitTasimaDetayUrunManager.TAdd(akaryakitTasimaDetayUrun);
                                detayUrunId += akaryakitTasimaDetayUrun.ID + ";";
                                if (akaryakitTasimaDetayUrun.OdemeYapanCariGrup == 1)
                                {
                                    faturaKesenId += akaryakitTasimaDetay.GondericiID + ";";
                                    faturaKesilenId += akaryakitTasimaDetay.AliciID + ";";
                                }
                                else if (akaryakitTasimaDetayUrun.OdemeYapanCariGrup == 2)
                                {
                                    faturaKesenId += akaryakitTasimaDetay.AliciID + ";";
                                    faturaKesilenId += akaryakitTasimaDetay.GondericiID + ";";
                                }
                            }
                            detayUrunId = detayUrunId.Trim(';');
                            faturaKesenId = faturaKesenId.Trim(';');
                            faturaKesilenId = faturaKesilenId.Trim(';');
                            akaryakitFatura.AkaryakitTasimaDetayUrunID = detayUrunId;
                            akaryakitFatura.FaturaKesenID = faturaKesenId;
                            akaryakitFatura.FaturaKesilenID = faturaKesilenId;
                            akaryakitFaturaManager.TAdd(akaryakitFatura);

                            CariHareket cariHareket = new CariHareket {
                                Kategori = 1,
                                TabloAdi="AkaryakitFatura",
                                TabloID=akaryakitFatura.ID,
                                Tutar= ToplamFiyat,
                                Durum =true,
                                FirmaID=1,
                                OlusturanId=1,
                                DuzenleyenID=1,
                                OlusturmaTarihi=DateTime.Now,
                                DuzenlemeTarihi=DateTime.Now,


                            };
                            Helper.CariHareketEkle(cariHareket);



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
            return RedirectToAction("Index");
        }


        public IActionResult TasimaDuzenle(int ID)
        {

            List<Arac> aracListesi = aracManager.GetAllList(x => x.Durum == true);
            List<Kullanicilar> surucuListesi = surucuManager.GetAllList(x => x.Kullanici_Durum == 1);
            List<TasinacakUrun> tasinacakUrun = tasinacakUrunManager.GetAllList(x => x.Durum == true);
            List<UnListesi> UnListesi = unListesiManager.GetAllList(x => x.Durum == 1);
            List<Cari> CariListesi = cariManager.GetAllList(x => x.Durum == 1);
            List<CariUcretlendirme> Ucretlendirme = ucretlendirme.GetAllList(x => x.Durum == true);
            List<AracTip> aracTip = aracTipManager.GetAllList(x => x.Durum == true);
            List<AracTur> aracTur = aracTurManager.GetAllList(x => x.Durum == true);
            List<Birimler> birimler = birimlerManager.GetAllList(x => x.Durum == true);
            List<TasimaTipi> tasimaTipi = tasimaTipiManager.GetAllList(x => x.Durum == true);

            List<Arac> cekiciPlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 1);
            List<Arac> dorsePlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 4);
            List<AracTip> dorseListesi = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID == 4);


            List<Cari> AliciListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 8);
            List<Cari> GondericiListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 6);


            List<Cari_OdemeYapan> CariOdemeYapan = cari_OdemeYapanManager.GetAllList(x => x.Durum == true);


            //ViewBag.Araclar = aracListesi;
            ViewBag.CekiciPlaka = cekiciPlaka;
            ViewBag.DorsePlaka = dorsePlaka;
            ViewBag.DorseListe = dorseListesi;
            ViewBag.Suruculer = surucuListesi;
            ViewBag.TasinacakUrun = tasinacakUrun;
            ViewBag.UnListesi = UnListesi;
            ViewBag.CariListesi = CariListesi;
            ViewBag.aracTip = aracTip;
            ViewBag.aracTur = aracTur;
            ViewBag.birimler = birimler;
            ViewBag.tasimaTipi = tasimaTipi;
            ViewBag.AliciListesi = AliciListesi;
            ViewBag.GondericiListesi = GondericiListesi;
            ViewBag.CariOdemeYapan = CariOdemeYapan;
            ViewBag.Ucretlendirme = Ucretlendirme;
            var adres = adresManager.List();

            var iller = adres.Select(a => new { IL_KODU = a.IL_KODU, Il = a.Il }).Distinct().ToList();

            ViewBag.Iller = iller;

            var combinedQuery = from arac in aracManager.GetAllList((y => y.Durum == true && y.AracTurID == 13 || y.AracTurID == 1))
                                join tur in aracTurManager.GetAllList((y => y.Durum == true)) on arac.AracTurID equals tur.ID
                                select new
                                {
                                    AracPlakasi = arac.Plaka,
                                    AracID = arac.ID,
                                    TurID = tur.ID,
                                    AracTurAdi = tur.Adi
                                };

            ViewBag.Araclar = combinedQuery.ToList();


            List<AkaryakitAracTur> akaryakitAracTur = akaryakitAracTurManager.List();

            ViewBag.AkaryakitAracTur = akaryakitAracTur;
            AkaryakitTasima tasima = akaryakitTasimaManager.GetByID(ID);

            return View(tasima);
        }

        [HttpPost]
        public IActionResult TasimaDuzenle(AkaryakitTasima kayit, IFormCollection form)
        {
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {


                    try
                    {
                        AkaryakitTasima akaryakitTasima = akaryakitTasimaManager.GetByID(kayit.ID);


                        akaryakitTasima.Kullanici1ID = kayit.Kullanici1ID;
                        akaryakitTasima.Kullanici2ID = kayit.Kullanici2ID;
                        akaryakitTasima.Kullanici3ID = kayit.Kullanici3ID;

                        string[] aracIDForm = form["AracID"].ToString().Split('|');
                        int aracID = int.Parse(aracIDForm[0]);
                        akaryakitTasima.AracID = aracID;
                        akaryakitTasima.DorsePlakaID = kayit.DorsePlakaID;
                        akaryakitTasima.DuzenlemeTarihi = DateTime.Now;
                        akaryakitTasima.DuzenleyenID = 1;

                        akaryakitTasima.Goz1Kapasite = kayit.Goz1Kapasite;
                        akaryakitTasima.Goz2Kapasite = kayit.Goz2Kapasite;
                        akaryakitTasima.Goz3Kapasite = kayit.Goz3Kapasite;
                        akaryakitTasima.Goz4Kapasite = kayit.Goz4Kapasite;
                        akaryakitTasima.Goz5Kapasite = kayit.Goz5Kapasite;
                        akaryakitTasima.Goz6Kapasite = kayit.Goz6Kapasite;


                        akaryakitTasima.AracTurID = int.Parse(form["AracTurID"]);
                        akaryakitTasima.ToplamYuklenenMiktar = kayit.ToplamYuklenenMiktar;

                        akaryakitTasima.Goz1UrunID = string.IsNullOrWhiteSpace(form["Goz1UrunID"]) ? 0 : int.Parse(form["Goz1UrunID"]);
                        akaryakitTasima.Goz2UrunID = string.IsNullOrWhiteSpace(form["Goz2UrunID"]) ? 0 : int.Parse(form["Goz2UrunID"]);
                        akaryakitTasima.Goz3UrunID = string.IsNullOrWhiteSpace(form["Goz3UrunID"]) ? 0 : int.Parse(form["Goz3UrunID"]);
                        akaryakitTasima.Goz4UrunID = string.IsNullOrWhiteSpace(form["Goz4UrunID"]) ? 0 : int.Parse(form["Goz4UrunID"]);
                        akaryakitTasima.Goz5UrunID = string.IsNullOrWhiteSpace(form["Goz5UrunID"]) ? 0 : int.Parse(form["Goz5UrunID"]);
                        akaryakitTasima.Goz6UrunID = string.IsNullOrWhiteSpace(form["Goz6UrunID"]) ? 0 : int.Parse(form["Goz6UrunID"]);

                        akaryakitTasimaManager.TUpdate(akaryakitTasima);


                        int kayitSayisi = int.Parse(form["KayitSayisi"]);

                        for (var i = 1; i <= kayitSayisi; i++)
                        {
                            AkaryakitTasimaDetay akaryakitTasimaDetay;
                            AkaryakitFatura akaryakitFatura;
                            try
                            {
                                int detayID = int.Parse(form["detayID" + i + "[]"]);

                                akaryakitTasimaDetay = akaryakitTasimaDetayManager.GetByID(detayID);

                                akaryakitTasimaDetay.GondericiID = int.Parse(form["GondericiCari_ID" + i + "[]"]);
                                akaryakitTasimaDetay.GondericiYuklemeIlID = int.Parse(form["MalYuklemeAdres_IL_ID" + i + "[]"]);
                                akaryakitTasimaDetay.GondericiYuklemeIlceID = int.Parse(form["MalYuklemeAdres_ILCE_ID" + i + "[]"]);
                                akaryakitTasimaDetay.GondericiYuklemeTarihSaat = DateTime.Parse(form["GondericiFirmaTarihSaat" + i + "[]"]);
                                akaryakitTasimaDetay.AliciID = int.Parse(form["AliciCari_ID" + i + "[]"]);
                                akaryakitTasimaDetay.AliciIndirilenIlID = int.Parse(form["IndirilenAdres_IL_ID" + i + "[]"]);
                                akaryakitTasimaDetay.AliciIndirilenIlceID = int.Parse(form["IndirilenAdres_ILCE_ID" + i + "[]"]);
                                akaryakitTasimaDetay.AliciIndirilenTarihSaat = DateTime.Parse(form["AliciFirmaTarihSaat" + i + "[]"]);
                                akaryakitTasimaDetay.NakliyeTutarKDVHaric = int.Parse(form["NakliyeBedelTutar_KDVsiz" + i + "[]"]);
                                akaryakitTasimaDetay.NakliyeKDV = int.Parse(form["NakliyeBedelTutar_KDV" + i + "[]"]);
                                akaryakitTasimaDetay.NakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
                                akaryakitTasimaDetay.NakliyeFiyat = int.Parse(form["Fiyat" + i + "[]"]);

                                akaryakitTasimaDetay.DuzenlemeTarihi = DateTime.Now;
                                akaryakitTasimaDetay.DuzenleyenID = 1;//değişcek
                                akaryakitTasimaDetayManager.TUpdate(akaryakitTasimaDetay);
                                akaryakitFatura = akaryakitFaturaManager.GetAllList(x => x.AkaryakitTasimaID == kayit.ID && x.AkaryakitTasimaDetayID == detayID).SingleOrDefault();
                                akaryakitFatura.FaturaNo = akaryakitTasima.ID + "_" + DateTime.Now; //isteğe göre değiştirilebilir
                                akaryakitFatura.AkaryakitTasimaID = akaryakitTasima.ID;
                                akaryakitFatura.AkaryakitTasimaDetayID = akaryakitTasimaDetay.ID;
                                akaryakitFatura.Durum = true;
                                akaryakitFatura.FirmaID = 1;//değişçek
                                akaryakitFatura.OlusturmaTarihi = DateTime.Now;
                                akaryakitFatura.DuzenlemeTarihi = DateTime.Now;
                                akaryakitFatura.OlusturanId = 1;//değişcek
                                akaryakitFatura.DuzenleyenID = 1;//değişcek
                            }
                            catch (ArgumentNullException)
                            {
                                akaryakitTasimaDetay = new AkaryakitTasimaDetay();
                                akaryakitTasimaDetay.AkaryakitTasimaID = akaryakitTasima.ID;
                                akaryakitTasimaDetay.GondericiID = int.Parse(form["GondericiCari_ID" + i + "[]"]);
                                akaryakitTasimaDetay.GondericiYuklemeIlID = int.Parse(form["MalYuklemeAdres_IL_ID" + i + "[]"]);
                                akaryakitTasimaDetay.GondericiYuklemeIlceID = int.Parse(form["MalYuklemeAdres_ILCE_ID" + i + "[]"]);
                                akaryakitTasimaDetay.GondericiYuklemeTarihSaat = DateTime.Parse(form["GondericiFirmaTarihSaat" + i + "[]"]);
                                akaryakitTasimaDetay.AliciID = int.Parse(form["AliciCari_ID" + i + "[]"]);
                                akaryakitTasimaDetay.AliciIndirilenIlID = int.Parse(form["IndirilenAdres_IL_ID" + i + "[]"]);
                                akaryakitTasimaDetay.AliciIndirilenIlceID = int.Parse(form["IndirilenAdres_ILCE_ID" + i + "[]"]);
                                akaryakitTasimaDetay.AliciIndirilenTarihSaat = DateTime.Parse(form["AliciFirmaTarihSaat" + i + "[]"]);
                                akaryakitTasimaDetay.NakliyeTutarKDVHaric = int.Parse(form["NakliyeBedelTutar_KDVsiz" + i + "[]"]);
                                akaryakitTasimaDetay.NakliyeKDV = int.Parse(form["NakliyeBedelTutar_KDV" + i + "[]"]);
                                akaryakitTasimaDetay.NakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
                                akaryakitTasimaDetay.NakliyeFiyat = int.Parse(form["Fiyat" + i + "[]"]);
                                akaryakitTasimaDetay.Durum = true;
                                akaryakitTasimaDetay.FirmaID = 1;//değişçek
                                akaryakitTasimaDetay.OlusturmaTarihi = DateTime.Now;
                                akaryakitTasimaDetay.DuzenlemeTarihi = DateTime.Now;
                                akaryakitTasimaDetay.OlusturanId = 1;//değişcek
                                akaryakitTasimaDetay.DuzenleyenID = 1;//değişcek
                                akaryakitTasimaDetayManager.TAdd(akaryakitTasimaDetay);
                                akaryakitFatura = new AkaryakitFatura();
                                akaryakitFatura.FaturaNo = akaryakitTasima.ID + "_" + DateTime.Now; //isteğe göre değiştirilebilir
                                akaryakitFatura.AkaryakitTasimaID = akaryakitTasima.ID;
                                akaryakitFatura.AkaryakitTasimaDetayID = akaryakitTasimaDetay.ID;
                                akaryakitFatura.Durum = true;
                                akaryakitFatura.FirmaID = 1;//değişçek
                                akaryakitFatura.OlusturmaTarihi = DateTime.Now;
                                akaryakitFatura.DuzenlemeTarihi = DateTime.Now;
                                akaryakitFatura.OlusturanId = 1;//değişcek
                                akaryakitFatura.DuzenleyenID = 1;//değişcek
                            }


                            string faturaKesenId = "", faturaKesilenId = "";
                            for (var j = 0; j < form["Un_ID" + i + "[]"].Count(); j++)
                            {
                                var detayUrunID = (form["detayUrunID" + i + "[]"]);
                                AkaryakitTasimaDetayUrun akaryakitTasimaDetayUrun;
                                try
                                {
                                    akaryakitTasimaDetayUrun = akaryakitTasimaDetayUrunManager.GetByID(int.Parse(detayUrunID[j]));
                                    akaryakitTasimaDetayUrun.UnID = int.Parse(form["Un_ID" + i + "[]"][j]);
                                    akaryakitTasimaDetayUrun.UrunID = int.Parse(form["TasinacakUrun_ID" + i + "[]"][j]);
                                    akaryakitTasimaDetayUrun.YuklemeMiktari = int.Parse(form["YuklemeMiktari" + i + "[]"][j]);
                                    akaryakitTasimaDetayUrun.OdemeYapanCariGrup = int.Parse(form["Cari_Odeme_Yapan" + i + "[]"][j]);
                                    akaryakitTasimaDetayUrun.OdemeYapanCariID = int.Parse(form["Cari_Odeme_YapanID" + i + "[]"][j]);
                                    akaryakitTasimaDetayUrun.Ucretlendirme = int.Parse(form["Ucretlendirme_ID" + i + "[]"][j]);
                                    akaryakitTasimaDetayUrun.BirimSeferFiyati = int.Parse(form["Birim_SeferFiyat" + i + "[]"][j]);

                                    akaryakitTasimaDetayUrun.DuzenlemeTarihi = DateTime.Now;
                                    akaryakitTasimaDetayUrun.DuzenleyenID = 1;  // Değişecek

                                    akaryakitTasimaDetayUrunManager.TUpdate(akaryakitTasimaDetayUrun);
                                }
                                catch
                                {
                                    akaryakitTasimaDetayUrun = new AkaryakitTasimaDetayUrun();
                                    akaryakitTasimaDetayUrun.AkaryakitTasimaID = akaryakitTasima.ID;
                                    akaryakitTasimaDetayUrun.AkaryakitTasimaDetayID = akaryakitTasimaDetay.ID;

                                    akaryakitTasimaDetayUrun.UnID = int.Parse(form["Un_ID" + i + "[]"][j]);
                                    akaryakitTasimaDetayUrun.UrunID = int.Parse(form["TasinacakUrun_ID" + i + "[]"][j]);
                                    akaryakitTasimaDetayUrun.YuklemeMiktari = int.Parse(form["YuklemeMiktari" + i + "[]"][j]);
                                    akaryakitTasimaDetayUrun.OdemeYapanCariGrup = int.Parse(form["Cari_Odeme_Yapan" + i + "[]"][j]);
                                    akaryakitTasimaDetayUrun.OdemeYapanCariID = int.Parse(form["Cari_Odeme_YapanID" + i + "[]"][j]);
                                    akaryakitTasimaDetayUrun.Ucretlendirme = int.Parse(form["Ucretlendirme_ID" + i + "[]"][j]);
                                    akaryakitTasimaDetayUrun.BirimSeferFiyati = int.Parse(form["Birim_SeferFiyat" + i + "[]"][j]);

                                    akaryakitTasimaDetayUrun.Durum = true;
                                    akaryakitTasimaDetayUrun.FirmaID = 1;  // Değişecek
                                    akaryakitTasimaDetayUrun.OlusturmaTarihi = DateTime.Now;
                                    akaryakitTasimaDetayUrun.DuzenlemeTarihi = DateTime.Now;
                                    akaryakitTasimaDetayUrun.OlusturanId = 1;  // Değişecek
                                    akaryakitTasimaDetayUrun.DuzenleyenID = 1;  // Değişecek


                                    akaryakitTasimaDetayUrunManager.TAdd(akaryakitTasimaDetayUrun);
                                    akaryakitFatura.AkaryakitTasimaDetayUrunID += ";" + akaryakitTasimaDetayUrun.ID + ";";
                                }

                                if (akaryakitTasimaDetayUrun.OdemeYapanCariGrup == 1)
                                {
                                    faturaKesenId += akaryakitTasimaDetay.GondericiID + ";";
                                    faturaKesilenId += akaryakitTasimaDetay.AliciID + ";";
                                }
                                else if (akaryakitTasimaDetayUrun.OdemeYapanCariGrup == 2)
                                {
                                    faturaKesenId += akaryakitTasimaDetay.AliciID + ";";
                                    faturaKesilenId += akaryakitTasimaDetay.GondericiID + ";";
                                }
                            }


                            akaryakitFatura.AkaryakitTasimaDetayUrunID = akaryakitFatura.AkaryakitTasimaDetayUrunID.Trim(';');
                            faturaKesenId = faturaKesenId.Trim(';');
                            faturaKesilenId = faturaKesilenId.Trim(';');
                            akaryakitFatura.FaturaKesenID = faturaKesenId;
                            akaryakitFatura.FaturaKesilenID = faturaKesilenId;
                            if (akaryakitFatura.ID != 0)
                                akaryakitFaturaManager.TUpdate(akaryakitFatura);
                            else
                                akaryakitFaturaManager.TAdd(akaryakitFatura);
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
            return RedirectToAction("Index");
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
                        AkaryakitTasima item = akaryakitTasimaManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        akaryakitTasimaManager.TUpdate(item);

                        List<AkaryakitTasimaDetay> itemDetay = akaryakitTasimaDetayManager.GetAllList(x => x.AkaryakitTasimaID == int.Parse(form["ID"]));
                        foreach (var x in itemDetay)
                        {
                            x.Durum = false;
                            akaryakitTasimaDetayManager.TUpdate(x);

                        }
                        List<AkaryakitTasimaDetayUrun> itemDetayUrun = akaryakitTasimaDetayUrunManager.GetAllList(x => x.AkaryakitTasimaID == int.Parse(form["ID"]));
                        foreach (var x in itemDetayUrun)
                        {
                            x.Durum = false;
                            akaryakitTasimaDetayUrunManager.TUpdate(x);

                        }
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

        public IActionResult FaturaGoster(int ID)
        {
            AkaryakitTasima tasima = akaryakitTasimaManager.GetByID(ID);
            List<AkaryakitTasimaDetay> tasimaDetay = akaryakitTasimaDetayManager.GetAllList(x => x.AkaryakitTasimaID == ID);
            List<AkaryakitTasimaDetayUrun> tasimaDetayUrun = akaryakitTasimaDetayUrunManager.GetAllList(x => x.AkaryakitTasimaID == ID);

            List<AkaryakitFatura> faturaList = akaryakitFaturaManager.GetAllList(x => x.AkaryakitTasimaID == ID);

            ViewBag.Tasima = tasima;
            ViewBag.TasimaDetay = tasimaDetay;
            ViewBag.TasimaDetayUrun = tasimaDetayUrun;
            ViewBag.faturaList = faturaList;


            return View();


        }
    }
}
