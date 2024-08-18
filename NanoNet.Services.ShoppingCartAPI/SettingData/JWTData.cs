namespace NanoNet.Services.ShoppingCartAPI.SettingData
{
    public class JWTData
    {
        public string SecretKey { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
