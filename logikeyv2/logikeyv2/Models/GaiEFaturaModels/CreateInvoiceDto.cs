namespace logikeyv2.Models.GaiEFaturaModels
{
    public class CreateInvoiceDto
    {
        public string Profile { get; set; }
        //Belge tipi belirtilmelidir(SATIS,IADE vb.) İade fatura ise aşağıdaki 3 alan doldurulmalıdır
        public string InvoiceType { get; set; }
        //İade faturası gönderilmesi durumunda iadeye konu olan fatura numarası bu parametre ile belirtilir. Bu parametre invoiceType IADE olması durumunda dikkate alınır.
        public string? BillingRefInvoiceNo { get; set; }
        //İade faturası gönderilmesi durumunda iadeye konu olan fatura tarihi bu parametre ile belirtilir. Bu parametre "invoiceType" alanı "IADE" olması durumunda dikkate alınır.
        public string? BillingRefInvoiceDate { get; set; }
        //İade faturası gönderilmesi durumunda iadeye konu olan fatura için varsa açıklama eklenecek parametredir. Bu parametre "invoiceType" alanı "IADE" olması durumunda dikkate alınır.
        public string? BillingRefNote { get; set; }
        //Ettn bilgisi girilmelidir. GUID formatında girilmelidir.
        public string Ettn { get; set; }
        //Fatura numarası bilgisi girilmelidir. (Fatura no ya da sayaç bilgisinden birisi gönderilmelidir. Eğer ikiside gönderilirse fatura numarası kabul edilecektir.)
        // E fatura için = UZF-2024000000001 ile
        // E arşiv için = UZA-2024000000001 gibi bir numara
        public string? DocNo { get; set; }
        // Datetime'ı stringe çevirip yollanması gerekiyor
        public string? DocDate { get; set; }
        //Para birimi değeri gönderilmelidir.Türk Lirası dışında bir para birimi gönderilmesi durumunda currencyRate(Kur bilgisi) alanı zorunludur.
        public string CurrencyCode { get; set; }
        public string? CurrencyRate { get; set; }
        //Gönderilen belgenin taslak olarak kalması bu paramtre ile ayarlanır. True durumunda gönderilen fatura taslak olarak sadece kaydedilir.
        //False olması durumunda ise fatura kaydedilir ve ardından hemen GİB'e gönderilir.
        public bool IsDraft { get; set; }
        //E-Arşiv fatura için kullanılan gönderim tipi bilgisidir (ELEKTRONIK,KAGIT)
        public string? SenderType { get; set; }

        public string? VknTckn { get; set; }
        public string? AccountName { get; set; }
        public string? TaxOfficeName { get; set; }
        public string? CountryName { get; set; }
        public string? CityName { get; set; }
        public string? Room { get; set; }
        public string? StreetName { get; set; }
        public string? BlockName { get; set; }
        public string? BuildingName { get; set; }
        public string? CitySubdivision { get; set; }
        public string? PostalCode { get; set; }
        public string? Region { get; set; }
        public string? District { get; set; }
        public string? Telephone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? WebSite { get; set; }
        public string? PublicServicePayeeVKN { get; set; }
        public string? RegistrationVkn { get; set; }
        public string? PassportNumber { get; set; }
        public string? PartyIdentificationVkn { get; set; }
        public string? PartyIdentificationName { get; set; }
        public string? DocumentNumber { get; set; }
        public string? PassportIssueDate { get; set; }
        public string? SupplierCode { get; set; }

        public string? Notes { get; set; }
        //Alıcının posta kutusu bilgisi alanıdır.
        #region İhracat Faturası Alanları
        public string? PkAlias { get; set; }
        //Teslim ilçe bilgisi (İhracat faturası oluşturulması durumunda gereklidir)
        public string? DeliveryCitySubdivisionName { get; set; }
        //Teslim il bilgisi (İhracat faturası oluşturulması durumunda gereklidir)
        public string? DeliveryCity { get; set; }
        //Teslim ülke bilgisi (İhracat faturası oluşturulması durumunda gereklidir)
        public string? DeliveryCountry { get; set; }
        //Kargo firmasının adı (İhracat faturası oluşturulması durumunda gereklidir)
        public string? CargoAccountName { get; set; }
        //Kargo Numarası (İhracat faturası oluşturulması durumunda gereklidir)
        public string? CargoNumber { get; set; }
        //Sigorta Bedeli (İhracat faturalarında isteğe bağlı doldurulabilecek alandır.)
        public double InsuranceValueAmount { get; set; }
        //Navlun Bedeli (İhracat faturalarında isteğe bağlı doldurulabilecek alandır.)
        public double DeclaredForCarriageValueAmount { get; set; }
        #endregion
        //xsltName, sistemde tanımlı bir tasarım kullanmak istendiğinde tanımlı Xslt şablona ait isim değeri göndererek kullanılması sağlanır.
        public string? XsltName { get; set; }
        //string <uuid> Banka hesap id bilgisi , sistemimizde tanımlı bir banka hesabının varsa faturada onun iban bilgilirin gösterilmesi için setleyebileceğiniz alan
        public string? BankAccountId { get; set; }
        //İkincil fatura türü alanıdır. SGK faturası oluşturulması durumunda kullanılır.
        public string? AccountingCost { get; set; }
        //Fatura Öneki, fatura numarası gönderilmediğinde, verilecek fatura numarasının önekidir (Örn:ABC).
        //Bu parametrenin kullanılması için portal'den web servis kullanımı için ön ek tanımı yapılmalıdır.
        public string? Prefix { get; set; }

        #region Kamu Faturası Alanları
        //Kamu unvan , "KAMU" faturalarında bu alanın doldurulması zorunludur.
        public string? PublicServicePayeePartyName { get; set; }
        //Kamu ülke , "KAMU" faturalarında bu alanın doldurulması zorunludur.
        public string? PublicServicePayeeCountry { get; set; }
        //Kamu il , "KAMU" faturalarında bu alanın doldurulması zorunludur.
        public string? PublicServicePayeeCity { get; set; }
        //Kamu ilçe , "KAMU" faturalarında bu alanın doldurulması zorunludur.
        public string? PublicServicePayeeCitysubdivision { get; set; }
        #endregion

        //Mal Hizmet Bilgisi
        public string ProductName { get; set; }
        //Mal Hizmet Miktarı
        public double Quantity { get; set; }
        //Mal / Hizmete ait Birim Kodu (C62,KGM,PR,MTR,MTQ,LTR,GRM,MTK,CTM vb.)
        public string UnitCode { get; set; }
        //Mal / Hizmetin Birim Fiyatı
        public double UnitPrice { get; set; }
        //Toplam Tutar
        public double Amount { get; set; }
        public double KdvDahilTutar { get; set; }
        //Üretici Kodu
        public string? manufacturerProductCode { get; set; }
        //Alıcı Kodu
        public string? buyerProductCode { get; set; }
        //Satıcı Kodu
        public string? productCode { get; set; }
        //Marka Adı
        public string? BrandName { get; set; }
        //Model Adı
        public string? ModelName { get; set; }
        //Satır Açıklaması
        public string? Note { get; set; }
        //Menşei Ülke Kısa Adı (TR , AC , AD ,AE vb.)
        public string? OriginCountryCode { get; set; }
        // Mal hizmet kaydı yapılsın mı
        public bool MalHizmetKaydet { get; set; }
        //İhracat Teslim Şekli, ihracat fatura gönderme işlemi gerçekleştiğinde bu alanda doldurulmalıdır. (CFR,CIF,CIP,CPT,DAF,DAP vb.)
        public string? DeliveryTermCode { get; set; }
        //İhracat GTİP No Bilgisi
        public string? Gtip { get; set; }
        //Taşıma Şekli, İhracat faturalarında gelecek mal/ hizmet bilgisinin hangi yolla geleceği bilgisi , (1,2,3,4,5,6,7,8 vb.)
        public string? TransportModeCode { get; set; }
        //İhracat Gümrük Takip No
        public string? ProductTraceId { get; set; }
        //HKS Künye Numarası
        public string? HksTagNumber { get; set; }
        //HKS Mal sahibi ünvan veya adı
        public string? HksOwnerFullName { get; set; }
        //HKS Mal Sahibi Vkn Tckn
        public string? HksOwnerIdentifier { get; set; }
        //Kargo Numarası
        public string? CargoNo { get; set; }
        //Tevkifat Kodu
        public string? WithHoldingTaxCode { get; set; }
        //Tevkifat Tutarı
        public string? WithHoldingTaxTotal { get; set; }
        public bool IsCalculateByApi { get; set; }
        //Referans Belge Numarası alanıdır. Fatura oluşturan ve sisteme entegre olan firma/kişi tarafından üretilip gönderilen faturaya verilecek referans id değeridir.
        //Bu değer sayesinde entegre olacak program ile entegratör arasında fatura takibinin yapılması açısından önemlidir.
        public string? RefNo { get; set; }

    }
}
