using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.EntityFrameworks;
using EntityLayer.Concrate;
using Google.Protobuf.WellKnownTypes;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class YurtDisiTasimaController : BaseController
    {
        #region tanimlamalar
        AracManager aracManager = new AracManager(new EFAracRepository());
        YurtDisiTasimaEvraklarManager evrakManager = new YurtDisiTasimaEvraklarManager(new EFYurtDisiTasimaEvraklarRepository());
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
        YurtDisiTasimaManager YurtDisiTasimaManager = new YurtDisiTasimaManager(new EFYurtDisiTasimaRepository());
        YurtDisiTasimaMasraflarManager YurtDisiTasimaMasraflarManager = new YurtDisiTasimaMasraflarManager(new EFYurtDisiTasimaMasraflarRepository());
        YurtDisiTasimaDetayManager YurtDisiTasimaDetayManager = new YurtDisiTasimaDetayManager(new EFYurtDisiTasimaDetayRepository());
        YurtDisiTasimaDetayUrunManager YurtDisiTasimaDetayUrunManager = new YurtDisiTasimaDetayUrunManager(new EFYurtDisiTasimaDetayUrunRepository());
        YurtDisiFaturaManager YurtDisiFaturaManager = new YurtDisiFaturaManager(new EFYurtDisiFaturaRepository());
        YurtDisiAracTurManager YurtDisiAracTurManager = new YurtDisiAracTurManager(new EFYurtDisiAracTurRepository());
        CariHareketManager cariHareketManager = new CariHareketManager(new EFCariHareketRepository());
        UlkeParaBirimManager ulkeParaBirimManager = new UlkeParaBirimManager(new EFUlkeParaBirimRepository());
        #endregion

        public IActionResult Index()
        {


            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            var combinedQuery = from tasima in YurtDisiTasimaManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2))
                                join arac in aracManager.GetAllList((y => y.Durum == true && (y.FirmaID == FirmaID || y.FirmaID ==-2))) on tasima.AracID equals arac.ID
                                join surucu1 in surucuManager.GetAllList((y => y.Kullanici_Durum == 1 && (y.Firma_ID == FirmaID || y.Firma_ID == -2))) on tasima.Kullanici1ID equals surucu1.Kullanici_ID
                                select new YurtDisiTasimaModel { Tasima = tasima, Arac = arac, Surucu = surucu1 };

            List<YurtDisiTasimaModel> combinedList = combinedQuery.ToList();
            return View(combinedList);
        }

        public IActionResult TasimaEkle()
        {
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
            List<UlkeParaBirim> paraBirim = ulkeParaBirimManager.GetAllList(x => x.Durum == 1 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));


            ViewBag.ParaBirimleri = paraBirim;
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

            var combinedQuery = from arac in aracManager.GetAllList((y => y.Durum == true && (y.FirmaID == FirmaID|| y.FirmaID == -2) && y.AracTurID != 13 && y.AracTurID != 1))
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


            List<YurtDisiAracTur> YurtDisiAracTur = YurtDisiAracTurManager.GetAllList(x => x.FirmaID == FirmaID || x.FirmaID == -2);

            ViewBag.YurtDisiAracTur = YurtDisiAracTur;
            return View();
        }

        [HttpPost]
        public IActionResult TasimaEkle(YurtDisiTasima YurtDisiTasima, IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {


                        var surucuKoparmaIslemi = Helper.YurtDisiSurucuKopar(YurtDisiTasima.Kullanici1ID, YurtDisiTasima.Kullanici2ID, YurtDisiTasima.Kullanici3ID);
                        if (surucuKoparmaIslemi == true)
                        {
                            int ToplamFiyat = 0;
                            string[] aracIDForm = form["AracID"].ToString().Split('|');
                            int aracID = int.Parse(aracIDForm[0]);
                            YurtDisiTasima.AracID = aracID;
                            YurtDisiTasima.Durum = true;
                            
                            YurtDisiTasima.FirmaID = FirmaID;
                            YurtDisiTasima.OlusturmaTarihi = DateTime.Now;
                            YurtDisiTasima.DuzenlemeTarihi = DateTime.Now;
                            YurtDisiTasima.OlusturanId = KullaniciID;
                            YurtDisiTasima.DuzenleyenID = KullaniciID;

                            YurtDisiTasima.AracTurID = int.Parse(form["AracTurID"]);
                            YurtDisiTasimaManager.TAdd(YurtDisiTasima);


                            int evrakSayisi = form["Evraklar[]"].Count();
                            for (int i = 0; i < evrakSayisi; i++)
                            {

                                YurtDisiTasimaEvraklar tasimaEvraklar = new YurtDisiTasimaEvraklar();
                                tasimaEvraklar.YurtDisiTasimaID = YurtDisiTasima.ID;
                                tasimaEvraklar.EvrakID = int.Parse(form["Evraklar[]"][i]);
                                tasimaEvraklar.EvraklarVerilisTarihi = DateTime.Parse(form["EvraklarVerilisTarihi[]"][i]);
                                tasimaEvraklar.EvraklarGecerlilikTarihi = DateTime.Parse(form["EvraklarGecerlilikTarihi[]"][i]);
                                tasimaEvraklar.Durum = true;
                                tasimaEvraklar.FirmaID = FirmaID;
                                tasimaEvraklar.OlusturmaTarihi = DateTime.Now;
                                tasimaEvraklar.DuzenlemeTarihi = DateTime.Now;
                                tasimaEvraklar.OlusturanId = KullaniciID;
                                tasimaEvraklar.DuzenleyenID = KullaniciID;
                                evrakManager.TAdd(tasimaEvraklar);

                            }




                            int kayitSayisi = int.Parse(form["KayitSayisi"]);
                            int NakliyeToplam = 0;
                            for (var i = 1; i <= kayitSayisi; i++)
                            {
                                NakliyeToplam += int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
                                YurtDisiTasimaDetay YurtDisiTasimaDetay = new YurtDisiTasimaDetay();
                                YurtDisiTasimaDetay.YurtDisiTasimaID = YurtDisiTasima.ID;
                                YurtDisiTasimaDetay.GondericiID = int.Parse(form["GondericiCari_ID" + i + "[]"]);
                                YurtDisiTasimaDetay.GondericiYuklemeUlke = form["GondericiYuklemeUlke" + i + "[]"];
                                YurtDisiTasimaDetay.GondericiYuklemeIl = form["GondericiYuklemeIl" + i + "[]"];
                                YurtDisiTasimaDetay.GondericiYuklemeIlce = form["GondericiYuklemeIlce" + i + "[]"];
                                YurtDisiTasimaDetay.GondericiYuklemeAcikAdres = form["GondericiYuklemeAcikAdres" + i + "[]"];
                                YurtDisiTasimaDetay.GondericiYuklemeTarihSaat = DateTime.Parse(form["GondericiFirmaTarihSaat" + i + "[]"]);
                                YurtDisiTasimaDetay.AliciID = int.Parse(form["AliciCari_ID" + i + "[]"]);
                                YurtDisiTasimaDetay.AliciIndirilenUlke = form["AliciIndirilenUlke" + i + "[]"];
                                YurtDisiTasimaDetay.AliciIndirilenIl = form["AliciIndirilenIl" + i + "[]"];
                                YurtDisiTasimaDetay.AliciIndirilenIlce = form["AliciIndirilenIlce" + i + "[]"];
                                YurtDisiTasimaDetay.AliciIndirilenAcikAdres = form["AliciIndirilenAcikAdres" + i + "[]"];
                                YurtDisiTasimaDetay.AliciIndirilenTarihSaat = DateTime.Parse(form["AliciFirmaTarihSaat" + i + "[]"]);
                                YurtDisiTasimaDetay.NakliyeTutarKDVHaric = int.Parse(form["NakliyeBedelTutar_KDVsiz" + i + "[]"]);
                                YurtDisiTasimaDetay.NakliyeKDV = int.Parse(form["NakliyeBedelTutar_KDV" + i + "[]"]);
                                YurtDisiTasimaDetay.NakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
                                YurtDisiTasimaDetay.NakliyeFiyat = int.Parse(form["Fiyat" + i + "[]"]);
                                YurtDisiTasimaDetay.YurtDisiTasimaTipiID = int.Parse(form["YurtDisiTasimaTipi_ID" + i+"[]"]);
                                YurtDisiTasimaDetay.GumruklemeTarihi = DateTime.Parse(form["GumruklemeTarihi" + i+"[]"]);
                                YurtDisiTasimaDetay.Durum = true;
                                YurtDisiTasimaDetay.FirmaID = FirmaID;
                                YurtDisiTasimaDetay.OlusturmaTarihi = DateTime.Now;
                                YurtDisiTasimaDetay.DuzenlemeTarihi = DateTime.Now;
                                YurtDisiTasimaDetay.OlusturanId = KullaniciID;
                                YurtDisiTasimaDetay.DuzenleyenID = KullaniciID;
                                YurtDisiTasimaDetayManager.TAdd(YurtDisiTasimaDetay);

                                int masrafSayisi = form["Masraflar"+i+"[]"].Count();
                                for (int j = 0; j < masrafSayisi; j++)
                                {

                                    YurtDisiTasimaMasraflar tasimaMasraflar = new YurtDisiTasimaMasraflar();
                                    tasimaMasraflar.YurtDisiTasimaID = YurtDisiTasima.ID;
                                    tasimaMasraflar.YurtDisiTasimaDetayID = YurtDisiTasimaDetay.ID;
                                    tasimaMasraflar.MasrafID = int.Parse(form["Masraflar"+i+"[]"][j]);
                                    tasimaMasraflar.Fiyat = float.Parse(form["MasrafFiyat"+i+"[]"][j]);
                                    tasimaMasraflar.ParaBirimID = int.Parse(form["ParaBirimID"+i+"[]"][j]);
                                    tasimaMasraflar.Durum = true;
                                    tasimaMasraflar.FirmaID = FirmaID;
                                    tasimaMasraflar.OlusturmaTarihi = DateTime.Now;
                                    tasimaMasraflar.DuzenlemeTarihi = DateTime.Now;
                                    tasimaMasraflar.OlusturanId = KullaniciID;
                                    tasimaMasraflar.DuzenleyenID = KullaniciID;
                                    YurtDisiTasimaMasraflarManager.TAdd(tasimaMasraflar);
                                   


                                }


                                ToplamFiyat += YurtDisiTasimaDetay.NakliyeFiyat;
                                string detayUrunId = "";
                                string faturaKesenId = "";
                                string faturaKesilenId = "";
                                for (var j = 0; j < form["Un_ID" + i + "[]"].Count(); j++)
                                {
                                    YurtDisiTasimaDetayUrun YurtDisiTasimaDetayUrun = new YurtDisiTasimaDetayUrun();
                                    YurtDisiTasimaDetayUrun.YurtDisiTasimaID = YurtDisiTasima.ID;
                                    YurtDisiTasimaDetayUrun.YurtDisiTasimaDetayID = YurtDisiTasimaDetay.ID;
                                    //YurtDisiTasimaDetayUrun.UnID = int.Parse(form["Un_ID" + i + "[]"][j]);
                                    YurtDisiTasimaDetayUrun.UrunID = int.Parse(form["TasinacakUrun_ID" + i + "[]"][j]);
                                    YurtDisiTasimaDetayUrun.YuklemeMiktari = int.Parse(form["YuklemeMiktari" + i + "[]"][j]);
                                    YurtDisiTasimaDetayUrun.OdemeYapanCariGrup = int.Parse(form["Cari_Odeme_Yapan" + i + "[]"][j]);
                                    YurtDisiTasimaDetayUrun.OdemeYapanCariID = int.Parse(form["Cari_Odeme_YapanID" + i + "[]"][j]);
                                    YurtDisiTasimaDetayUrun.Ucretlendirme = int.Parse(form["Ucretlendirme_ID" + i + "[]"][j]);
                                    YurtDisiTasimaDetayUrun.BirimSeferFiyati = int.Parse(form["Birim_SeferFiyat" + i + "[]"][j]);
                                    YurtDisiTasimaDetayUrun.Durum = true;
                                    YurtDisiTasimaDetayUrun.FirmaID = FirmaID;
                                    YurtDisiTasimaDetayUrun.OlusturmaTarihi = DateTime.Now;
                                    YurtDisiTasimaDetayUrun.DuzenlemeTarihi = DateTime.Now;
                                    YurtDisiTasimaDetayUrun.OlusturanId = KullaniciID;
                                    YurtDisiTasimaDetayUrun.DuzenleyenID = KullaniciID;
                                    YurtDisiTasimaDetayUrunManager.TAdd(YurtDisiTasimaDetayUrun);
                                    //detayUrunId += YurtDisiTasimaDetayUrun.ID + ";";

                                    YurtDisiFatura YurtDisiFatura = new YurtDisiFatura();
                                    YurtDisiFatura.FaturaNo = YurtDisiTasima.ID + "_" + DateTime.Now; //isteğe göre değiştirilebilir
                                    YurtDisiFatura.YurtDisiTasimaID = YurtDisiTasima.ID;
                                    YurtDisiFatura.YurtDisiTasimaDetayID = YurtDisiTasimaDetay.ID;
                                    YurtDisiFatura.Odeme = 0;
                                    YurtDisiFatura.Durum = true;
                                    YurtDisiFatura.FirmaID = FirmaID;
                                    YurtDisiFatura.OlusturmaTarihi = DateTime.Now;
                                    YurtDisiFatura.DuzenlemeTarihi = DateTime.Now;
                                    YurtDisiFatura.OlusturanId = KullaniciID;
                                    YurtDisiFatura.DuzenleyenID = KullaniciID;
                                    if (YurtDisiTasimaDetayUrun.OdemeYapanCariGrup == 1)
                                    {

                                        //detayUrunId = detayUrunId.Trim(';');
                                        YurtDisiFatura.YurtDisiTasimaDetayUrunID = YurtDisiTasimaDetayUrun.ID;
                                        //YurtDisiFatura.FaturaKesenID = YurtDisiTasimaDetay.GondericiID;
                                        YurtDisiFatura.FaturaKesenID = FirmaID;
                                        YurtDisiFatura.FaturaKesilenID = YurtDisiTasimaDetay.AliciID;
                                        YurtDisiFaturaManager.TAdd(YurtDisiFatura);

                                    }
                                    else if (YurtDisiTasimaDetayUrun.OdemeYapanCariGrup == 2)
                                    {
                                        //detayUrunId = detayUrunId.Trim(';');
                                        YurtDisiFatura.YurtDisiTasimaDetayUrunID = YurtDisiTasimaDetayUrun.ID;
                                        //YurtDisiFatura.FaturaKesenID = YurtDisiTasimaDetay.AliciID;
                                        YurtDisiFatura.FaturaKesenID = FirmaID;
                                        YurtDisiFatura.FaturaKesilenID = YurtDisiTasimaDetay.GondericiID;
                                        YurtDisiFaturaManager.TAdd(YurtDisiFatura);

                                    }

                                    YurtDisiCariHareket cariHareket1 = new YurtDisiCariHareket
                                    {
                                        CariID = YurtDisiFatura.FaturaKesenID,
                                        FaturaID = YurtDisiFatura.ID,
                                        OdemeID = 0,
                                        PlakaID = YurtDisiTasima.AracID,
                                        Tutar = ToplamFiyat,
                                        Durum = true,
                                        FirmaID = FirmaID,
                                        OlusturanId = KullaniciID,
                                        DuzenleyenID = KullaniciID,
                                        OlusturmaTarihi = DateTime.Now,
                                        DuzenlemeTarihi = DateTime.Now
                                    };
                                    Helper.YurtDisiCariHareketEkle(cariHareket1);

                                    CariHareket cariHareket = new CariHareket();
                                    cariHareket.CariID = YurtDisiFatura.FaturaKesilenID;
                                    cariHareket.YurtDisiFaturaID = YurtDisiTasima.ID;
                                    cariHareket.YurtDisiFaturaTutar = NakliyeToplam;
                                    cariHareket.Durum = true;
                                    cariHareket.FirmaID = FirmaID;
                                    cariHareket.OlusturanId = KullaniciID;
                                    cariHareket.DuzenleyenID = KullaniciID;
                                    cariHareket.OlusturmaTarihi = DateTime.Now;
                                    cariHareket.DuzenlemeTarihi = DateTime.Now;
                                    cariHareketManager.TAdd(cariHareket);
                                    Helper.CariBorcAlacakKalanHesapla(YurtDisiFatura.FaturaKesilenID);

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

        public IActionResult TasimaDuzenle(int ID)
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");

            List<YurtDisiTasimaEvraklar> evraklar = evrakManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.YurtDisiTasimaID == ID);
            ViewBag.YurtDisiTasimaEvraklar = evraklar;
            List<YurtDisiTasimaMasraflar> masraflar = YurtDisiTasimaMasraflarManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID && x.YurtDisiTasimaID == ID);
            ViewBag.YurtDisiTasimaMasraflar = masraflar;
            List<Arac> aracListesi = aracManager.GetAllList(x => x.Durum == true && (x.FirmaID == FirmaID || x.FirmaID == -2));
            List<Kullanicilar> surucuListesi = surucuManager.GetAllList(x => x.Kullanici_Durum == 1 && x.KullaniciGrup_ID==2 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));
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
            List<UlkeParaBirim> paraBirim = ulkeParaBirimManager.GetAllList(x => x.Durum == 1 && (x.Firma_ID == FirmaID || x.Firma_ID == -2));


            ViewBag.ParaBirimleri = paraBirim;
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

            var combinedQuery = from arac in aracManager.GetAllList((y => y.Durum == true && (y.FirmaID == FirmaID || y.FirmaID == -2) && y.AracTurID != 13 && y.AracTurID != 1))
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


            List<YurtDisiAracTur> YurtDisiAracTur = YurtDisiAracTurManager.GetAllList(x => x.FirmaID == FirmaID || x.FirmaID == -2);

            ViewBag.YurtDisiAracTur = YurtDisiAracTur;
            YurtDisiTasima tasima = YurtDisiTasimaManager.GetByID(ID);

            return View(tasima);
        }

        [HttpPost]
        public IActionResult TasimaDuzenle(YurtDisiTasima kayit, IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {


                    try
                    {
                        YurtDisiTasima YurtDisiTasima = YurtDisiTasimaManager.GetByID(kayit.ID);


                        var surucuKoparmaIslemi = Helper.YurtDisiSurucuKopar(kayit.Kullanici1ID, kayit.Kullanici2ID, kayit.Kullanici3ID);
                        if (surucuKoparmaIslemi == true)
                        {

                            YurtDisiTasima.Kullanici1ID = kayit.Kullanici1ID;
                            YurtDisiTasima.Kullanici2ID = kayit.Kullanici2ID;
                            YurtDisiTasima.Kullanici3ID = kayit.Kullanici3ID;

                            string[] aracIDForm = form["AracID"].ToString().Split('|');
                            int aracID = int.Parse(aracIDForm[0]);
                            YurtDisiTasima.AracID = aracID;
                            YurtDisiTasima.DorsePlakaID = kayit.DorsePlakaID;
//                            YurtDisiTasima.Evraklar = kayit.Evraklar;
                            YurtDisiTasima.DuzenlemeTarihi = DateTime.Now;
                            YurtDisiTasima.DuzenleyenID = KullaniciID;

                            YurtDisiTasima.AracTurID = int.Parse(form["AracTurID"]);
                            //YurtDisiTasima.ToplamYuklenenMiktar = kayit.ToplamYuklenenMiktar;

                            YurtDisiTasimaManager.TUpdate(YurtDisiTasima);


                            Helper.TumYurtDisiTasimaEvraklariSil(YurtDisiTasima.ID);


                            int evrakSayisi = form["Evraklar[]"].Count();
                            for (int i = 0; i < evrakSayisi; i++)
                            {

                                YurtDisiTasimaEvraklar tasimaEvraklar = new YurtDisiTasimaEvraklar();
                                tasimaEvraklar.YurtDisiTasimaID = YurtDisiTasima.ID;
                                tasimaEvraklar.EvrakID = int.Parse(form["Evraklar[]"][i]);
                                tasimaEvraklar.EvraklarVerilisTarihi = DateTime.Parse(form["EvraklarVerilisTarihi[]"][i]);
                                tasimaEvraklar.EvraklarGecerlilikTarihi = DateTime.Parse(form["EvraklarGecerlilikTarihi[]"][i]);
                                tasimaEvraklar.Durum = true;
                                tasimaEvraklar.FirmaID = FirmaID;
                                tasimaEvraklar.OlusturmaTarihi = DateTime.Now;
                                tasimaEvraklar.DuzenlemeTarihi = DateTime.Now;
                                tasimaEvraklar.OlusturanId = KullaniciID;
                                tasimaEvraklar.DuzenleyenID = KullaniciID;
                                evrakManager.TAdd(tasimaEvraklar);

                            }




                            List<CariHareket> cariHareketEski = cariHareketManager.GetAllList(x => x.YurtDisiFaturaID == YurtDisiTasima.ID);
                            foreach (var silinecek in cariHareketEski)
                            {
                                cariHareketManager.TDelete(silinecek);
                            }

                            int kayitSayisi = int.Parse(form["KayitSayisi"]);

                            int ToplamFiyat = 0;
                            int eskiNakliyeToplam = 0, yeniNakliyeToplam = 0;
                            for (var i = 1; i <= kayitSayisi; i++)
                            {
                                YurtDisiTasimaDetay YurtDisiTasimaDetay;
                                YurtDisiFatura YurtDisiFatura;
                                try
                                {
                                    int detayID = int.Parse(form["detayID" + i + "[]"]);

                                    YurtDisiTasimaDetay = YurtDisiTasimaDetayManager.GetByID(detayID);
                                    eskiNakliyeToplam = YurtDisiTasimaDetay.NakliyeToplam;
                                    yeniNakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);

                                    YurtDisiTasimaDetay.GondericiID = int.Parse(form["GondericiCari_ID" + i + "[]"]);
                                    YurtDisiTasimaDetay.GondericiYuklemeUlke = form["GondericiYuklemeUlke" + i + "[]"];
                                    YurtDisiTasimaDetay.GondericiYuklemeIl = form["GondericiYuklemeIl" + i + "[]"];
                                    YurtDisiTasimaDetay.GondericiYuklemeIlce = form["GondericiYuklemeIlce" + i + "[]"];
                                    YurtDisiTasimaDetay.GondericiYuklemeAcikAdres = form["GondericiYuklemeAcikAdres" + i + "[]"];
                                    YurtDisiTasimaDetay.GondericiYuklemeTarihSaat = DateTime.Parse(form["GondericiFirmaTarihSaat" + i + "[]"]);
                                    YurtDisiTasimaDetay.AliciID = int.Parse(form["AliciCari_ID" + i + "[]"]);
                                    YurtDisiTasimaDetay.AliciIndirilenUlke = form["AliciIndirilenUlke" + i + "[]"];
                                    YurtDisiTasimaDetay.AliciIndirilenIl = form["AliciIndirilenIl" + i + "[]"];
                                    YurtDisiTasimaDetay.AliciIndirilenIlce = form["AliciIndirilenIlce" + i + "[]"];
                                    YurtDisiTasimaDetay.AliciIndirilenAcikAdres = form["AliciIndirilenAcikAdres" + i + "[]"];
                                    YurtDisiTasimaDetay.AliciIndirilenTarihSaat = DateTime.Parse(form["AliciFirmaTarihSaat" + i + "[]"]);
                                    YurtDisiTasimaDetay.NakliyeTutarKDVHaric = int.Parse(form["NakliyeBedelTutar_KDVsiz" + i + "[]"]);
                                    YurtDisiTasimaDetay.NakliyeKDV = int.Parse(form["NakliyeBedelTutar_KDV" + i + "[]"]);
                                    YurtDisiTasimaDetay.NakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
                                    YurtDisiTasimaDetay.NakliyeFiyat = int.Parse(form["Fiyat" + i + "[]"]);

                                    YurtDisiTasimaDetay.YurtDisiTasimaTipiID = int.Parse(form["YurtDisiTasimaTipi_ID" + i + "[]"]);
                                    YurtDisiTasimaDetay.GumruklemeTarihi = DateTime.Parse(form["GumruklemeTarihi" + i + "[]"]);
                                    YurtDisiTasimaDetay.DuzenlemeTarihi = DateTime.Now;
                                    YurtDisiTasimaDetay.DuzenleyenID = KullaniciID;
                                    YurtDisiTasimaDetayManager.TUpdate(YurtDisiTasimaDetay);


                                    Helper.TumYurtDisiTasimaMasraflariSil(YurtDisiTasima.ID);


                                    int masrafSayisi = form["Masraflar" + i + "[]"].Count();
                                    for (int j = 0; j < masrafSayisi; j++)
                                    {

                                        YurtDisiTasimaMasraflar tasimaMasraflar = new YurtDisiTasimaMasraflar();
                                        tasimaMasraflar.YurtDisiTasimaID = YurtDisiTasima.ID;
                                        tasimaMasraflar.YurtDisiTasimaDetayID = YurtDisiTasimaDetay.ID;
                                        tasimaMasraflar.MasrafID = int.Parse(form["Masraflar" + i + "[]"][j]);
                                        tasimaMasraflar.Fiyat = float.Parse(form["MasrafFiyat" + i + "[]"][j]);
                                        tasimaMasraflar.ParaBirimID = int.Parse(form["ParaBirimID" + i + "[]"][j]);
                                        tasimaMasraflar.Durum = true;
                                        tasimaMasraflar.FirmaID = FirmaID;
                                        tasimaMasraflar.OlusturmaTarihi = DateTime.Now;
                                        tasimaMasraflar.DuzenlemeTarihi = DateTime.Now;
                                        tasimaMasraflar.OlusturanId = KullaniciID;
                                        tasimaMasraflar.DuzenleyenID = KullaniciID;
                                        YurtDisiTasimaMasraflarManager.TAdd(tasimaMasraflar);



                                    }


                                    ToplamFiyat += YurtDisiTasimaDetay.NakliyeFiyat;

                                }
                                catch (ArgumentNullException)
                                {
                                    YurtDisiTasimaDetay = new YurtDisiTasimaDetay();
                                    YurtDisiTasimaDetay.YurtDisiTasimaID = YurtDisiTasima.ID;
                                    YurtDisiTasimaDetay.GondericiID = int.Parse(form["GondericiCari_ID" + i + "[]"]);
                                    YurtDisiTasimaDetay.GondericiYuklemeUlke = form["GondericiYuklemeUlke" + i + "[]"];
                                    YurtDisiTasimaDetay.GondericiYuklemeIl = form["GondericiYuklemeIl" + i + "[]"];
                                    YurtDisiTasimaDetay.GondericiYuklemeIlce = form["GondericiYuklemeIlce" + i + "[]"];
                                    YurtDisiTasimaDetay.GondericiYuklemeAcikAdres = form["GondericiYuklemeAcikAdres" + i + "[]"];
                                    YurtDisiTasimaDetay.GondericiYuklemeTarihSaat = DateTime.Parse(form["GondericiFirmaTarihSaat" + i + "[]"]);
                                    YurtDisiTasimaDetay.AliciID = int.Parse(form["AliciCari_ID" + i + "[]"]);
                                    YurtDisiTasimaDetay.AliciIndirilenUlke = form["AliciIndirilenUlke" + i + "[]"];
                                    YurtDisiTasimaDetay.AliciIndirilenIl = form["AliciIndirilenIl" + i + "[]"];
                                    YurtDisiTasimaDetay.AliciIndirilenIlce = form["AliciIndirilenIlce" + i + "[]"];
                                    YurtDisiTasimaDetay.AliciIndirilenAcikAdres = form["AliciIndirilenAcikAdres" + i + "[]"];
                                    YurtDisiTasimaDetay.AliciIndirilenTarihSaat = DateTime.Parse(form["AliciFirmaTarihSaat" + i + "[]"]);
                                    YurtDisiTasimaDetay.NakliyeTutarKDVHaric = int.Parse(form["NakliyeBedelTutar_KDVsiz" + i + "[]"]);
                                    YurtDisiTasimaDetay.NakliyeKDV = int.Parse(form["NakliyeBedelTutar_KDV" + i + "[]"]);
                                    YurtDisiTasimaDetay.NakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
                                    YurtDisiTasimaDetay.NakliyeFiyat = int.Parse(form["Fiyat" + i + "[]"]);
                                    YurtDisiTasimaDetay.YurtDisiTasimaTipiID = int.Parse(form["YurtDisiTasimaTipi_ID" + i + "[]"]);
                                    YurtDisiTasimaDetay.GumruklemeTarihi = DateTime.Parse(form["GumruklemeTarihi" + i + "[]"]);
                                    YurtDisiTasimaDetay.Durum = true;
                                    YurtDisiTasimaDetay.FirmaID = FirmaID;
                                    YurtDisiTasimaDetay.OlusturmaTarihi = DateTime.Now;
                                    YurtDisiTasimaDetay.DuzenlemeTarihi = DateTime.Now;
                                    YurtDisiTasimaDetay.OlusturanId = KullaniciID;
                                    YurtDisiTasimaDetay.DuzenleyenID = KullaniciID;
                                    YurtDisiTasimaDetayManager.TAdd(YurtDisiTasimaDetay);
                                    Helper.TumYurtDisiTasimaMasraflariSil(YurtDisiTasima.ID);


                                    int masrafSayisi = form["Masraflar" + i + "[]"].Count();
                                    for (int j = 0; j < masrafSayisi; j++)
                                    {

                                        YurtDisiTasimaMasraflar tasimaMasraflar = new YurtDisiTasimaMasraflar();
                                        tasimaMasraflar.YurtDisiTasimaID = YurtDisiTasima.ID;
                                        tasimaMasraflar.YurtDisiTasimaDetayID = YurtDisiTasimaDetay.ID;
                                        tasimaMasraflar.MasrafID = int.Parse(form["Masraflar" + i + "[]"][j]);
                                        tasimaMasraflar.Fiyat = float.Parse(form["MasrafFiyat" + i + "[]"][j]);
                                        tasimaMasraflar.ParaBirimID = int.Parse(form["ParaBirimID" + i + "[]"][j]);
                                        tasimaMasraflar.Durum = true;
                                        tasimaMasraflar.FirmaID = FirmaID;
                                        tasimaMasraflar.OlusturmaTarihi = DateTime.Now;
                                        tasimaMasraflar.DuzenlemeTarihi = DateTime.Now;
                                        tasimaMasraflar.OlusturanId = KullaniciID;
                                        tasimaMasraflar.DuzenleyenID = KullaniciID;
                                        YurtDisiTasimaMasraflarManager.TAdd(tasimaMasraflar);



                                    }
                                    yeniNakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
                                }


                                string faturaKesenId = "", faturaKesilenId = "";
                                for (var j = 0; j < form["Un_ID" + i + "[]"].Count(); j++)
                                {
                                    var detayUrunID = (form["detayUrunID" + i + "[]"]);
                                    YurtDisiTasimaDetayUrun YurtDisiTasimaDetayUrun;
                                    try
                                    {
                                        YurtDisiTasimaDetayUrun = YurtDisiTasimaDetayUrunManager.GetByID(int.Parse(detayUrunID[j]));
                                        //YurtDisiTasimaDetayUrun.UnID = int.Parse(form["Un_ID" + i + "[]"][j]);
                                        YurtDisiTasimaDetayUrun.UrunID = int.Parse(form["TasinacakUrun_ID" + i + "[]"][j]);
                                        YurtDisiTasimaDetayUrun.YuklemeMiktari = int.Parse(form["YuklemeMiktari" + i + "[]"][j]);
                                        YurtDisiTasimaDetayUrun.OdemeYapanCariGrup = int.Parse(form["Cari_Odeme_Yapan" + i + "[]"][j]);
                                        YurtDisiTasimaDetayUrun.OdemeYapanCariID = int.Parse(form["Cari_Odeme_YapanID" + i + "[]"][j]);
                                        YurtDisiTasimaDetayUrun.Ucretlendirme = int.Parse(form["Ucretlendirme_ID" + i + "[]"][j]);
                                        YurtDisiTasimaDetayUrun.BirimSeferFiyati = int.Parse(form["Birim_SeferFiyat" + i + "[]"][j]);

                                        YurtDisiTasimaDetayUrun.DuzenlemeTarihi = DateTime.Now;
                                        YurtDisiTasimaDetayUrun.DuzenleyenID = KullaniciID;

                                        YurtDisiTasimaDetayUrunManager.TUpdate(YurtDisiTasimaDetayUrun);
                                        YurtDisiFatura = YurtDisiFaturaManager.GetAllList(x => x.YurtDisiTasimaID == kayit.ID && x.YurtDisiTasimaDetayID == YurtDisiTasimaDetayUrun.YurtDisiTasimaDetayID && x.YurtDisiTasimaDetayUrunID == YurtDisiTasimaDetayUrun.ID).SingleOrDefault();
                                        YurtDisiFatura.FaturaNo = YurtDisiTasima.ID + "_" + DateTime.Now; //isteğe göre değiştirilebilir
                                        YurtDisiFatura.YurtDisiTasimaID = YurtDisiTasima.ID;
                                        YurtDisiFatura.YurtDisiTasimaDetayID = YurtDisiTasimaDetay.ID;
                                        YurtDisiFatura.Durum = true;
                                        YurtDisiFatura.FirmaID = FirmaID;
                                        YurtDisiFatura.OlusturmaTarihi = DateTime.Now;
                                        YurtDisiFatura.DuzenlemeTarihi = DateTime.Now;
                                        YurtDisiFatura.OlusturanId = KullaniciID;
                                        YurtDisiFatura.DuzenleyenID = KullaniciID;
                                    }
                                    catch
                                    {
                                        YurtDisiTasimaDetayUrun = new YurtDisiTasimaDetayUrun();
                                        YurtDisiTasimaDetayUrun.YurtDisiTasimaID = YurtDisiTasima.ID;
                                        YurtDisiTasimaDetayUrun.YurtDisiTasimaDetayID = YurtDisiTasimaDetay.ID;

                                        //YurtDisiTasimaDetayUrun.UnID = int.Parse(form["Un_ID" + i + "[]"][j]);
                                        YurtDisiTasimaDetayUrun.UrunID = int.Parse(form["TasinacakUrun_ID" + i + "[]"][j]);
                                        YurtDisiTasimaDetayUrun.YuklemeMiktari = int.Parse(form["YuklemeMiktari" + i + "[]"][j]);
                                        YurtDisiTasimaDetayUrun.OdemeYapanCariGrup = int.Parse(form["Cari_Odeme_Yapan" + i + "[]"][j]);
                                        YurtDisiTasimaDetayUrun.OdemeYapanCariID = int.Parse(form["Cari_Odeme_YapanID" + i + "[]"][j]);
                                        YurtDisiTasimaDetayUrun.Ucretlendirme = int.Parse(form["Ucretlendirme_ID" + i + "[]"][j]);
                                        YurtDisiTasimaDetayUrun.BirimSeferFiyati = int.Parse(form["Birim_SeferFiyat" + i + "[]"][j]);

                                        YurtDisiTasimaDetayUrun.Durum = true;
                                        YurtDisiTasimaDetayUrun.FirmaID = FirmaID;
                                        YurtDisiTasimaDetayUrun.OlusturmaTarihi = DateTime.Now;
                                        YurtDisiTasimaDetayUrun.DuzenlemeTarihi = DateTime.Now;
                                        YurtDisiTasimaDetayUrun.OlusturanId = KullaniciID;
                                        YurtDisiTasimaDetayUrun.DuzenleyenID = KullaniciID;


                                        YurtDisiTasimaDetayUrunManager.TAdd(YurtDisiTasimaDetayUrun);
                                        YurtDisiFatura = new YurtDisiFatura();
                                        YurtDisiFatura.FaturaNo = YurtDisiTasima.ID + "_" + DateTime.Now; //isteğe göre değiştirilebilir
                                        YurtDisiFatura.YurtDisiTasimaID = YurtDisiTasima.ID;
                                        YurtDisiFatura.YurtDisiTasimaDetayID = YurtDisiTasimaDetay.ID;
                                        YurtDisiFatura.Durum = true;
                                        YurtDisiFatura.FirmaID = FirmaID;
                                        YurtDisiFatura.OlusturmaTarihi = DateTime.Now;
                                        YurtDisiFatura.DuzenlemeTarihi = DateTime.Now;
                                        YurtDisiFatura.OlusturanId = KullaniciID;
                                        YurtDisiFatura.DuzenleyenID = KullaniciID;
                                        YurtDisiFatura.YurtDisiTasimaDetayUrunID = YurtDisiTasimaDetayUrun.ID;
                                    }

                                    if (YurtDisiTasimaDetayUrun.OdemeYapanCariGrup == 1)
                                    {

                                        //YurtDisiFatura.FaturaKesenID = YurtDisiTasimaDetay.GondericiID;
                                        YurtDisiFatura.FaturaKesenID = FirmaID; ;
                                        YurtDisiFatura.FaturaKesilenID = YurtDisiTasimaDetay.AliciID;
                                    }
                                    else if (YurtDisiTasimaDetayUrun.OdemeYapanCariGrup == 2)
                                    {
                                        //YurtDisiFatura.FaturaKesenID = YurtDisiTasimaDetay.AliciID;
                                        YurtDisiFatura.FaturaKesenID = FirmaID;
                                        YurtDisiFatura.FaturaKesilenID = YurtDisiTasimaDetay.GondericiID;
                                    }
                                    if (YurtDisiFatura.ID != 0)
                                    {
                                        YurtDisiFaturaManager.TUpdate(YurtDisiFatura);
                                        Helper.YurtDisiCariHareketTemizle(YurtDisiFatura.ID);
                                    }
                                    else
                                        YurtDisiFaturaManager.TAdd(YurtDisiFatura);



                                    YurtDisiCariHareket cariHareket1 = new YurtDisiCariHareket
                                    {
                                        CariID = YurtDisiFatura.FaturaKesenID,
                                        FaturaID = YurtDisiFatura.ID,
                                        OdemeID = 0,
                                        PlakaID = YurtDisiTasima.AracID,
                                        Tutar = ToplamFiyat,
                                        Durum = true,
                                        FirmaID = FirmaID,
                                        OlusturanId = KullaniciID,
                                        DuzenleyenID = KullaniciID,
                                        OlusturmaTarihi = DateTime.Now,
                                        DuzenlemeTarihi = DateTime.Now
                                    };
                                    Helper.YurtDisiCariHareketEkle(cariHareket1);


                                    CariHareket cariHareket = new CariHareket();
                                    cariHareket.CariID = YurtDisiFatura.FaturaKesilenID;
                                    cariHareket.YurtDisiFaturaID = YurtDisiFatura.ID;
                                    cariHareket.AkaryakitFaturaTutar = yeniNakliyeToplam;
                                    cariHareket.Durum = true;
                                    cariHareket.FirmaID = FirmaID;
                                    cariHareket.OlusturanId = KullaniciID;
                                    cariHareket.DuzenleyenID = KullaniciID;
                                    cariHareket.OlusturmaTarihi = DateTime.Now;
                                    cariHareket.DuzenlemeTarihi = DateTime.Now;
                                    cariHareketManager.TAdd(cariHareket);

                                    Helper.CariBorcAlacakKalanHesapla(YurtDisiFatura.FaturaKesilenID);

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
                        YurtDisiTasima item = YurtDisiTasimaManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        YurtDisiTasimaManager.TUpdate(item);

                        List<YurtDisiTasimaDetay> itemDetay = YurtDisiTasimaDetayManager.GetAllList(x => x.YurtDisiTasimaID == int.Parse(form["ID"]));
                        foreach (var x in itemDetay)
                        {
                            x.Durum = false;
                            x.FirmaID = FirmaID;
                            x.DuzenlemeTarihi = DateTime.Now;
                            x.DuzenleyenID = KullaniciID;
                            YurtDisiTasimaDetayManager.TUpdate(x);

                        }
                        List<YurtDisiTasimaDetayUrun> itemDetayUrun = YurtDisiTasimaDetayUrunManager.GetAllList(x => x.YurtDisiTasimaID == int.Parse(form["ID"]));
                        foreach (var x in itemDetayUrun)
                        {
                            x.Durum = false;
                            x.FirmaID = FirmaID;
                            x.DuzenlemeTarihi = DateTime.Now;
                            x.DuzenleyenID = KullaniciID;
                            YurtDisiTasimaDetayUrunManager.TUpdate(x);
                            List<CariHareket> hareketler = cariHareketManager.GetAllList(y => y.CariID == x.OdemeYapanCariID && y.YurtDisiFaturaID == int.Parse(form["ID"]));
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
            YurtDisiTasima tasima = YurtDisiTasimaManager.GetByID(ID);
            List<YurtDisiTasimaDetay> tasimaDetay = YurtDisiTasimaDetayManager.GetAllList(x => x.YurtDisiTasimaID == ID && x.FirmaID == FirmaID);
            List<YurtDisiTasimaDetayUrun> tasimaDetayUrun = YurtDisiTasimaDetayUrunManager.GetAllList(x => x.YurtDisiTasimaID == ID && x.FirmaID == FirmaID);

            List<YurtDisiFatura> faturaList = YurtDisiFaturaManager.GetAllList(x => x.YurtDisiTasimaID == ID && x.FirmaID == FirmaID);

            ViewBag.GrupFatura = YurtDisiFaturaManager.GetAllList(x => x.YurtDisiTasimaID == ID && x.FirmaID == FirmaID)
             .GroupBy(x => x.FaturaKesenID)
             .Select(group => group.Key)
             .ToList();

            ViewBag.Tasima = tasima;
            ViewBag.TasimaDetay = tasimaDetay;
            ViewBag.TasimaDetayUrun = tasimaDetayUrun;
            ViewBag.Fatura = faturaList;


            return View();


        }

        public IActionResult Odeme(int FaturaID)
        {
            YurtDisiFatura fatura = YurtDisiFaturaManager.GetByID(FaturaID);
            return View(fatura);
        }
        [HttpPost]
        public IActionResult Odeme(YurtDisiFatura fatura)
        {
            YurtDisiFatura kayit = YurtDisiFaturaManager.GetByID(fatura.ID);
            kayit.Odeme = fatura.Odeme;
            YurtDisiFaturaManager.TUpdate(kayit);
            return RedirectToAction("CariHareket", "Cari", new { CariID = kayit.FaturaKesenID });
        }

    }
}
