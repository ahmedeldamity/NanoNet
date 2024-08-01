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

		public AuthService(IdentityContext identityContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
			ILogger<AuthService> logger)
        {
			_identityContext = identityContext;
			_userManager = userManager;
			_roleManager = roleManager;
			_logger = logger;
		}

        public Task<LoginResponseDto> Login(LoginRequestDto requestDto)
		{
			throw new NotImplementedException();
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
	}
}
