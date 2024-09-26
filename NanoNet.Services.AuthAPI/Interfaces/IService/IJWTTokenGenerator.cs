using Microsoft.AspNetCore.Identity;
using NanoNet.Services.AuthAPI.Models;

namespace NanoNet.Services.AuthAPI.Interfaces.IService;
public interface IJWTTokenGenerator
{
	Task<string> GenerateTokenAsync(AppUser appUser, UserManager<AppUser> userManager);
}