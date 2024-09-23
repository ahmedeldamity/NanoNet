namespace NanoNet.Web.ViewModels;
public class ResponseViewModel
{
    public bool IsSuccess { get; set; } = true;
    public object? Value { get; set; }
    public string Error { get; set; } = null!;
}