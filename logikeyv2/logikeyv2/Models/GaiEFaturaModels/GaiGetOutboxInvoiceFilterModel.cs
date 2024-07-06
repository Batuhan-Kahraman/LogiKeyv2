namespace logikeyv2.Models.GaiEFaturaModels
{
    public class GaiGetOutboxInvoiceFilterModel
    {
        //Fatura numarası
        public string? DocNo { get; set; }
        //Unvan
        public string? AccountName { get; set; }
        //Vkn Tckn Bilgisi
        public string? Identifier { get; set; }
        //Fatura Senaryo Tipi
        public List<string>? Profile { get; set; }
        //Fatura Belge Tipi
        public List<string>? InvoiceType { get; set; }
        public string? Ettn { get; set; }
        public bool? IsRead { get; set; }
        //Başlangıç tarihi (Başlangıç ve bitiş tarihi aralığı 90 günden fazla olmamalıdır.)
        public string? StartDate { get; set; }
        //Bitiş Tarihi
        public string? EndDate { get; set; }
        //UblXml bilgisi getirilsin mi
        public bool IsDetail { get; set; }
        //Referans Numara Listesi
        public List<string>? RefNoList { get; set; }
        //Getirilecek sayfa değeri
        public int PageCount { get; set; }
        //Getirilecek kayıt sayısı
        public int PageSize { get; set; }
    }

    public class GaiGetOutboxInvoiceResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? ExceptionMessage { get; set; }
        public List<GaiCreatedInvoiceModel>? data { get; set; }
    }
}
