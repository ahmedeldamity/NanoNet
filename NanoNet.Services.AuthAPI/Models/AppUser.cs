using Microsoft.AspNetCore.Identity;

namespace NanoNet.Services.AuthAPI.Models;
public class AppUser: IdentityUser
{
    public string Name { get; set; } = null!;
}