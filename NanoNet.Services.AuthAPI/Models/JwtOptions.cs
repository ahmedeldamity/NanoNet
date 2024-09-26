namespace NanoNet.Services.AuthAPI.Models;
public class JwtOptions
{
    public string ValidIssuer { get; set; } = null!;

	public string ValidAudience { get; set; } = null!;

    public string SecretKey { get; set; } = null!;

    public string DurationInDays { get; set; } = null!;
}