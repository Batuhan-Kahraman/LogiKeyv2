using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;

namespace logikeyv2.Models.GaiEFaturaModels
{
	public class GaiInvoiceCreateModel
	{
        public GaiInvoiceCreateModel()
        {
            InvoiceDetail = new List<InvoiceDetail>();
            InvoiceAccount = new GaiInvoiceAccount();
            WayBillInfo = new List<WayBillInfo>();
            InternetShipmentInfo = new InternetShipmentInfo();
            OrderDetails = new OrderDetails();
            PaymentMeans = new PaymentMeans();
            InvoiceCalculation = new InvoiceCalculation();
            BottomDiscountDetails = new BottomDiscountDetails();
        }
        //Senaryo türü belirtilmelidir(TEMELFATURA,TICARIFATURA vb.) Ayrıca faturanın e-fatura mı yoksa e-arşiv fatura mı olacağı bilgisi bu parametre ile belirtilmelidir.
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
        public GaiInvoiceAccount? InvoiceAccount { get; set; }
		//Faturaya eklenecek irsaliye bilgisidir. Fatura'nın hangi irsaliyeye konu olduğunu belirtmektedir. Birden fazla irsaliye bilgisi eklenebilir.
        public List<WayBillInfo>? WayBillInfo { get; set; }
        public InternetShipmentInfo? InternetShipmentInfo { get; set; }
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
        public  double InsuranceValueAmount { get; set; }
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
        public OrderDetails? OrderDetails { get; set; }
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
        public PaymentMeans? PaymentMeans { get; set; }
        public IList<InvoiceDetail> InvoiceDetail { get; set; }
        public InvoiceCalculation? InvoiceCalculation { get; set; }
        //Tutar değerleri api'de hesaplansın mı? Bu parametre true olması durumunda fatura satırlarında birim, birim fiyat, kdv oranı gibi değerlerin servise gönderilmesi
        //yeterli olup satır tutarı ve fatura tutarı alanları servis tarafından hesaplanacaktır.
        public bool IsCalculateByApi { get; set; }
		//Referans Belge Numarası alanıdır. Fatura oluşturan ve sisteme entegre olan firma/kişi tarafından üretilip gönderilen faturaya verilecek referans id değeridir.
        //Bu değer sayesinde entegre olacak program ile entegratör arasında fatura takibinin yapılması açısından önemlidir.
		public string? RefNo { get; set; }
        public BottomDiscountDetails? BottomDiscountDetails { get; set; }
    }
    public class GaiCreateInvoiceResponse
    {
        public GaiCreateInvoiceResponse()
        {
            data = new List<ResponseData>();
            ErrorList = new List<string>();
        }
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? ExceptionMessage { get; set; }
        public List<ResponseData>? data { get; set; }
        public List<string>? ErrorList { get; set; }
    }
    public class ResponseData
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? ExceptionMessage { get; set; }
        public string? Ettn { get; set; }
        public string? DocumentNo { get; set; }
        public string? RefNo { get; set; }
    }
    public class BottomDiscountDetails
    {
        public double? DiscountAmount { get; set; }
        public double? DiscountRate { get; set; }
    }
	public class InvoiceCalculation
    {
        public InvoiceCalculation()
        {
            GidenFaturaDigerVergiDto = new List<GidenFaturaDigerVergiDto>();
        }
        public double LineExtensionAmount { get; set; }
        public double TaxExclusiveAmount { get; set; }
        public double TaxInclusiveAmount { get; set; }
        public double TaxAmount0 { get; set; }
        public double TaxAmount1 { get; set; }
        public double TaxAmount8 { get; set; }
        public double TaxAmount9 { get; set; }
        public double TaxAmount10 { get; set; }
        public double TaxAmount18 { get; set; }
        public double TaxAmount20 { get; set; }
        public double AllowanceTotalAmount { get; set; }
        public List<GidenFaturaDigerVergiDto>? GidenFaturaDigerVergiDto { get; set; }
		public double RoundingAmount { get; set; }
		public double PayableAmount { get; set; }
	}
    public class GidenFaturaDigerVergiDto
    {
        public string? TaxName { get; set; }
        public double TaxAmount { get; set; }
    }
    public class InvoiceDetail
    {
        public InvoiceDetail()
        {
            AllowanceCharge = new List<AllowanceCharge>();
            Tax = new List<Tax>();
            PackageList = new List<PackageList>();
        }
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
        public IList<AllowanceCharge>? AllowanceCharge { get; set; }
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
        public IList<PackageList>? PackageList { get; set; }
        public IList<Tax>? Tax { get; set; }
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
    }
    public class Tax
    {
		//Vergi Adı, uygulanan vergiye ait ad değerini referans alır.
		public string? TaxName { get; set; }
		//Vergi Kodu, uygulanan vergiye ait kod değerini referans alır.
		public string? TaxCode { get; set; }
		//Vergi Oranı, uygulanan vergi oran değerini referans alır.
		public double TaxRate { get; set; }
		//Vergi Tutarı, uygulanan vergi tutar değerini referans alır.
		public double TaxAmount { get; set; }
		//Kdv Muafiyet Kodu ,"invoiceType" değeri "ISTISNA" olduğunda ve Kdv Oranı "0''a eşit olduğunda zorunludur. (201,301,101,801,701 vb.)
		public string? TaxExemptionReasonCode { get; set; }
    }
    public class PackageList
    {
        public string? PackageType { get; set; }
        public string? packageNumber { get; set; }
        public string? packageBrand { get; set; }
        public double netQuantity { get; set; }
        public double packageQuantity { get; set; }
    }
	public class AllowanceCharge
    {
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string? Description { get; set; }
    }
    public class PaymentMeans
    {
		//"paymentMeansCode" (Ödeme Şekli) 1,10,30,48,97 değerlerinden biri olmalıdır.
		public string? PaymentMeansCode { get; set; }
		public string? DueDate { get; set; }
		public string? AccountNumber { get; set; }
		public string? PaymentNote { get; set; }
		//"paymentChannelCode" (Ödeme Kanalı) 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15 değerlerinden biri olmalıdır.
		public string? PaymentChannelCode { get; set; }
		public string? CurrencyCode { get; set; }
    }
	public class WayBillInfo
    {
        public string? WayBillNo { get; set; }
        public string? WayBillDate { get; set; }
    }
    public class InternetShipmentInfo
    {
        public string? WebSite { get; set; }
        public string? ShippingDate { get; set; }
        public string? ShippingAccountVknTckn { get; set; }
        public string? ShippingAccountName { get; set; }
        public string? PaymentDate { get; set; }
        public string? InternetAccountName { get; set; }
        public string? PaymentType { get; set; }
    }
    public class OrderDetails
    {
        public string? OrderNumber { get; set; }
        public string? OrderDate { get; set; }
        public string? OrderAcceptNumber { get; set; }
    }
}
