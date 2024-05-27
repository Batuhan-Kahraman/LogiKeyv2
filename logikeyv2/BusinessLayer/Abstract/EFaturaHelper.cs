using DataAccessLayer.Concrate;
using EntityLayer.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using System.Net.Http;
using DataAccessLayer.EntityFramework;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;
using Google.Cloud.Vision.V1;
using static Azure.Core.HttpHeader;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Drawing;
using System.Xml.Linq;
using Google.Api.Gax.ResourceNames;
using Google.Type;
using System.IO;


namespace BusinessLayer.Concrate
{
    public class EFaturaHelper : Context
    {

        static string accessToken = null;
        static int loginSayac = 0;

        public static async Task<string> Login(string username, string pswd)
        {
            // API'nin URL'si
            string apiUrl = "https://integration-test.gai.com.tr/api/IntegrationKullanici/Login";

            // Kullanıcı adı ve şifre
            string userName = username;
            string password = pswd;

            // Kullanıcı bilgilerini içeren JSON oluştur
            var requestData = new
            {
                userName = userName,
                password = password
            };

            // JSON nesnesini string'e dönüştür
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);

            // String'i içeren HttpContent oluştur
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            // HttpClient oluştur
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // API'ye POST isteği gönder
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Yanıt başarılı ise
                    if (response.IsSuccessStatusCode)
                    {

                        // Yanıt içeriğini oku
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);
                        if (jsonResponse.status == true)
                        {
                            var jwttoken = jsonResponse.jwtToken;
                            if (jwttoken != null)
                            {
                                accessToken = jwttoken.accessToken;
                                return accessToken;
                            }
                            else
                                return "-1";
                        }
                        else
                            return "-1";
                    }
                    else
                    {
                        return "-1";

                    }
                }
                catch (Exception ex)
                {
                    return "-1";
                }
            }
        }


        public static async Task<string> Create(int ID, int FirmaID)
        {
            var sonuc = "";
            var context = new Context();
            var firma = context.Firma.FirstOrDefault(e => e.Firma_ID == FirmaID);

            if (accessToken != null)
            {
                var tasima = context.AkaryakitTasima.FirstOrDefault(e => e.ID == ID);
                var tasimaDetay = context.AkaryakitTasimaDetay.Where(e => e.AkaryakitTasimaID == ID).ToList();
                var tasimaDetayUrun = context.AkaryakitTasimaDetayUrun.Where(e => e.AkaryakitTasimaID == ID).ToList();
                var faturaList = context.AkaryakitFatura.Where(e => e.AkaryakitTasimaID == ID).ToList();

                var GrupFaturaKesen = context.AkaryakitFatura
                    .Where(e => e.AkaryakitTasimaID == ID)
                    .GroupBy(x => x.FaturaKesenID)
                    .Select(group => group.Key)
                    .ToList();

                var GrupFaturaKesilen = context.AkaryakitFatura
                    .Where(e => e.AkaryakitTasimaID == ID)
                    .GroupBy(x => x.FaturaKesilenID)
                    .Select(group => group.Key)
                    .ToList();

                string url = "https://integration-test.gai.com.tr/api/IntegrationGidenFatura/Create";
                Guid ettn = Guid.NewGuid();

                foreach (var CariID in GrupFaturaKesilen)
                {
                    var cari = context.Cari.FirstOrDefault(e => e.Cari_ID == CariID);
                    var toplamNakliyeFiyati = context.AkaryakitTasimaDetay
                        .Where(e => e.ID == (context.AkaryakitTasimaDetayUrun
                            .Where(u => u.OdemeYapanCariID == CariID)
                            .Select(u => u.AkaryakitTasimaDetayID)
                            .FirstOrDefault()))
                        .Sum(e => e.NakliyeFiyat);

                    var itemDto = CreateInvoiceDto(faturaList, toplamNakliyeFiyati, cari);
                    string jsonContent = JsonConvert.SerializeObject(itemDto);
                    using (HttpClient client = new HttpClient())
                    {

                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                        using var response = await client.PostAsync(url, content);
                        response.EnsureSuccessStatusCode();
                        sonuc = "ok";

                    }
                }
            }

            return sonuc;
        }

        public static async Task<string> Create1(int ID, int FirmaID)
        {
            var sonuc = "";
            var context = new Context();
            var firma = context.Firma.FirstOrDefault(e => e.Firma_ID == FirmaID);

            if (accessToken != null)
            {
                var tasima = context.AkaryakitTasima.FirstOrDefault(e => e.ID == ID);
                var tasimaDetay = context.AkaryakitTasimaDetay.Where(e => e.AkaryakitTasimaID == ID).ToList();
                var tasimaDetayUrun = context.AkaryakitTasimaDetayUrun.Where(e => e.AkaryakitTasimaID == ID).ToList();
                var faturaList = context.AkaryakitFatura.Where(e => e.AkaryakitTasimaID == ID).ToList();

                var GrupFaturaKesen = context.AkaryakitFatura
                    .Where(e => e.AkaryakitTasimaID == ID)
                    .GroupBy(x => x.FaturaKesenID)
                    .Select(group => group.Key)
                    .ToList();

                var GrupFaturaKesilen = context.AkaryakitFatura
                    .Where(e => e.AkaryakitTasimaID == ID)
                    .GroupBy(x => x.FaturaKesilenID)
                    .Select(group => group.Key)
                    .ToList();

                string apiUrl = "https://integration-test.gai.com.tr/api/IntegrationGidenFatura/Create";
                Guid ettn = Guid.NewGuid();

                foreach (var CariID in GrupFaturaKesilen)
                {
                    var cari = context.Cari.FirstOrDefault(e => e.Cari_ID == CariID);
                    var toplamNakliyeFiyati = context.AkaryakitTasimaDetay
                        .Where(e => e.ID == (context.AkaryakitTasimaDetayUrun
                            .Where(u => u.OdemeYapanCariID == CariID)
                            .Select(u => u.AkaryakitTasimaDetayID)
                            .FirstOrDefault()))
                        .Sum(e => e.NakliyeFiyat);

                    var itemDto = CreateInvoiceDto(faturaList, toplamNakliyeFiyati, cari);
                    string jsonContent = JsonConvert.SerializeObject(itemDto);

                    sonuc = await SendApiRequest(apiUrl, jsonContent);
                    if (sonuc.Contains("Hata"))
                    {
                        await Login(firma.Firma_EFatura_KullaniciAdi, firma.Firma_EFatura_Sifre);
                    }
                }
            }
            else
            {
                loginSayac++;
                sonuc = await Login(firma.Firma_EFatura_KullaniciAdi, firma.Firma_EFatura_Sifre);
                if (sonuc == "-1" && loginSayac <= 3)
                {
                    sonuc = "Login işlemi başarısız";
                }
                else
                {
                    return await Create(ID, FirmaID);
                }
            }
            return sonuc;
        }

        private static async Task<string> SendApiRequest(string apiUrl, string jsonContent)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = await client.PostAsync("", new StringContent(jsonContent, Encoding.UTF8, "application/json"));
                string responseBody = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return "Hata oluştu.";
                }

                if (jsonResponse == true)
                {
                    return "E-Fatura gönderildi";
                }
                else
                {
                    return "API isteği başarısız: " + jsonResponse.message;
                }
            }
        }

        private static object CreateInvoiceDto(List<AkaryakitFatura> faturaList, decimal toplamNakliyeFiyati, Cari cari)
        {
            return new List<object>
    {
        new
        {
            profile = "TICARIFATURA",
            invoiceType = "SATIS",
            ettn = Guid.NewGuid().ToString(),
            refNo = Guid.NewGuid(),
            prefix = "GIB",
            docDate = faturaList[0].DuzenlemeTarihi,
            currencyCode = "TRY",
            currencyRate = 1,
            isDraft = true,
            invoiceAccount = new
            {
                vknTckn = "3941069153",
                accountName = "Genesis Yapay Zeka Teknolojileri A.Ş.",
                countryName = "Türkiye",
                cityName = "İSTANBUL",
                district = "BEŞİKTAŞ",
                citySubdivision = "BEŞİKTAŞ",
                region = "Büyükdere Cad.",
                postalCode = "06530",
                streetName = "Şişli Levent Mah.",
                buildingName = "175",
                room = "7",
                blockName = "",
                taxOfficeName = "BEŞİKTAŞ",
                telephone = "5433494434",
                fax = "5433494434",
                email = "info@gai.com.tr",
                webSite = "www.gai.com.tr",
            },
            notes = new List<string> { "Test Fatura Notu "  },
            pkAlias = "urn:mail:defaultpk@gai.com.tr",

            invoiceDetail = new List<object>
            {
                new
                {
                    productName = "Test Ürün 1",
                    quantity = 5,
                    unitCode = "C62",
                    unitPrice = 38.14,
                    amount = 190.7,
                    kdvDahilTutar = 225.03,
                    tax = new List<object>
                    {
                        new
                        {
                            taxName = "GERÇEK USULDE KATMA DEĞER VERGİSİ",
                            taxCode = "0015",
                            taxRate = 20,
                            taxAmount = 34.33
                        }
                    }
                },
                new
                {
                    productName = "Test Ürün 2",
                    quantity = 4,
                    unitCode = "C62",
                    unitPrice = 84.75,
                    amount = 291.54,
                    kdvDahilTutar = 355.017,
                    tax = new List<object>
                    {
                        new
                        {
                            taxName = "GERÇEK USULDE KATMA DEĞER VERGİSİ",
                            taxCode = "0015",
                            taxRate = 18,
                            taxAmount = 149.5
                        }
                    },
                    allowanceCharge = new List<object>
                    {
                        new
                        {
                            rate = 14,
                            amount = 47.4645678
                        }
                    }
                },
                new
                {
                    productName = "Test Ürün 3",
                    quantity = 4,
                    unitCode = "C62",
                    unitPrice = 271.19,
                    amount = 983.334,
                    kdvDahilTutar = 355.017,
                    tax = new List<object>
                    {
                        new
                        {
                            taxName = "GERÇEK USULDE KATMA DEĞER VERGİSİ",
                            taxCode = "0015",
                            taxRate = 18,
                            taxAmount = 177
                        }
                    },
                    allowanceCharge = new List<object>
                    {
                        new
                        {
                            rate = 2,
                            amount = 21.6952
                        },
                        new
                        {
                            rate = 7.5,
                            amount = 79.72986
                        }
                    }
                },
                new
                {
                    productName = "Test Ürün 4",
                    quantity = 7,
                    unitCode = "C62",
                    unitPrice = 17.33,
                    amount = 118.81,
                    kdvDahilTutar = 199.998,
                    tax = new List<object>
                    {
                        new
                        {
                            taxName = "GERÇEK USULDE KATMA DEĞER VERGİSİ",
                            taxCode = "0015",
                            taxRate = 1,
                            taxAmount = 1.19
                        }
                    },
                    allowanceCharge = new List<object>
                    {
                        new
                        {
                            rate = 2.06,
                            amount = 2.5
                        }
                    }
                }
            },

            invoiceCalculation = new
            {
                taxExclusiveAmount = 2582.83,
                taxInclusiveAmount = 2274.49,
                taxAmount0 = 0.0,
                taxAmount1 = 1.19,
                taxAmount8 = 43.37,
                taxAmount9 = 0,
                taxAmount10 = 0,
                taxAmount18 = 263.81,
                taxAmount20 = 0,
                allowanceTotalAmount = 577.31,
                roundingAmount = 82.83
            },

            isCalculateByApi = true
        }
    };
        }

        public static async Task<string> qCreate(int ID, int FirmaID)
        {
            var sonuc = "";
            var context = new Context();
            var firma = context.Firma.FirstOrDefault(e => e.Firma_ID == FirmaID);

            if (accessToken != null)
            {
                AkaryakitTasima tasima = context.AkaryakitTasima.FirstOrDefault(e => e.ID == ID);
                List<AkaryakitTasimaDetay> tasimaDetay = context.AkaryakitTasimaDetay.Where(e => e.AkaryakitTasimaID == ID).ToList();
                List<AkaryakitTasimaDetayUrun> tasimaDetayUrun = context.AkaryakitTasimaDetayUrun.Where(e => e.AkaryakitTasimaID == ID).ToList();

                List<AkaryakitFatura> faturaList = context.AkaryakitFatura.Where(e => e.AkaryakitTasimaID == ID).ToList();

                var GrupFaturaKesen = context.AkaryakitFatura.Where(e => e.AkaryakitTasimaID == ID)
                    .GroupBy(x => x.FaturaKesenID)
                    .Select(group => group.Key)
                    .ToList();

                var GrupFaturaKesilen = context.AkaryakitFatura.Where(e => e.AkaryakitTasimaID == ID)
                   .GroupBy(x => x.FaturaKesilenID)
                   .Select(group => group.Key)
                   .ToList();
                Cari cari;
                // API'nin URL'si
                string apiUrl = "https://integration-test.gai.com.tr/api/IntegrationGidenFatura/Create";
                Guid ettn = Guid.NewGuid();
                // JSON verisini oluştur
                foreach (var CariID in GrupFaturaKesilen)
                {
                    cari = context.Cari.FirstOrDefault(e => e.Cari_ID == CariID);
                    var toplamNakliyeFiyati = context.AkaryakitTasimaDetay
                    .Where(e => e.ID == (context.AkaryakitTasimaDetayUrun
                            .Where(u => u.OdemeYapanCariID == CariID)
                            .Select(u => u.AkaryakitTasimaDetayID)
                            .FirstOrDefault()))
                    .Sum(e => e.NakliyeFiyat);


                    var itemDto = new List<object>
{
    new
    {
        profile = "TICARIFATURA",
        invoiceType = "SATIS",
        ettn = Guid.NewGuid().ToString(),
        refNo = Guid.NewGuid(),
        prefix = "GIB",
        docDate = faturaList[0].DuzenlemeTarihi,
        currencyCode = "TRY",
        currencyRate = 1,
        isDraft = true,
        invoiceAccount = new
        {
            vknTckn = "3941069153",
            accountName = "Genesis Yapay Zeka Teknolojileri A.Ş.",
            countryName = "Türkiye",
            cityName = "İSTANBUL",
            district = "BEŞİKTAŞ",
            citySubdivision = "BEŞİKTAŞ",
            region = "Büyükdere Cad.",
            postalCode = "06530",
            streetName = "Şişli Levent Mah.",
            buildingName = "175",
            room = "7",
            blockName = "",
            taxOfficeName = "BEŞİKTAŞ",
            telephone = "5433494434",
            fax = "5433494434",
            email = "info@gai.com.tr",
            webSite = "www.gai.com.tr",
        },
        notes = new List<string> { "Test Fatura Notu "  },
        pkAlias = "urn:mail:defaultpk@gai.com.tr", // INFO: bu değer mukellef sorgusunda gelen alias değerinden doldurulmalı yoksa hata dönecektir.

        // Mal/Hizmet Satır bilgileri
        invoiceDetail = new List<object>
        {
            new
            {
                productName = "Test Ürün 1", // INFO: Ürün Adı
                quantity = 5, // INFO: Ürün Miktarı
                unitCode = "C62", // INFO: Ürün Birimi
                unitPrice = 38.14, // INFO: Ürün Birim Fiyatı
                amount = 190.7, // INFO: Ürün Tutarı => amount = (quantity * unitPrice) - allowanceAmount
                kdvDahilTutar = 225.03,
                tax = new List<object>
                {
                    new
                    {
                        taxName = "GERÇEK USULDE KATMA DEĞER VERGİSİ",
                        taxCode = "0015",
                        taxRate = 20,
                        taxAmount = 34.33
                    }
                }
            },
            new
            {
                productName = "Test Ürün 2",
                quantity = 4,
                unitCode = "C62", // INFO: Ürün Birimi
                unitPrice = 84.75,
                amount = 291.54,
                kdvDahilTutar = 355.017,
                tax = new List<object>
                {
                    new
                    {
                        taxName = "GERÇEK USULDE KATMA DEĞER VERGİSİ",
                        taxCode = "0015",
                        taxRate = 18,
                        taxAmount = 149.5
                    }
                },
                allowanceCharge = new List<object>
                {
                    new
                    {
                        rate = 14,
                        amount = 47.4645678 // INFO: hesaplamalar Decimal.Round(x,2) şekilde yapılıp, gösterimler Decimal.Round(x,4) şeklinde olacaktır.
                    }
                }
            },
            new
            {
                productName = "Test Ürün 3",
                quantity = 4,
                unitCode = "C62", // INFO: Ürün Birimi
                unitPrice = 271.19,
                amount = 983.334,
                kdvDahilTutar = 355.017,
                tax = new List<object>
                {
                    new
                    {
                        taxName = "GERÇEK USULDE KATMA DEĞER VERGİSİ",
                        taxCode = "0015",
                        taxRate = 18,
                        taxAmount = 177
                    }
                },
                allowanceCharge = new List<object>
                {
                    new
                    {
                        rate = 2,
                        amount = 21.6952
                    },
                    new
                    {
                        rate = 7.5,
                        amount = 79.72986
                    }
                }
            },
            new
            {
                productName = "Test Ürün 4",
                quantity = 7,
                unitCode = "C62", // INFO: Ürün Birimi
                unitPrice = 17.33,
                amount = 118.81,
                kdvDahilTutar = 199.998,
                tax = new List<object>
                {
                    new
                    {

                        taxName = "GERÇEK USULDE KATMA DEĞER VERGİSİ",
                        taxCode = "0015",
                        taxRate = 1,
                        taxAmount = 1.19
                    }
                },
                allowanceCharge = new List<object>
                {
                    new
                    {
                        rate = 2.06,
                        amount = 2.5
                    }
                }
            }
        },

        invoiceCalculation = new
        {
            taxExclusiveAmount = 2582.83,
            taxInclusiveAmount = 2274.49,
            taxAmount0 = 0.0,
            taxAmount1 = 1.19,
            taxAmount8 = 43.37,
            taxAmount9 = 0,
            taxAmount10 = 0,
            taxAmount18 = 263.81,
            taxAmount20 = 0,
            allowanceTotalAmount = 577.31,
            roundingAmount = 82.83
        },

        isCalculateByApi = true // INFO: Satırda ve toplam mal hizmet bilgilerinin entegrasyon tarafından hesaplanması istendiğinde "isCalculateByApi = true" şeklinde gönderilmelidir. false gönderilmesi durumunda hesaplama yapılmadan gönderilen değerler aynı şekilde kaydedilir / gönderilir.
        
        // INFO: Burası dip iskonto bilgisi için kullanılmakta "discountAmount" değeri ya da "discountRate" olarak gönderilebilir ya da iki değerde gönderilebilir. İki değer de gönderilmesi durumunda tutar üzerinden hesaplanacaktır.
        //bottomDiscountDetails = new 
        //{
        //    discountAmount = 10
        //}
    }
};



                    /*
                                        var faturaNesnesi = new
                                        {

                                                accountingCost= "",
                                                bankAccountId= "",
                                                billingRefInvoiceDate= "0001-01-01T00:00:00",
                                                billingRefInvoiceNo= "",
                                                billingRefNote= "",
                                                bottomDiscountDetails= "",
                                                cargoAccountName= "",
                                                cargoNumber= "",
                                                currencyCode= "TRY",
                                                currencyRate= 1.0,
                                                declaredForCarriageValueAmount= 0.0,
                                                deliveryCity= "",
                                                deliveryCitySubdivisionName= "",
                                                deliveryCountry= "",
                                                docDate= "2024-05-15T16:43:17.2660713Z",
                                                docNo= "",
                                                ettn= "999b9284-ceb6-428a-afa3-013b371e8b12",
                                                insuranceValueAmount= 0.0,
                                                internetShipmentInfo= "",
                                                invoiceAccount= new {
                                                    accountName= "GENESİS TEKNOLOJİ BİLİŞİM ANONİM ŞİRKETİ",
                                                    blockName= "",
                                                    buildingName= "Bina Adı",
                                                    cityName= "Ankara",
                                                    citySubdivision= "Çankaya",
                                                    countryName= "Türkiye",
                                                    district= "Çankaya",
                                                    documentNumber= "",
                                                    email= "salih.altun@gai.com.tr",
                                                    fax= "5551110000",
                                                    partyIdentificationName= "",
                                                    partyIdentificationVkn= "",
                                                    passportIssueDate= "0001-01-01T00:00:00",
                                                    passportNumber= "",
                                                    postalCode= "06530",
                                                    publicServicePayeeVKN= "",
                                                    region= "Çankaya Mahallesi",
                                                    registrationVkn= "",
                                                    room= "280/6 – 1260",
                                                    streetName= "Dumlupınar Bulvarı",
                                                    supplierCode= "",
                                                    taxOfficeName= "Maltepe",
                                                    telephone= "5333010020",
                                                    vknTckn= "11111111125",
                                                    webSite= "www.gai.com.tr"
                                                        },
                                                invoiceCalculation=new  {
                                                    allowanceTotalAmount= 577.31,
                                                    gidenFaturaDigerVergiDto= "",
                                                    roundingAmount= 82.83,
                                                    taxAmount0= 0.0,
                                                    taxAmount1= 1.19,
                                                    taxAmount10= 0.0,
                                                    taxAmount18= 263.81,
                                                    taxAmount20= 0.0,
                                                    taxAmount8= 43.37,
                                                    taxAmount9= 0.0,
                                                    taxExclusiveAmount= 2582.83,
                                                    taxInclusiveAmount= 2274.49
                                                        },
                                                invoiceDetail= new {
                                                        allowanceCharge= "",
                                                        amount= 190.7,
                                                        brandName= "",
                                                        buyerProductCode= "",
                                                        cargoNo= "",
                                                        deliveryTermCode= "",
                                                        gtip= "",
                                                        hksOwnerFullName= "",
                                                        hksOwnerIdentifier= "",
                                                        hksTagNumber= "",
                                                        kdvDahilTutar= 225.03,
                                                        malHizmetKaydet= false,
                                                        manufacturerProductCode= "",
                                                        modelName= "",
                                                        note= "",
                                                        originCountryCode= "",
                                                        packageList= "",
                                                        productCode= "",
                                                        productName= "Test Ürün 1",
                                                        productTraceId= "",
                                                        quantity= 5.0,
                                                        tax= new
                                                                    {
                                                                taxAmount= 34.33,
                                                                taxCode= "0015",
                                                                taxExemptionReasonCode= "",
                                                                taxName= "GERÇEK USULDE KATMA DEĞER VERGİSİ",
                                                                taxRate= 20.0
                                                                    }
                                                                ,
                                                        transportModeCode= "",
                                                        unitCode= "C62",
                                                        unitPrice= 38.14
                                                            },
                                                invoiceType= "SATIS",
                                                isCalculateByApi= true,
                                                isDraft= true,
                                                notes= "Gai Test Fatura Notu 15.05.2024 16:43:17",
                                                orderDetails= "",
                                                paymentMeans= "",
                                                pkAlias= "urn:mail:defaultpk@gai.com.tr",
                                                prefix= "GIB",
                                                profile= "TICARIFATURA",
                                                publicServicePayeeCity= "",
                                                publicServicePayeeCitysubdivision= "",
                                                publicServicePayeeCountry= "",
                                                publicServicePayeePartyName= "",
                                                refNo= "dec0501f-4050-4677-b5a6-bee78f3c91a3",
                                                senderType= "",
                                                wayBillInfo= "",
                                                xsltName= ""

                                        };
                    */

                    // JSON nesnesini string'e dönüştür
                    string jsonContent = JsonConvert.SerializeObject(itemDto);

                    // String'i içeren HttpContent oluştur
                    HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                    sonuc = " Hata oluştu.";


                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://integration-test.gai.com.tr/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                        HttpResponseMessage resp = client.PostAsync("api/IntegrationGidenFatura/Create", new StringContent(JsonConvert.SerializeObject(itemDto), Encoding.UTF8, "application/json")).Result;
                        if (resp != null && resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            await Login(firma.Firma_EFatura_KullaniciAdi, firma.Firma_EFatura_Sifre);
                        }
                        string responseBody = await resp.Content.ReadAsStringAsync();

                        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);
                        if (jsonResponse == true)
                            sonuc = "E-Fatura gönderildi";
                        else
                            sonuc = "API isteği başarısız: " + jsonResponse.message;
                    }



                }
            }
            else
            {
                loginSayac++;
                sonuc = await Login(firma.Firma_EFatura_KullaniciAdi, firma.Firma_EFatura_Sifre);
                if (sonuc == "-1" && loginSayac <= 3)
                {
                    sonuc = "Login işlemi başarısız";
                }
                else
                    return await Create(ID, FirmaID);
            }
            return sonuc;
        }
    }
}
