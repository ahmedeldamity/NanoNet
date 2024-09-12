using Microsoft.Extensions.Options;

namespace NanoNet.Web.Utility
{
    public static class SD
    {
        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }
        public static string ProductAPIBase { get; set; }
        public static string CartAPIBase { get; set; }
        public static string OrderAPIBase { get; set; }


        public const string RoleAdmin = "Admin";

        public const string RoleUser = "Client";

        public const string TokenName = "JWTToken";

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public static void AddPropertiesValueForUnityClass(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            var apiData = serviceProvider.GetRequiredService<IOptions<APIsUrl>>().Value;

            CouponAPIBase = apiData.CouponAPI;
            AuthAPIBase = apiData.AuthAPI;
            ProductAPIBase = apiData.ProductAPI;
            CartAPIBase = apiData.CartAPI;
            OrderAPIBase = apiData.OrderAPI;
        }
    }
}
