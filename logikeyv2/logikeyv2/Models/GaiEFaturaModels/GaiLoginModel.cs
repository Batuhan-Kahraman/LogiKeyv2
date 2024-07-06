namespace logikeyv2.Models.GaiEFaturaModels
{
    public class GaiLoginModel
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class GaiLoginResponseModel
    {
        public string RefreshToken { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public JwtToken JwtToken { get; set; }
    }

    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }
    }

    public class JwtToken
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
