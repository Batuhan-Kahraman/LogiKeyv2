namespace logikeyv2.Models.GaiEFaturaModels
{
    public class GaiCheckUserModel
    {
        //Sorgulanacak mükellefin Vkn/Tckn bilgisi girilmelidir.
        public string Identifier { get; set; }
        //E-Fatura için "Invoice" / E-İrsaliye için "DespatchAdvice" değeri girilmelidir.
        public string DocumentType { get; set; }
    }
    public class GaiCheckUserResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public bool data { get; set; }
    }
}
