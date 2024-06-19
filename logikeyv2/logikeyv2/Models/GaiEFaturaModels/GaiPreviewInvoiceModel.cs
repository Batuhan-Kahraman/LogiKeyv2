namespace logikeyv2.Models.GaiEFaturaModels
{
    public class GaiPreviewInvoiceModel
    {
        //Görüntülenmek istenen faturanın ETTN bilgisi girilmelidir.
        public string Ettn { get; set; }
        //Görüntülenmek istenen faturanın gönderilirken belirlendiği tasarımı ile mi yoksa GİB'in standart tasarımı ile mi görüntülenmesi tercihini belirtir.
        //False ya da null olması durumunda fatura mevcut tasarımıyla görüntülenecektir.
        public bool IsDefaultXslt { get; set; }
    }
    public class GaiPreviewInvoiceResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? ExceptionMessage { get; set; }
        public string? Data { get; set; }
    }
}
