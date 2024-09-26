using static NanoNet.Web.Utility.SD;

namespace NanoNet.Web.ViewModels;
public class RequestViewModel
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; } = null!;
    public object Data { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
}