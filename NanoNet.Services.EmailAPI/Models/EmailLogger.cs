namespace NanoNet.Services.EmailAPI.Models;
public class EmailLogger
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime? EmailMessage { get; set; }
}