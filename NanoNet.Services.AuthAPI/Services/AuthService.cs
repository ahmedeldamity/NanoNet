using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using NanoNet.Services.AuthAPI.Dtos;
using NanoNet.Services.AuthAPI.ErrorHandling;
using NanoNet.Services.AuthAPI.Interfaces.IService;
using NanoNet.Services.AuthAPI.Models;

namespace NanoNet.Services.AuthAPI.Services;
public class AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
ILogger<AuthService> logger, IJWTTokenGenerator jWtTokenGenerator) : IAuthService
{
    public async Task<Result<UserDto>> Register(RegistrationRequestDto requestDto)
    {
        if (requestDto is null || IsValidEmail(requestDto.Email) is false) 
			return Result.Failure<UserDto>("Invalid email address");

		var userExist = await userManager.FindByEmailAsync(requestDto.Email);

		if (userExist is not null)
			return Result.Failure<UserDto>("User already exists");

		var user = new AppUser
		{
			UserName = requestDto.Email,
			Email = requestDto.Email,
			NormalizedEmail = requestDto.Email.ToUpper(),
			Name = requestDto.Name,
			PhoneNumber = requestDto.PhoneNumber,
		};

		var result = await userManager.CreateAsync(user, requestDto.Password);

		if (result.Succeeded)
		{
			logger.LogInformation($"User {requestDto.Email} registered successfully.");

			var userToReturn = new UserDto
			{
				Email = requestDto.Email,
				Name = requestDto.Name,
				PhoneNumber = requestDto.PhoneNumber
			};

			return Result.Success(userToReturn);
		}

		var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));

		logger.LogWarning($"Registration failed for user {requestDto.Email}: {errorMessage}");

		return Result.Failure<UserDto>("Registration failed");
	}

	public async Task<Result<LoginResponseDto>> Login(LoginRequestDto requestDto)
	{
		if (requestDto is null || IsValidEmail(requestDto.Email) is false)
			return Result.Failure<LoginResponseDto>("Invalid email address");

		var user = await userManager.FindByEmailAsync(requestDto.Email);

		if (user is null || await userManager.CheckPasswordAsync(user, requestDto.Password) is false)
			return Result.Failure<LoginResponseDto>("Invalid email or password");

		var userDto = new UserDto
		{
			Email = user.Email!,
			Name = user.Name,
			PhoneNumber = user.PhoneNumber!
		};

		var token = await jWtTokenGenerator.GenerateTokenAsync(user, userManager);

		var loginResponseDto = new LoginResponseDto
		{
			User = userDto,
			Token = token
		};

		return Result.Success(loginResponseDto);
	}

	public async Task<Result> AssignRole(string email, string roleName)
	{
		var user = await userManager.FindByEmailAsync(email);

		var isRoleExist = await roleManager.RoleExistsAsync(roleName);

        if (user is null)
			return Result.Failure("User does not exist");

        if(isRoleExist is false)
            await roleManager.CreateAsync(new IdentityRole(roleName));

        await userManager.AddToRoleAsync(user, roleName);

		return Result.Success("Role assigned successfully");
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        try
        {
            var emailAddress = new MailAddress(email);
            return emailAddress.Address == email;
        }
        catch
        {
            return false;
        }
    }
}