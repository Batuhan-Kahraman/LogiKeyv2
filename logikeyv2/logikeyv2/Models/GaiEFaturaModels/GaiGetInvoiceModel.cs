namespace logikeyv2.Models.GaiEFaturaModels
{
    public class GaiGetInvoiceModel
    {
        public string? RefNo { get; set; }
        public string? Ettn { get; set; }
        //Detayların gelip gelmeyeceği bilgisidir.
        //True verilmesi durumunda reponse içerisindeki UblXml değerinden faturanın xml datasına, bu datanın içinden de fatura satır/detay bilgilerine ulaşabilirsiniz.
        public bool IsDetail { get; set; }
    }
    public class GaiGetInvoiceResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? ExceptionMessage { get; set; }
        public GaiCreatedInvoiceModel? data { get; set; }
    }
}
