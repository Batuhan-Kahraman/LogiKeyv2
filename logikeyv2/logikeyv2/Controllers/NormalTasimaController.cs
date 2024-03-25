using BusinessLayer.Concrate;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.EntityFrameworks;
using EntityLayer.Concrate;
using logikeyv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace logikeyv2.Controllers
{
    [OturumKontrolAttributeController]
    public class NormalTasimaController : BaseController
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
        NormalTasimaManager NormalTasimaManager = new NormalTasimaManager(new EFNormalTasimaRepository());
        NormalTasimaDetayManager NormalTasimaDetayManager = new NormalTasimaDetayManager(new EFNormalTasimaDetayRepository());
        NormalTasimaDetayUrunManager NormalTasimaDetayUrunManager = new NormalTasimaDetayUrunManager(new EFNormalTasimaDetayUrunRepository());
        NormalFaturaManager NormalFaturaManager = new NormalFaturaManager(new EFNormalFaturaRepository());
        NormalAracTurManager NormalAracTurManager = new NormalAracTurManager(new EFNormalAracTurRepository());
        #endregion

        public IActionResult Index()
        {


            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            var combinedQuery = from tasima in NormalTasimaManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID)
                                join arac in aracManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID)) on tasima.AracID equals arac.ID
                                join surucu1 in surucuManager.GetAllList((y => y.Kullanici_Durum == 1 && y.Firma_ID == FirmaID)) on tasima.Kullanici1ID equals surucu1.Kullanici_ID
                                select new NormalTasimaModel { Tasima = tasima, Arac = arac, Surucu = surucu1 };

            List<NormalTasimaModel> combinedList = combinedQuery.ToList();
            return View(combinedList);
        }

        public IActionResult TasimaEkle()
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Arac> aracListesi = aracManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            List<Kullanicilar> surucuListesi = surucuManager.GetAllList(x => x.Kullanici_Durum == 1 && x.Firma_ID == FirmaID);
            List<TasinacakUrun> tasinacakUrun = tasinacakUrunManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            List<UnListesi> UnListesi = unListesiManager.GetAllList(x => x.Durum == 1 && x.Firma_ID == FirmaID);
            List<Cari> CariListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Firma_ID == FirmaID);
            List<CariUcretlendirme> Ucretlendirme = ucretlendirme.GetAllList(x => x.Durum == true && x.Firma_ID == FirmaID);
            List<AracTip> aracTip = aracTipManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            List<AracTur> aracTur = aracTurManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            List<Birimler> birimler = birimlerManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            List<TasimaTipi> tasimaTipi = tasimaTipiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);

            List<Arac> cekiciPlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 1 && x.FirmaID == FirmaID);
            List<Arac> dorsePlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 4 && x.FirmaID == FirmaID);
            List<AracTip> dorseListesi = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID == 4 && x.FirmaID == FirmaID);


            List<Cari> AliciListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 8 && x.Firma_ID == FirmaID);
            List<Cari> GondericiListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 6 && x.Firma_ID == FirmaID);


            List<Cari_OdemeYapan> CariOdemeYapan = cari_OdemeYapanManager.GetAllList(x => x.Durum == true && x.Firma_ID == FirmaID);


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

            var combinedQuery = from arac in aracManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID &&  y.AracTurID == 13 || y.AracTurID == 1))
                                join tur in aracTurManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID)) on arac.AracTurID equals tur.ID
                                select new
                                {
                                    AracPlakasi = arac.Plaka,
                                    AracID = arac.ID,
                                    TurID = tur.ID,
                                    AracTurAdi = tur.Adi,
                                    Kapasite = arac.Goz1 + arac.Goz2 + arac.Goz3 + arac.Goz4 + arac.Goz5 + arac.Goz6
                                };

            ViewBag.Araclar = combinedQuery.ToList();


            List<NormalAracTur> NormalAracTur = NormalAracTurManager.GetAllList(x=>x.FirmaID==FirmaID);

            ViewBag.NormalAracTur = NormalAracTur;
            return View();
        }

        [HttpPost]
        public IActionResult TasimaEkle(NormalTasima NormalTasima, IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int ToplamFiyat = 0;
                        string[] aracIDForm = form["AracID"].ToString().Split('|');
                        int aracID = int.Parse(aracIDForm[0]);
                        NormalTasima.AracID = aracID;
                        NormalTasima.Durum = true;
                        NormalTasima.FirmaID = FirmaID;
                        NormalTasima.OlusturmaTarihi = DateTime.Now;
                        NormalTasima.DuzenlemeTarihi = DateTime.Now;
                        NormalTasima.OlusturanId = KullaniciID;
                        NormalTasima.DuzenleyenID = KullaniciID;

                        NormalTasima.AracTurID = int.Parse(form["AracTurID"]);
                        NormalTasimaManager.TAdd(NormalTasima);
                        int kayitSayisi = int.Parse(form["KayitSayisi"]);
                        for (var i = 1; i <= kayitSayisi; i++)
                        {
                            NormalTasimaDetay NormalTasimaDetay = new NormalTasimaDetay();
                            NormalTasimaDetay.NormalTasimaID = NormalTasima.ID;
                            NormalTasimaDetay.GondericiID = int.Parse(form["GondericiCari_ID" + i + "[]"]);
                            NormalTasimaDetay.GondericiYuklemeIlID = int.Parse(form["MalYuklemeAdres_IL_ID" + i + "[]"]);
                            NormalTasimaDetay.GondericiYuklemeIlceID = int.Parse(form["MalYuklemeAdres_ILCE_ID" + i + "[]"]);
                            NormalTasimaDetay.GondericiYuklemeTarihSaat = DateTime.Parse(form["GondericiFirmaTarihSaat" + i + "[]"]);
                            NormalTasimaDetay.AliciID = int.Parse(form["AliciCari_ID" + i + "[]"]);
                            NormalTasimaDetay.AliciIndirilenIlID = int.Parse(form["IndirilenAdres_IL_ID" + i + "[]"]);
                            NormalTasimaDetay.AliciIndirilenIlceID = int.Parse(form["IndirilenAdres_ILCE_ID" + i + "[]"]);
                            NormalTasimaDetay.AliciIndirilenTarihSaat = DateTime.Parse(form["AliciFirmaTarihSaat" + i + "[]"]);
                            NormalTasimaDetay.NakliyeTutarKDVHaric = int.Parse(form["NakliyeBedelTutar_KDVsiz" + i + "[]"]);
                            NormalTasimaDetay.NakliyeKDV = int.Parse(form["NakliyeBedelTutar_KDV" + i + "[]"]);
                            NormalTasimaDetay.NakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
                            NormalTasimaDetay.NakliyeFiyat = int.Parse(form["Fiyat" + i + "[]"]);
                            NormalTasimaDetay.Durum = true;
                            NormalTasimaDetay.FirmaID = FirmaID;
                            NormalTasimaDetay.OlusturmaTarihi = DateTime.Now;
                            NormalTasimaDetay.DuzenlemeTarihi = DateTime.Now;
                            NormalTasimaDetay.OlusturanId = KullaniciID;
                            NormalTasimaDetay.DuzenleyenID = KullaniciID; 
                            NormalTasimaDetayManager.TAdd(NormalTasimaDetay);
                            ToplamFiyat += NormalTasimaDetay.NakliyeFiyat;
                            string detayUrunId = "";
                            string faturaKesenId = "";
                            string faturaKesilenId = "";
                            for (var j = 0; j < form["Un_ID" + i + "[]"].Count(); j++)
                            {
                                NormalTasimaDetayUrun NormalTasimaDetayUrun = new NormalTasimaDetayUrun();
                                NormalTasimaDetayUrun.NormalTasimaID = NormalTasima.ID;
                                NormalTasimaDetayUrun.NormalTasimaDetayID = NormalTasimaDetay.ID;
                                NormalTasimaDetayUrun.UnID = int.Parse(form["Un_ID" + i + "[]"][j]);
                                NormalTasimaDetayUrun.UrunID = int.Parse(form["TasinacakUrun_ID" + i + "[]"][j]);
                                NormalTasimaDetayUrun.YuklemeMiktari = int.Parse(form["YuklemeMiktari" + i + "[]"][j]);
                                NormalTasimaDetayUrun.OdemeYapanCariGrup = int.Parse(form["Cari_Odeme_Yapan" + i + "[]"][j]);
                                NormalTasimaDetayUrun.OdemeYapanCariID = int.Parse(form["Cari_Odeme_YapanID" + i + "[]"][j]);
                                NormalTasimaDetayUrun.Ucretlendirme = int.Parse(form["Ucretlendirme_ID" + i + "[]"][j]);
                                NormalTasimaDetayUrun.BirimSeferFiyati = int.Parse(form["Birim_SeferFiyat" + i + "[]"][j]);
                                NormalTasimaDetayUrun.Durum = true;
                                NormalTasimaDetayUrun.FirmaID = FirmaID;
                                NormalTasimaDetayUrun.OlusturmaTarihi = DateTime.Now;
                                NormalTasimaDetayUrun.DuzenlemeTarihi = DateTime.Now;
                                NormalTasimaDetayUrun.OlusturanId = KullaniciID;
                                NormalTasimaDetayUrun.DuzenleyenID = KullaniciID;
                                NormalTasimaDetayUrunManager.TAdd(NormalTasimaDetayUrun);
                                //detayUrunId += NormalTasimaDetayUrun.ID + ";";

                                NormalFatura NormalFatura = new NormalFatura();
                                NormalFatura.FaturaNo = NormalTasima.ID + "_" + DateTime.Now; //isteğe göre değiştirilebilir
                                NormalFatura.NormalTasimaID = NormalTasima.ID;
                                NormalFatura.NormalTasimaDetayID = NormalTasimaDetay.ID;
                                NormalFatura.Odeme = 0;
                                NormalFatura.Durum = true;
                                NormalFatura.FirmaID = FirmaID;
                                NormalFatura.OlusturmaTarihi = DateTime.Now;
                                NormalFatura.DuzenlemeTarihi = DateTime.Now;
                                NormalFatura.OlusturanId = KullaniciID;
                                NormalFatura.DuzenleyenID = KullaniciID;
                                if (NormalTasimaDetayUrun.OdemeYapanCariGrup == 1)
                                {

                                    //detayUrunId = detayUrunId.Trim(';');
                                    NormalFatura.NormalTasimaDetayUrunID = NormalTasimaDetayUrun.ID;
                                    NormalFatura.FaturaKesenID = NormalTasimaDetay.GondericiID;
                                    NormalFatura.FaturaKesilenID = NormalTasimaDetay.AliciID;
                                    NormalFaturaManager.TAdd(NormalFatura);

                                }
                                else if (NormalTasimaDetayUrun.OdemeYapanCariGrup == 2)
                                {
                                    //detayUrunId = detayUrunId.Trim(';');
                                    NormalFatura.NormalTasimaDetayUrunID = NormalTasimaDetayUrun.ID;
                                    NormalFatura.FaturaKesenID = NormalTasimaDetay.AliciID;
                                    NormalFatura.FaturaKesilenID = NormalTasimaDetay.GondericiID;
                                    NormalFaturaManager.TAdd(NormalFatura);

                                }

                                NormalCariHareket cariHareket = new NormalCariHareket
                                {
                                    CariID = NormalFatura.FaturaKesenID,
                                    FaturaID = NormalFatura.ID,
                                    OdemeID = 0,
                                    PlakaID = NormalTasima.AracID,
                                    Tutar = ToplamFiyat,
                                    Durum = true,
                                    FirmaID =FirmaID,
                                    OlusturanId = KullaniciID,
                                    DuzenleyenID = KullaniciID,
                                    OlusturmaTarihi = DateTime.Now,
                                    DuzenlemeTarihi = DateTime.Now
                                };
                                Helper.NormalCariHareketEkle(cariHareket);


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
            return RedirectToAction("Index");
        }

        public IActionResult TasimaDuzenle(int ID)
        {

            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            List<Arac> aracListesi = aracManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            List<Kullanicilar> surucuListesi = surucuManager.GetAllList(x => x.Kullanici_Durum == 1 && x.Firma_ID == FirmaID);
            List<TasinacakUrun> tasinacakUrun = tasinacakUrunManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            List<UnListesi> UnListesi = unListesiManager.GetAllList(x => x.Durum == 1 && x.Firma_ID == FirmaID);
            List<Cari> CariListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Firma_ID == FirmaID);
            List<CariUcretlendirme> Ucretlendirme = ucretlendirme.GetAllList(x => x.Durum == true && x.Firma_ID == FirmaID);
            List<AracTip> aracTip = aracTipManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            List<AracTur> aracTur = aracTurManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            List<Birimler> birimler = birimlerManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);
            List<TasimaTipi> tasimaTipi = tasimaTipiManager.GetAllList(x => x.Durum == true && x.FirmaID == FirmaID);

            List<Arac> cekiciPlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 1 && x.FirmaID == FirmaID);
            List<Arac> dorsePlaka = aracManager.GetAllList(x => x.Durum == true && x.AracTurID == 4 && x.FirmaID == FirmaID);
            List<AracTip> dorseListesi = aracTipManager.GetAllList(x => x.Durum == true && x.AracTurID == 4 && x.FirmaID == FirmaID);


            List<Cari> AliciListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 8 && x.Firma_ID == FirmaID);
            List<Cari> GondericiListesi = cariManager.GetAllList(x => x.Durum == 1 && x.Cari_GrupID == 6 && x.Firma_ID == FirmaID);


            List<Cari_OdemeYapan> CariOdemeYapan = cari_OdemeYapanManager.GetAllList(x => x.Durum == true && x.Firma_ID == FirmaID);

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

            var combinedQuery = from arac in aracManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID &&  y.AracTurID == 13 || y.AracTurID == 1))
                                join tur in aracTurManager.GetAllList((y => y.Durum == true && y.FirmaID == FirmaID)) on arac.AracTurID equals tur.ID
                                select new
                                {
                                    AracPlakasi = arac.Plaka,
                                    AracID = arac.ID,
                                    TurID = tur.ID,
                                    AracTurAdi = tur.Adi,
                                    Kapasite = arac.Goz1 + arac.Goz2 + arac.Goz3 + arac.Goz4 + arac.Goz5 + arac.Goz6
                                };

            ViewBag.Araclar = combinedQuery.ToList();


            List<NormalAracTur> NormalAracTur = NormalAracTurManager.GetAllList(x => x.FirmaID == FirmaID);

            ViewBag.NormalAracTur = NormalAracTur;
            NormalTasima tasima = NormalTasimaManager.GetByID(ID);

            return View(tasima);
        }

        [HttpPost]
        public IActionResult TasimaDuzenle(NormalTasima kayit, IFormCollection form)
        {
            int FirmaID = (int)HttpContext.Session.GetInt32("FirmaID");
            int KullaniciID = (int)HttpContext.Session.GetInt32("KullaniciID");
            using (var context = new Context())
            {
                using (var transaction = context.Database.BeginTransaction())
                {


                    try
                    {
                        NormalTasima NormalTasima = NormalTasimaManager.GetByID(kayit.ID);


                        NormalTasima.Kullanici1ID = kayit.Kullanici1ID;
                        NormalTasima.Kullanici2ID = kayit.Kullanici2ID;
                        NormalTasima.Kullanici3ID = kayit.Kullanici3ID;

                        string[] aracIDForm = form["AracID"].ToString().Split('|');
                        int aracID = int.Parse(aracIDForm[0]);
                        NormalTasima.AracID = aracID;
                        NormalTasima.DorsePlakaID = kayit.DorsePlakaID;
                        NormalTasima.DuzenlemeTarihi = DateTime.Now;
                        NormalTasima.DuzenleyenID = KullaniciID;

                        NormalTasima.AracTurID = int.Parse(form["AracTurID"]);
                        NormalTasima.ToplamYuklenenMiktar = kayit.ToplamYuklenenMiktar;

                        NormalTasimaManager.TUpdate(NormalTasima);


                        int kayitSayisi = int.Parse(form["KayitSayisi"]);

                        int ToplamFiyat = 0;

                        for (var i = 1; i <= kayitSayisi; i++)
                        {
                            NormalTasimaDetay NormalTasimaDetay;
                            NormalFatura NormalFatura;
                            try
                            {
                                int detayID = int.Parse(form["detayID" + i + "[]"]);

                                NormalTasimaDetay = NormalTasimaDetayManager.GetByID(detayID);

                                NormalTasimaDetay.GondericiID = int.Parse(form["GondericiCari_ID" + i + "[]"]);
                                NormalTasimaDetay.GondericiYuklemeIlID = int.Parse(form["MalYuklemeAdres_IL_ID" + i + "[]"]);
                                NormalTasimaDetay.GondericiYuklemeIlceID = int.Parse(form["MalYuklemeAdres_ILCE_ID" + i + "[]"]);
                                NormalTasimaDetay.GondericiYuklemeTarihSaat = DateTime.Parse(form["GondericiFirmaTarihSaat" + i + "[]"]);
                                NormalTasimaDetay.AliciID = int.Parse(form["AliciCari_ID" + i + "[]"]);
                                NormalTasimaDetay.AliciIndirilenIlID = int.Parse(form["IndirilenAdres_IL_ID" + i + "[]"]);
                                NormalTasimaDetay.AliciIndirilenIlceID = int.Parse(form["IndirilenAdres_ILCE_ID" + i + "[]"]);
                                NormalTasimaDetay.AliciIndirilenTarihSaat = DateTime.Parse(form["AliciFirmaTarihSaat" + i + "[]"]);
                                NormalTasimaDetay.NakliyeTutarKDVHaric = int.Parse(form["NakliyeBedelTutar_KDVsiz" + i + "[]"]);
                                NormalTasimaDetay.NakliyeKDV = int.Parse(form["NakliyeBedelTutar_KDV" + i + "[]"]);
                                NormalTasimaDetay.NakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
                                NormalTasimaDetay.NakliyeFiyat = int.Parse(form["Fiyat" + i + "[]"]);

                                NormalTasimaDetay.DuzenlemeTarihi = DateTime.Now;
                                NormalTasimaDetay.DuzenleyenID = KullaniciID;
                                NormalTasimaDetayManager.TUpdate(NormalTasimaDetay);
                                ToplamFiyat += NormalTasimaDetay.NakliyeFiyat;

                            }
                            catch (ArgumentNullException)
                            {
                                NormalTasimaDetay = new NormalTasimaDetay();
                                NormalTasimaDetay.NormalTasimaID = NormalTasima.ID;
                                NormalTasimaDetay.GondericiID = int.Parse(form["GondericiCari_ID" + i + "[]"]);
                                NormalTasimaDetay.GondericiYuklemeIlID = int.Parse(form["MalYuklemeAdres_IL_ID" + i + "[]"]);
                                NormalTasimaDetay.GondericiYuklemeIlceID = int.Parse(form["MalYuklemeAdres_ILCE_ID" + i + "[]"]);
                                NormalTasimaDetay.GondericiYuklemeTarihSaat = DateTime.Parse(form["GondericiFirmaTarihSaat" + i + "[]"]);
                                NormalTasimaDetay.AliciID = int.Parse(form["AliciCari_ID" + i + "[]"]);
                                NormalTasimaDetay.AliciIndirilenIlID = int.Parse(form["IndirilenAdres_IL_ID" + i + "[]"]);
                                NormalTasimaDetay.AliciIndirilenIlceID = int.Parse(form["IndirilenAdres_ILCE_ID" + i + "[]"]);
                                NormalTasimaDetay.AliciIndirilenTarihSaat = DateTime.Parse(form["AliciFirmaTarihSaat" + i + "[]"]);
                                NormalTasimaDetay.NakliyeTutarKDVHaric = int.Parse(form["NakliyeBedelTutar_KDVsiz" + i + "[]"]);
                                NormalTasimaDetay.NakliyeKDV = int.Parse(form["NakliyeBedelTutar_KDV" + i + "[]"]);
                                NormalTasimaDetay.NakliyeToplam = int.Parse(form["NakliyeBedeliToplam_KDVli" + i + "[]"]);
                                NormalTasimaDetay.NakliyeFiyat = int.Parse(form["Fiyat" + i + "[]"]);
                                NormalTasimaDetay.Durum = true;
                                NormalTasimaDetay.FirmaID = FirmaID;
                                NormalTasimaDetay.OlusturmaTarihi = DateTime.Now;
                                NormalTasimaDetay.DuzenlemeTarihi = DateTime.Now;
                                NormalTasimaDetay.OlusturanId = KullaniciID;
                                NormalTasimaDetay.DuzenleyenID = KullaniciID;
                                NormalTasimaDetayManager.TAdd(NormalTasimaDetay);
                            }


                            string faturaKesenId = "", faturaKesilenId = "";
                            for (var j = 0; j < form["Un_ID" + i + "[]"].Count(); j++)
                            {
                                var detayUrunID = (form["detayUrunID" + i + "[]"]);
                                NormalTasimaDetayUrun NormalTasimaDetayUrun;
                                try
                                {
                                    NormalTasimaDetayUrun = NormalTasimaDetayUrunManager.GetByID(int.Parse(detayUrunID[j]));
                                    NormalTasimaDetayUrun.UnID = int.Parse(form["Un_ID" + i + "[]"][j]);
                                    NormalTasimaDetayUrun.UrunID = int.Parse(form["TasinacakUrun_ID" + i + "[]"][j]);
                                    NormalTasimaDetayUrun.YuklemeMiktari = int.Parse(form["YuklemeMiktari" + i + "[]"][j]);
                                    NormalTasimaDetayUrun.OdemeYapanCariGrup = int.Parse(form["Cari_Odeme_Yapan" + i + "[]"][j]);
                                    NormalTasimaDetayUrun.OdemeYapanCariID = int.Parse(form["Cari_Odeme_YapanID" + i + "[]"][j]);
                                    NormalTasimaDetayUrun.Ucretlendirme = int.Parse(form["Ucretlendirme_ID" + i + "[]"][j]);
                                    NormalTasimaDetayUrun.BirimSeferFiyati = int.Parse(form["Birim_SeferFiyat" + i + "[]"][j]);

                                    NormalTasimaDetayUrun.DuzenlemeTarihi = DateTime.Now;
                                    NormalTasimaDetayUrun.DuzenleyenID = KullaniciID;

                                    NormalTasimaDetayUrunManager.TUpdate(NormalTasimaDetayUrun);
                                    NormalFatura = NormalFaturaManager.GetAllList(x => x.NormalTasimaID == kayit.ID && x.NormalTasimaDetayID == NormalTasimaDetayUrun.NormalTasimaDetayID && x.NormalTasimaDetayUrunID == NormalTasimaDetayUrun.ID).SingleOrDefault();
                                    NormalFatura.FaturaNo = NormalTasima.ID + "_" + DateTime.Now; //isteğe göre değiştirilebilir
                                    NormalFatura.NormalTasimaID = NormalTasima.ID;
                                    NormalFatura.NormalTasimaDetayID = NormalTasimaDetay.ID;
                                    NormalFatura.Durum = true;
                                    NormalFatura.FirmaID = FirmaID;
                                    NormalFatura.OlusturmaTarihi = DateTime.Now;
                                    NormalFatura.DuzenlemeTarihi = DateTime.Now;
                                    NormalFatura.OlusturanId = KullaniciID;
                                    NormalFatura.DuzenleyenID = KullaniciID;
                                }
                                catch
                                {
                                    NormalTasimaDetayUrun = new NormalTasimaDetayUrun();
                                    NormalTasimaDetayUrun.NormalTasimaID = NormalTasima.ID;
                                    NormalTasimaDetayUrun.NormalTasimaDetayID = NormalTasimaDetay.ID;

                                    NormalTasimaDetayUrun.UnID = int.Parse(form["Un_ID" + i + "[]"][j]);
                                    NormalTasimaDetayUrun.UrunID = int.Parse(form["TasinacakUrun_ID" + i + "[]"][j]);
                                    NormalTasimaDetayUrun.YuklemeMiktari = int.Parse(form["YuklemeMiktari" + i + "[]"][j]);
                                    NormalTasimaDetayUrun.OdemeYapanCariGrup = int.Parse(form["Cari_Odeme_Yapan" + i + "[]"][j]);
                                    NormalTasimaDetayUrun.OdemeYapanCariID = int.Parse(form["Cari_Odeme_YapanID" + i + "[]"][j]);
                                    NormalTasimaDetayUrun.Ucretlendirme = int.Parse(form["Ucretlendirme_ID" + i + "[]"][j]);
                                    NormalTasimaDetayUrun.BirimSeferFiyati = int.Parse(form["Birim_SeferFiyat" + i + "[]"][j]);

                                    NormalTasimaDetayUrun.Durum = true;
                                    NormalTasimaDetayUrun.FirmaID = FirmaID;
                                    NormalTasimaDetayUrun.OlusturmaTarihi = DateTime.Now;
                                    NormalTasimaDetayUrun.DuzenlemeTarihi = DateTime.Now;
                                    NormalTasimaDetayUrun.OlusturanId = KullaniciID;
                                    NormalTasimaDetayUrun.DuzenleyenID = KullaniciID;


                                    NormalTasimaDetayUrunManager.TAdd(NormalTasimaDetayUrun);
                                    NormalFatura = new NormalFatura();
                                    NormalFatura.FaturaNo = NormalTasima.ID + "_" + DateTime.Now; //isteğe göre değiştirilebilir
                                    NormalFatura.NormalTasimaID = NormalTasima.ID;
                                    NormalFatura.NormalTasimaDetayID = NormalTasimaDetay.ID;
                                    NormalFatura.Durum = true;
                                    NormalFatura.FirmaID = FirmaID;
                                    NormalFatura.OlusturmaTarihi = DateTime.Now;
                                    NormalFatura.DuzenlemeTarihi = DateTime.Now;
                                    NormalFatura.OlusturanId = KullaniciID;
                                    NormalFatura.DuzenleyenID = KullaniciID;
                                    NormalFatura.NormalTasimaDetayUrunID = NormalTasimaDetayUrun.ID;
                                }

                                if (NormalTasimaDetayUrun.OdemeYapanCariGrup == 1)
                                {

                                    NormalFatura.FaturaKesenID = NormalTasimaDetay.GondericiID;
                                    NormalFatura.FaturaKesilenID = NormalTasimaDetay.AliciID;
                                }
                                else if (NormalTasimaDetayUrun.OdemeYapanCariGrup == 2)
                                {
                                    NormalFatura.FaturaKesenID = NormalTasimaDetay.AliciID;
                                    NormalFatura.FaturaKesilenID = NormalTasimaDetay.GondericiID;
                                }
                                if (NormalFatura.ID != 0)
                                {
                                    NormalFaturaManager.TUpdate(NormalFatura);
                                    Helper.NormalCariHareketTemizle(NormalFatura.ID);
                                }
                                else
                                    NormalFaturaManager.TAdd(NormalFatura);



                                NormalCariHareket cariHareket = new NormalCariHareket
                                {
                                    CariID = NormalFatura.FaturaKesenID,
                                    FaturaID = NormalFatura.ID,
                                    OdemeID = 0,
                                    PlakaID = NormalTasima.AracID,
                                    Tutar = ToplamFiyat,
                                    Durum = true,
                                    FirmaID =FirmaID,
                                    OlusturanId = KullaniciID,
                                    DuzenleyenID = KullaniciID,
                                    OlusturmaTarihi = DateTime.Now,
                                    DuzenlemeTarihi = DateTime.Now
                                };
                                Helper.NormalCariHareketEkle(cariHareket);


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
                        NormalTasima item = NormalTasimaManager.GetByID(int.Parse(form["ID"]));
                        item.Durum = false;
                        item.FirmaID = FirmaID;
                        item.DuzenlemeTarihi = DateTime.Now;
                        item.DuzenleyenID = KullaniciID;
                        NormalTasimaManager.TUpdate(item);

                        List<NormalTasimaDetay> itemDetay = NormalTasimaDetayManager.GetAllList(x => x.NormalTasimaID == int.Parse(form["ID"]));
                        foreach (var x in itemDetay)
                        {
                            x.Durum = false;
                            x.FirmaID = FirmaID;
                            x.DuzenlemeTarihi = DateTime.Now;
                            x.DuzenleyenID = KullaniciID;
                            NormalTasimaDetayManager.TUpdate(x);

                        }
                        List<NormalTasimaDetayUrun> itemDetayUrun = NormalTasimaDetayUrunManager.GetAllList(x => x.NormalTasimaID == int.Parse(form["ID"]));
                        foreach (var x in itemDetayUrun)
                        {
                            x.Durum = false;
                            x.FirmaID = FirmaID;
                            x.DuzenlemeTarihi = DateTime.Now;
                            x.DuzenleyenID = KullaniciID;
                            NormalTasimaDetayUrunManager.TUpdate(x);

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
            NormalTasima tasima = NormalTasimaManager.GetByID(ID);
            List<NormalTasimaDetay> tasimaDetay = NormalTasimaDetayManager.GetAllList(x => x.NormalTasimaID == ID && x.FirmaID == FirmaID);
            List<NormalTasimaDetayUrun> tasimaDetayUrun = NormalTasimaDetayUrunManager.GetAllList(x => x.NormalTasimaID == ID && x.FirmaID == FirmaID);

            List<NormalFatura> faturaList = NormalFaturaManager.GetAllList(x => x.NormalTasimaID == ID && x.FirmaID == FirmaID);

            ViewBag.GrupFatura = NormalFaturaManager.GetAllList(x => x.NormalTasimaID == ID && x.FirmaID == FirmaID)
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
            NormalFatura fatura = NormalFaturaManager.GetByID(FaturaID);
            return View(fatura);
        }
        [HttpPost]
        public IActionResult Odeme(NormalFatura fatura)
        {
            NormalFatura kayit = NormalFaturaManager.GetByID(fatura.ID);
            kayit.Odeme = fatura.Odeme;
            NormalFaturaManager.TUpdate(kayit);
            return RedirectToAction("CariHareket", "Cari", new { CariID = kayit.FaturaKesenID });
        }

    }
}
