using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NanoNet.Services.AuthAPI.Interfaces.IService;
using NanoNet.Services.AuthAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NanoNet.Services.AuthAPI.Services;
public class JWTTokenGenerator(IOptions<JwtOptions> jwtOptions) : IJWTTokenGenerator
{
	private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<string> GenerateTokenAsync(AppUser user, UserManager<AppUser> userManager)
	{
		var authClaims = new List<Claim>
		{
			new (ClaimTypes.Name, user.UserName!.Split('@')[0]),
			new (ClaimTypes.Email, user.Email!),
			new (ClaimTypes.NameIdentifier, user.Id)
		};

		var userRoles = await userManager.GetRolesAsync(user);

		authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

		var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

		var token = new JwtSecurityToken(
			issuer: _jwtOptions.ValidIssuer,
			audience: _jwtOptions.ValidAudience,
			expires: DateTime.UtcNow.AddDays(double.Parse(_jwtOptions.DurationInDays)),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}