namespace NanoNet.Services.ProductAPI.SettingData;
public class JwtData
{
    public string SecretKey { get; init; } = null!;
    public string ValidAudience { get; init; } = null!;
    public string ValidIssuer { get; init; } = null!;
    public double DurationInMinutes { get; init; }
}