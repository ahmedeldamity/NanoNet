namespace NanoNet.Web.Utility
{
    public static class SD
    {
        public static string CouponAPIBase { get; set; }

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public static void AddPropertiesValueForUnityClass(this IConfiguration configuration)
        {
            CouponAPIBase = configuration["ServiceUrls:CouponAPI"];
        }
    }
}
