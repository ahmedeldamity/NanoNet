namespace NanoNet.Web.Interfaces.IService;
public interface ITokenProvider
{
    void SetToken(string token);
    string? GetToken();
    void ClearToken();
}