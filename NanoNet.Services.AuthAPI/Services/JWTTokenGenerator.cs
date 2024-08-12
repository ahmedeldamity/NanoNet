using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NanoNet.Services.AuthAPI.Interfaces.IService;
using NanoNet.Services.AuthAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NanoNet.Services.AuthAPI.Services
{
	public class JWTTokenGenerator : IJWTTokenGenerator
	{
		private readonly JwtOptions _jwtOptions;

		public JWTTokenGenerator(IOptions<JwtOptions> jwtOptions)
		{
			_jwtOptions = jwtOptions.Value;
		}

		public async Task<string> GenerateTokenAsync(AppUser user, UserManager<AppUser> userManager)
		{
			// Private Claims (user defined - can change from user to other)
			var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserName.Split('@')[0]),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.NameIdentifier, user.Id)
			};

			var userRoles = await userManager.GetRolesAsync(user);

			authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

			// secret key
			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

			// Token Object
			var token = new JwtSecurityToken(
				// Registered Claims
				issuer: _jwtOptions.ValidIssuer,
				audience: _jwtOptions.ValidAudience,
				expires: DateTime.UtcNow.AddDays(double.Parse(_jwtOptions.DurationInDays)),
				// Private Claims
				claims: authClaims,
				// Signature Key
				signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
			);

			// Create Token And Return It
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
