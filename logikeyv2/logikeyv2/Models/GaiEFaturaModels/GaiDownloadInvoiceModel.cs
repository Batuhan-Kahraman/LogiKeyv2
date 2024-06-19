namespace logikeyv2.Models.GaiEFaturaModels
{
    public class GaiDownloadInvoiceModel
    {
        //İndirilmek istenen faturaların ETTN bilgisi liste şeklinde verilmelidir.
        public List<string> EttnList { get; set; }
        //Görüntülenmek istenen faturanın gönderilirken belirlendiği tasarımı ile mi yoksa GİB'in standart tasarımı ile mi görüntülenmesi tercihini belirtir.
        //False ya da null olması durumunda fatura mevcut tasarımıyla görüntülenecektir.
        public bool IsDefaultXslt { get; set; }
    }
    public class GaiDownloadInvoiceResponse
    {
        public string? Message { get; set; }
        public bool Status { get; set; }
        public string? ByteArray { get; set; }
        public string? Html { get; set; }
        public string? ExceptionMessage { get; set; }
    }
}
