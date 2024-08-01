using Microsoft.AspNetCore.Identity;
using NanoNet.Services.AuthAPI.Data;
using NanoNet.Services.AuthAPI.Dtos;
using NanoNet.Services.AuthAPI.Interfaces.IService;
using NanoNet.Services.AuthAPI.Models;

namespace NanoNet.Services.AuthAPI.Services
{
	public class AuthService : IAuthService
	{
		private readonly IdentityContext _identityContext;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ILogger<AuthService> _logger;
		private readonly IJWTTokenGenerator _jWTTokenGenerator;

		public AuthService(IdentityContext identityContext, UserManager<AppUser> userManager, 
			RoleManager<IdentityRole> roleManager, ILogger<AuthService> logger, IJWTTokenGenerator jWTTokenGenerator)
		{
			_identityContext = identityContext;
			_userManager = userManager;
			_roleManager = roleManager;
			_logger = logger;
			_jWTTokenGenerator = jWTTokenGenerator;
		}

		public async Task<ResponseDto> Register(RegisterationRequestDto requestDto)
		{
			if (requestDto is null || (!IsValidEmail(requestDto.Email)))
			{
				return new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid registration data."
				};
			}

			var userExist = await _userManager.FindByEmailAsync(requestDto.Email);

			if (userExist is not null)
			{
				return new ResponseDto
				{
					IsSuccess = false,
					Message = "This email has already been used."
				};
			}

			AppUser user = new AppUser()
			{
				UserName = requestDto.Email,
				Email = requestDto.Email,
				NormalizedEmail = requestDto.Email.ToUpper(),
				Name = requestDto.Name,
				PhoneNumber = requestDto.PhoneNumber,
			};

			try
			{
				var result = await _userManager.CreateAsync(user, requestDto.Password);

				if (result.Succeeded)
				{
					_logger.LogInformation($"User {requestDto.Email} registered successfully.");

					var userToReturn = new UserDto()
					{
						Email = requestDto.Email,
						Name = requestDto.Name,
						PhoneNumber = requestDto.PhoneNumber
					};

					return new ResponseDto
					{
						IsSuccess = true,
						Message = "Registration successful.",
						Result = userToReturn
					};
				}
				else
				{
					var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
					_logger.LogWarning($"Registration failed for user {requestDto.Email}: {errorMessage}");

					return new ResponseDto
					{
						IsSuccess = false,
						Message = errorMessage
					};
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, $"Error occurred during registration for user {requestDto.Email}.");

				return new ResponseDto
				{
					IsSuccess = false,
					Message = "An unexpected error occurred during registration. Please try again later."
				};
			}
		}

		public async Task<LoginResponseDto> Login(LoginRequestDto requestDto)
		{
			if (requestDto is null || (!IsValidEmail(requestDto.Email)))
			{
				_logger.LogWarning("Login request is null");
				return new LoginResponseDto()
				{
					User = null,
					Token = ""
				};
			}

			var user = await _userManager.FindByEmailAsync(requestDto.Email);

			if (user is null || await _userManager.CheckPasswordAsync(user, requestDto.Password) is false)
			{
				_logger.LogWarning("Invalid login attempt for email: {Email}", requestDto.Email);
				return new LoginResponseDto()
				{
					User = null,
					Token = ""
				};
			}

			UserDto userDto = new UserDto()
			{
				Email = user.Email,
				Name = user.Name,
				PhoneNumber = user.PhoneNumber
			};

			var token = await _jWTTokenGenerator.GenerateTokenAsync(user, _userManager);

			LoginResponseDto loginResponseDto = new LoginResponseDto()
			{
				User = userDto,
				Token = token
			};

			return loginResponseDto;
		}


		private bool IsValidEmail(string email)
		{
			if (string.IsNullOrEmpty(email))
				return false;

			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> AssignRole(string email, string roleName)
		{
			var user = await _userManager.FindByEmailAsync(email);

			bool isRoleExist = await _roleManager.RoleExistsAsync(roleName);

			if (user is not null)
			{
				if(isRoleExist is false)
					await _roleManager.CreateAsync(new IdentityRole(roleName));

				await _userManager.AddToRoleAsync(user, roleName);
				return true;
			}

			return false;
		}
	}
}
