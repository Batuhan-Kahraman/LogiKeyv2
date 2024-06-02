using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.EntityFrameworks;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class AkaryakitTasimaController : BaseController
    {
        #region tanimlamalar
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
        CariHareketManager cariHareketManager = new CariHareketManager(new EFCariHareketRepository());
        #endregion

        public IActionResult Index()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");

            var combinedQuery = from tasima in akaryakitTasimaManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2))
                                join arac in aracManager.List() on tasima.AracID equals arac.ID
                                join dorse in aracManager.List() on tasima.DorsePlakaID equals dorse.ID
                                join surucu1 in surucuManager.List() on tasima.Kullanici1ID equals surucu1.Kullanici_ID
                                select new TasimaModel
                                {
                                    Tasima = tasima,
                                    Arac = arac,
                                    Dorse = dorse,
                                    Surucu = surucu1
                                };



            List<TasimaModel> combinedList = combinedQuery.ToList();


            return View(combinedList);
        }

        public IActionResult TasimaEkle(int ModulID = 0)
        {
            HttpContext.Session.SetInt32("MenuModulID", ModulID);
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Arac> aracListesi = aracManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<Kullanicilar> surucuListesi = surucuManager.GetAllList(x => x.Kullanici_Durum == 1 && x.KullaniciGrup_ID == 2 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<TasinacakUrun> tasinacakUrun = tasinacakUrunManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<UnListesi> UnListesi = unListesiManager.GetAllList(x => x.Durum == 1 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<Cari> CariListesi = cariManager.GetAllList(x => x.Durum == 1 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<CariUcretlendirme> Ucretlendirme = ucretlendirme.GetAllList(x => x.Durum == true && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<AracTip> aracTip = aracTipManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<AracTur> aracTur = aracTurManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<Birimler> birimler = birimlerManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<TasimaTipi> tasimaTipi = tasimaTipiManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));

            List<Arac> cekiciPlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 1 && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<Arac> dorsePlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 4 && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<AracTip> dorseListesi = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID == 4 && (x.FirmaID == FirmaID || x.FirmaID == -2));


            List<Cari> AliciListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 8 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<Cari> GondericiListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 6 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));


            List<Cari_OdemeYapan> CariOdemeYapan = cari_OdemeYapanManager.GetAllList(x => x.Durum == true && (x.Firma_ID == FirmaID || x.Firma_ID == -2));


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

            var combinedQuery = from arac in aracManager.GetAllList((y => y.Durum == true && (y.FirmaID == FirmaID || y.FirmaID == -2) && y.AracTurID == 13 || y.AracTurID == 1))
                                join tur in aracTurManager.GetAllList((y => y.Durum == true && (y.FirmaID == FirmaID || y.FirmaID == -2))) on arac.AracTurID equals tur.ID
                                select new
                                {
                                    AracPlakasi = arac.Plaka,
                                    AracID = arac.ID,
                                    TurID = tur.ID,
                                    AracTurAdi = tur.Adi,
                                    Kapasite = arac.Goz1 + arac.Goz2 + arac.Goz3 + arac.Goz4 + arac.Goz5 + arac.Goz6
                                };

            ViewBag.Araclar = combinedQuery.ToList();


            List<AkaryakitAracTur> akaryakitAracTur = akaryakitAracTurManager.GetAllList(x => x.FirmaID == FirmaID);

            ViewBag.AkaryakitAracTur = akaryakitAracTur;
            return View();
        }

        [HttpPost]
        public IActionResult TasimaEkle(AkaryakitTasima akaryakitTasima, IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {


                        var surucuKoparmaIslemi = Helper.AkaryakitSurucuKopar(akaryakitTasima.Kullanici1ID, akaryakitTasima.Kullanici2ID, akaryakitTasima.Kullanici3ID);
                        if (surucuKoparmaIslemi == true)
                        {
                            int ToplamFiyat = 0;
                            string[] aracIDForm = form["AracID"].ToString().Split('|');
                            int aracID = int.Parse(aracIDForm[0]);
                            akaryakitTasima.AracID = aracID;
                            akaryakitTasima.Goz1UrunID = string.IsNullOrWhiteSpace(form["Goz1UrunID"]) ? 0 : int.Parse(form["Goz1UrunID"]);
                            akaryakitTasima.Goz2UrunID = string.IsNullOrWhiteSpace(form["Goz2UrunID"]) ? 0 : int.Parse(form["Goz2UrunID"]);
                            akaryakitTasima.Goz3UrunID = string.IsNullOrWhiteSpace(form["Goz3UrunID"]) ? 0 : int.Parse(form["Goz3UrunID"]);
                            akaryakitTasima.Goz4UrunID = string.IsNullOrWhiteSpace(form["Goz4UrunID"]) ? 0 : int.Parse(form["Goz4UrunID"]);
                            akaryakitTasima.Goz5UrunID = string.IsNullOrWhiteSpace(form["Goz5UrunID"]) ? 0 : int.Parse(form["Goz5UrunID"]);
                            akaryakitTasima.Goz6UrunID = string.IsNullOrWhiteSpace(form["Goz6UrunID"]) ? 0 : int.Parse(form["Goz6UrunID"]);
                            akaryakitTasima.Durum = true;
                            akaryakitTasima.FirmaID = FirmaID;//değişçek
                            akaryakitTasima.OlusturmaTarihi = DateTime.Now;
                            akaryakitTasima.DuzenlemeTarihi = DateTime.Now;
                            akaryakitTasima.OlusturanId = KullaniciID;//değişcek
                            akaryakitTasima.DuzenleyenID = KullaniciID;//değişcek

                            akaryakitTasima.AracTurID = int.Parse(form["AracTurID"]);
                            akaryakitTasimaManager.TAdd(akaryakitTasima);
                            int kayitSayisi = int.Parse(form["KayitSayisi"]);
                            int NakliyeToplam = 0;
                            for (var i = 1; i <= kayitSayisi; i++)
                            {
                                NakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
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
                                akaryakitTasimaDetay.FirmaID = FirmaID;//değişçek
                                akaryakitTasimaDetay.OlusturmaTarihi = DateTime.Now;
                                akaryakitTasimaDetay.DuzenlemeTarihi = DateTime.Now;
                                akaryakitTasimaDetay.OlusturanId = KullaniciID;//değişcek
                                akaryakitTasimaDetay.DuzenleyenID = KullaniciID;//değişcek
                                akaryakitTasimaDetayManager.TAdd(akaryakitTasimaDetay);
                                ToplamFiyat += akaryakitTasimaDetay.NakliyeFiyat;
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
                                    akaryakitTasimaDetayUrun.FirmaID = FirmaID;  // Değişecek
                                    akaryakitTasimaDetayUrun.OlusturmaTarihi = DateTime.Now;
                                    akaryakitTasimaDetayUrun.DuzenlemeTarihi = DateTime.Now;
                                    akaryakitTasimaDetayUrun.OlusturanId = KullaniciID;  // Değişecek
                                    akaryakitTasimaDetayUrun.DuzenleyenID = KullaniciID;  // Değişecek
                                    akaryakitTasimaDetayUrunManager.TAdd(akaryakitTasimaDetayUrun);
                                    //detayUrunId += akaryakitTasimaDetayUrun.ID + ";";

                                    AkaryakitFatura akaryakitFatura = new AkaryakitFatura();
                                    akaryakitFatura.FaturaNo = akaryakitTasima.ID + "_" + DateTime.Now; //isteğe göre değiştirilebilir
                                    akaryakitFatura.AkaryakitTasimaID = akaryakitTasima.ID;
                                    akaryakitFatura.AkaryakitTasimaDetayID = akaryakitTasimaDetay.ID;
                                    akaryakitFatura.Odeme = 0;
                                    akaryakitFatura.Durum = true;
                                    akaryakitFatura.FirmaID = FirmaID;//değişçek
                                    akaryakitFatura.OlusturmaTarihi = DateTime.Now;
                                    akaryakitFatura.DuzenlemeTarihi = DateTime.Now;
                                    akaryakitFatura.OlusturanId = KullaniciID;//değişcek
                                    akaryakitFatura.DuzenleyenID = KullaniciID;//değişcek
                                    if (akaryakitTasimaDetayUrun.OdemeYapanCariGrup == 1)
                                    {

                                        //detayUrunId = detayUrunId.Trim(';');
                                        akaryakitFatura.AkaryakitTasimaDetayUrunID = akaryakitTasimaDetayUrun.ID;
                                        //akaryakitFatura.FaturaKesenID = akaryakitTasimaDetay.GondericiID;
                                        akaryakitFatura.FaturaKesenID = FirmaID;
                                        akaryakitFatura.FaturaKesilenID = akaryakitTasimaDetay.AliciID;
                                        akaryakitFaturaManager.TAdd(akaryakitFatura);

                                    }
                                    else if (akaryakitTasimaDetayUrun.OdemeYapanCariGrup == 2)
                                    {
                                        //detayUrunId = detayUrunId.Trim(';');
                                        akaryakitFatura.AkaryakitTasimaDetayUrunID = akaryakitTasimaDetayUrun.ID;
                                        //akaryakitFatura.FaturaKesenID = akaryakitTasimaDetay.AliciID;
                                        akaryakitFatura.FaturaKesenID = FirmaID;
                                        akaryakitFatura.FaturaKesilenID = akaryakitTasimaDetay.GondericiID;
                                        akaryakitFaturaManager.TAdd(akaryakitFatura);

                                    }

                                    AkaryakitCariHareket akaryakitCariHareket = new AkaryakitCariHareket
                                    {
                                        CariID = akaryakitFatura.FaturaKesenID,
                                        FaturaID = akaryakitFatura.ID,
                                        OdemeID = 0,
                                        PlakaID = akaryakitTasima.AracID,
                                        Tutar = ToplamFiyat,
                                        Durum = true,
                                        FirmaID = FirmaID,
                                        OlusturanId = KullaniciID,
                                        DuzenleyenID = KullaniciID,
                                        OlusturmaTarihi = DateTime.Now,
                                        DuzenlemeTarihi = DateTime.Now
                                    };
                                    Helper.AkaryakitCariHareketEkle(akaryakitCariHareket);

                                    CariHareket cariHareket = new CariHareket();
                                    cariHareket.CariID = akaryakitFatura.FaturaKesilenID;
                                    cariHareket.AkaryakitFaturaID = akaryakitTasima.ID;
                                    cariHareket.AkaryakitFaturaTutar = NakliyeToplam;
                                    cariHareket.Durum = true;
                                    cariHareket.FirmaID = FirmaID;
                                    cariHareket.OlusturanId = KullaniciID;
                                    cariHareket.DuzenleyenID = KullaniciID;
                                    cariHareket.OlusturmaTarihi = DateTime.Now;
                                    cariHareket.DuzenlemeTarihi = DateTime.Now;
                                    cariHareketManager.TAdd(cariHareket);

                                    Helper.CariBorcAlacakKalanHesapla(akaryakitFatura.FaturaKesilenID);

                                }

                            }



                            TempData["Msg"] = "İşlem başarılı.";
                            TempData["Bgcolor"] = "green";
                        }
                        else
                        {
                            TempData["Msg"] = "Sürücü koparma işlemi başarısız oldu.";
                            TempData["Bgcolor"] = "green";
                            transaction.Rollback();
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

        public IActionResult TasimaDuzenle(int ID, int ModulID = 0)
        {

            HttpContext.Session.SetInt32("MenuModulID", ModulID);
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Arac> aracListesi = aracManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<Kullanicilar> surucuListesi = surucuManager.GetAllList(x => x.Kullanici_Durum == 1 && x.KullaniciGrup_ID == 2 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<TasinacakUrun> tasinacakUrun = tasinacakUrunManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<UnListesi> UnListesi = unListesiManager.GetAllList(x => x.Durum == 1 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<Cari> CariListesi = cariManager.GetAllList(x => x.Durum == 1 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<CariUcretlendirme> Ucretlendirme = ucretlendirme.GetAllList(x => x.Durum == true && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<AracTip> aracTip = aracTipManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<AracTur> aracTur = aracTurManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<Birimler> birimler = birimlerManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<TasimaTipi> tasimaTipi = tasimaTipiManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));

            List<Arac> cekiciPlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 1 && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<Arac> dorsePlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 4 && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<AracTip> dorseListesi = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID == 4 && (x.FirmaID == FirmaID || x.FirmaID == -2));


            List<Cari> AliciListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 8 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
            List<Cari> GondericiListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 6 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));


            List<Cari_OdemeYapan> CariOdemeYapan = cari_OdemeYapanManager.GetAllList(x => x.Durum == true && (x.Firma_ID == FirmaID || x.Firma_ID == -2));


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

            var combinedQuery = from arac in aracManager.GetAllList((y => y.Durum == true && (y.FirmaID == FirmaID || y.FirmaID == -2) && y.AracTurID == 13 || y.AracTurID == 1))
                                join tur in aracTurManager.GetAllList((y => y.Durum == true && (y.FirmaID == FirmaID || y.FirmaID == -2))) on arac.AracTurID equals tur.ID
                                select new
                                {
                                    AracPlakasi = arac.Plaka,
                                    AracID = arac.ID,
                                    TurID = tur.ID,
                                    AracTurAdi = tur.Adi,
                                    Kapasite = arac.Goz1 + arac.Goz2 + arac.Goz3 + arac.Goz4 + arac.Goz5 + arac.Goz6
                                };

            ViewBag.Araclar = combinedQuery.ToList();


            List<AkaryakitAracTur> akaryakitAracTur = akaryakitAracTurManager.GetAllList(x => x.FirmaID == FirmaID);

            ViewBag.AkaryakitAracTur = akaryakitAracTur;
            AkaryakitTasima tasima = akaryakitTasimaManager.GetByID(ID);

            return View(tasima);
        }

        [HttpPost]
        public IActionResult TasimaDuzenle(AkaryakitTasima kayit, IFormCollection form)
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {


                    try
                    {
                        AkaryakitTasima akaryakitTasima = akaryakitTasimaManager.GetByID(kayit.ID);
                        var surucuKoparmaIslemi = Helper.AkaryakitSurucuKopar(kayit.Kullanici1ID, kayit.Kullanici2ID, kayit.Kullanici3ID);
                        if (surucuKoparmaIslemi == true)
                        {

                            akaryakitTasima.Kullanici1ID = kayit.Kullanici1ID;
                            akaryakitTasima.Kullanici2ID = kayit.Kullanici2ID;
                            akaryakitTasima.Kullanici3ID = kayit.Kullanici3ID;

                            string[] aracIDForm = form["AracID"].ToString().Split('|');
                            int aracID = int.Parse(aracIDForm[0]);
                            akaryakitTasima.AracID = aracID;
                            akaryakitTasima.DorsePlakaID = kayit.DorsePlakaID;
                            akaryakitTasima.DuzenlemeTarihi = DateTime.Now;
                            akaryakitTasima.DuzenleyenID = KullaniciID;

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


                            List<CariHareket> cariHareketEski = cariHareketManager.GetAllList(x => x.AkaryakitFaturaID == akaryakitTasima.ID);
                            foreach (var silinecek in cariHareketEski)
                            {
                                cariHareketManager.TDelete(silinecek);
                            }
                            int kayitSayisi = int.Parse(form["KayitSayisi"]);

                            int ToplamFiyat = 0;
                            int eskiNakliyeToplam = 0, yeniNakliyeToplam = 0;
                            for (var i = 1; i <= kayitSayisi; i++)
                            {
                                AkaryakitTasimaDetay akaryakitTasimaDetay;
                                AkaryakitFatura akaryakitFatura;
                                try
                                {
                                    int detayID = int.Parse(form["detayID" + i + "[]"]);

                                    akaryakitTasimaDetay = akaryakitTasimaDetayManager.GetByID(detayID);

                                    eskiNakliyeToplam = akaryakitTasimaDetay.NakliyeToplam;
                                    yeniNakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);

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
                                    akaryakitTasimaDetay.DuzenleyenID = KullaniciID;//değişcek

                                    akaryakitTasimaDetayManager.TUpdate(akaryakitTasimaDetay);
                                    ToplamFiyat += akaryakitTasimaDetay.NakliyeFiyat;

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
                                    akaryakitTasimaDetay.FirmaID = FirmaID;//değişçek
                                    akaryakitTasimaDetay.OlusturmaTarihi = DateTime.Now;
                                    akaryakitTasimaDetay.DuzenlemeTarihi = DateTime.Now;
                                    akaryakitTasimaDetay.OlusturanId = KullaniciID;//değişcek
                                    akaryakitTasimaDetay.DuzenleyenID = KullaniciID;//değişcek
                                    akaryakitTasimaDetayManager.TAdd(akaryakitTasimaDetay);

                                    yeniNakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
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
                                        akaryakitTasimaDetayUrun.DuzenleyenID = KullaniciID;  // Değişecek

                                        akaryakitTasimaDetayUrunManager.TUpdate(akaryakitTasimaDetayUrun);
                                        akaryakitFatura = akaryakitFaturaManager.GetAllList(x => x.AkaryakitTasimaID == kayit.ID && x.AkaryakitTasimaDetayID == akaryakitTasimaDetayUrun.AkaryakitTasimaDetayID && x.AkaryakitTasimaDetayUrunID == akaryakitTasimaDetayUrun.ID).SingleOrDefault();
                                        akaryakitFatura.FaturaNo = akaryakitTasima.ID + "_" + DateTime.Now; //isteğe göre değiştirilebilir
                                        akaryakitFatura.AkaryakitTasimaID = akaryakitTasima.ID;
                                        akaryakitFatura.AkaryakitTasimaDetayID = akaryakitTasimaDetay.ID;
                                        akaryakitFatura.Durum = true;
                                        akaryakitFatura.FirmaID = FirmaID;
                                        akaryakitFatura.OlusturmaTarihi = DateTime.Now;
                                        akaryakitFatura.DuzenlemeTarihi = DateTime.Now;
                                        akaryakitFatura.OlusturanId = KullaniciID;
                                        akaryakitFatura.DuzenleyenID = KullaniciID;
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
                                        akaryakitTasimaDetayUrun.FirmaID = FirmaID;  
                                        akaryakitTasimaDetayUrun.OlusturmaTarihi = DateTime.Now;
                                        akaryakitTasimaDetayUrun.DuzenlemeTarihi = DateTime.Now;
                                        akaryakitTasimaDetayUrun.OlusturanId = KullaniciID;
                                        akaryakitTasimaDetayUrun.DuzenleyenID = KullaniciID; 

                                        akaryakitTasimaDetayUrunManager.TAdd(akaryakitTasimaDetayUrun);

                                        akaryakitFatura = new AkaryakitFatura();
                                        akaryakitFatura.FaturaNo = akaryakitTasima.ID + "_" + DateTime.Now; //isteğe göre değiştirilebilir
                                        akaryakitFatura.AkaryakitTasimaID = akaryakitTasima.ID;
                                        akaryakitFatura.AkaryakitTasimaDetayID = akaryakitTasimaDetay.ID;
                                        akaryakitFatura.Durum = true;
                                        akaryakitFatura.FirmaID = FirmaID;//değişçek
                                        akaryakitFatura.OlusturmaTarihi = DateTime.Now;
                                        akaryakitFatura.DuzenlemeTarihi = DateTime.Now;
                                        akaryakitFatura.OlusturanId = KullaniciID;//değişcek
                                        akaryakitFatura.DuzenleyenID = KullaniciID;//değişcek
                                        akaryakitFatura.AkaryakitTasimaDetayUrunID = akaryakitTasimaDetayUrun.ID;
                                    }

                                    if (akaryakitTasimaDetayUrun.OdemeYapanCariGrup == 1)
                                    {

                                        //akaryakitFatura.FaturaKesenID = akaryakitTasimaDetay.GondericiID;
                                        akaryakitFatura.FaturaKesenID = FirmaID;
                                        akaryakitFatura.FaturaKesilenID = akaryakitTasimaDetay.AliciID;
                                    }
                                    else if (akaryakitTasimaDetayUrun.OdemeYapanCariGrup == 2)
                                    {
                                        //akaryakitFatura.FaturaKesenID = akaryakitTasimaDetay.AliciID;
                                        akaryakitFatura.FaturaKesenID = FirmaID;
                                        akaryakitFatura.FaturaKesilenID = akaryakitTasimaDetay.GondericiID;
                                    }
                                    if (akaryakitFatura.ID != 0)
                                    {
                                        akaryakitFaturaManager.TUpdate(akaryakitFatura);
                                        Helper.AkaryakitCariHareketTemizle(akaryakitFatura.ID);
                                    }
                                    else
                                        akaryakitFaturaManager.TAdd(akaryakitFatura);



                                    AkaryakitCariHareket cariHareket1 = new AkaryakitCariHareket
                                    {
                                        CariID = akaryakitFatura.FaturaKesenID,
                                        FaturaID = akaryakitFatura.ID,
                                        OdemeID = 0,
                                        PlakaID = akaryakitTasima.AracID,
                                        Tutar = ToplamFiyat,
                                        Durum = true,
                                        FirmaID = FirmaID,
                                        OlusturanId = KullaniciID,
                                        DuzenleyenID = KullaniciID,
                                        OlusturmaTarihi = DateTime.Now,
                                        DuzenlemeTarihi = DateTime.Now
                                    };
                                    Helper.AkaryakitCariHareketEkle(cariHareket1);

                                    CariHareket cariHareket = new CariHareket();
                                    cariHareket.CariID = akaryakitFatura.FaturaKesilenID;
                                    cariHareket.AkaryakitFaturaID = akaryakitTasima.ID;
                                    cariHareket.AkaryakitFaturaTutar = yeniNakliyeToplam;
                                    cariHareket.Durum = true;
                                    cariHareket.FirmaID = FirmaID;
                                    cariHareket.OlusturanId = KullaniciID;
                                    cariHareket.DuzenleyenID = KullaniciID;
                                    cariHareket.OlusturmaTarihi = DateTime.Now;
                                    cariHareket.DuzenlemeTarihi = DateTime.Now;
                                    cariHareketManager.TAdd(cariHareket);

                                    Helper.CariBorcAlacakKalanHesapla(akaryakitFatura.FaturaKesilenID);


                                }



                            }



                            TempData["Msg"] = "İşlem başarılı.";
                            TempData["Bgcolor"] = "green";
                        }
                        else
                        {

                            TempData["Msg"] = "Sürücü koparma işlemi başarısız";
                            TempData["Bgcolor"] = "red";
                            transaction.Rollback();
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
                        AkaryakitTasima item = akaryakitTasimaManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        akaryakitTasimaManager.TUpdate(item);

                        List<AkaryakitTasimaDetay> itemDetay = akaryakitTasimaDetayManager.GetAllList(x => x.AkaryakitTasimaID == int.Parse(form["ID"]));
                        foreach (var x in itemDetay)
                        {
                            x.Durum = false;

                            x.FirmaID = FirmaID;
                            x.DuzenlemeTarihi = DateTime.Now;
                            x.DuzenleyenID = KullaniciID;
                            akaryakitTasimaDetayManager.TUpdate(x);

                        }
                        List<AkaryakitTasimaDetayUrun> itemDetayUrun = akaryakitTasimaDetayUrunManager.GetAllList(x => x.AkaryakitTasimaID == int.Parse(form["ID"]));
                        foreach (var x in itemDetayUrun)
                        {
                            x.Durum = false;
                            x.FirmaID = FirmaID;
                            x.DuzenlemeTarihi = DateTime.Now;
                            x.DuzenleyenID = KullaniciID;
                            akaryakitTasimaDetayUrunManager.TUpdate(x);
                            List<CariHareket> hareketler = cariHareketManager.GetAllList(y => y.CariID == x.OdemeYapanCariID && y.AkaryakitFaturaID== int.Parse(form["ID"]));
                            foreach (var item1 in hareketler)
                            {
                                item1.Durum = false;
                                item1.DuzenlemeTarihi = DateTime.Now;
                                item1.DuzenleyenID = KullaniciID;
                                cariHareketManager.TUpdate(item1);
                            }
                            Helper.CariBorcAlacakKalanHesapla(x.OdemeYapanCariID);

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
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            AkaryakitTasima tasima = akaryakitTasimaManager.GetByID(ID);
            List<AkaryakitTasimaDetay> tasimaDetay = akaryakitTasimaDetayManager.GetAllList(x => x.AkaryakitTasimaID == ID && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<AkaryakitTasimaDetayUrun> tasimaDetayUrun = akaryakitTasimaDetayUrunManager.GetAllList(x => x.AkaryakitTasimaID == ID && (x.FirmaID == FirmaID || x.FirmaID == -2));

            List<AkaryakitFatura> faturaList = akaryakitFaturaManager.GetAllList(x => x.AkaryakitTasimaID == ID && (x.FirmaID == FirmaID || x.FirmaID == -2));

            ViewBag.GrupFatura = akaryakitFaturaManager.GetAllList(x => x.AkaryakitTasimaID == ID && (x.FirmaID == FirmaID || x.FirmaID == -2))
             .GroupBy(x => x.FaturaKesenID)
             .Select(group => group.Key)
             .ToList();

            ViewBag.Tasima = tasima;
            ViewBag.TasimaDetay = tasimaDetay;
            ViewBag.TasimaDetayUrun = tasimaDetayUrun;
            ViewBag.Fatura = faturaList;

            ViewBag.FaturaID = ID;

            return View();


        }

        public IActionResult Odeme(int FaturaID)
        {
            AkaryakitFatura fatura = akaryakitFaturaManager.GetByID(FaturaID);
            return View(fatura);
        }
        [HttpPost]
        public IActionResult Odeme(AkaryakitFatura fatura)
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            AkaryakitFatura kayit = akaryakitFaturaManager.GetByID(fatura.ID);
            kayit.Odeme = fatura.Odeme;
            kayit.FirmaID = FirmaID;
            kayit.DuzenlemeTarihi = DateTime.Now;
            kayit.DuzenleyenID = KullaniciID;
            akaryakitFaturaManager.TUpdate(kayit);
            return RedirectToAction("CariHareket", "Cari", new { CariID = kayit.FaturaKesenID });
        }

        public IActionResult Gorsel(int ID)
        {

            AkaryakitTasima kayit = akaryakitTasimaManager.GetByID(ID);
            return View(kayit);
        }

    }
}
