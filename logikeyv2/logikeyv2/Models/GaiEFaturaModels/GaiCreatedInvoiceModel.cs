namespace logikeyv2.Models.GaiEFaturaModels
{
    public class GaiCreatedInvoiceModel
    {
        public string? DocNo { get; set; }
        public string? Ettn { get; set; }
        public string? AccountName { get; set; }
        public string? Identifier { get; set; }
        public string? DocStatus { get; set; }
        public string? DocDate { get; set; }
        public string? CreateDate { get; set; }
        public double TaxTotal { get; set; }
        public double Tax20 { get; set; }
        public double Tax18 { get; set; }
        public double Tax10 { get; set; }
        public double Tax9 { get; set; }
        public double Tax8 { get; set; }
        public double Tax1 { get; set; }
        public double PayableAmount { get; set; }
        public double TaxExclusiveAmount { get; set; }
        public double CurrencyRate { get; set; }
        public string? Profile { get; set; }
        public string? InvoiceType { get; set; }
        public string? CurrencyCode { get; set; }
        public string? UblXml { get; set; }
        public bool IsRead { get; set; }
        public string? RefNo { get; set; }
    }
}
