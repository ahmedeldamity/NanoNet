﻿using NanoNet.Web.Interfaces.IService;
using NanoNet.Web.Utility;
using NanoNet.Web.ViewModels;

namespace NanoNet.Web.Services
{
    public class AuthService(IBaseService _baseService) : IAuthService
    {
        public async Task<ResponseViewModel?> AssignRoleAsync(LoginRequestViewModel model)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.GET,
                Data = model,
                Url = SD.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseViewModel?> LoginAsync(LoginRequestViewModel model)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.GET,
                Data = model,
                Url = SD.AuthAPIBase + "/api/auth/login"
            });
        }

        public async Task<ResponseViewModel?> RegisterAsync(LoginRequestViewModel model)
        {
            return await _baseService.SendAsync(new RequestViewModel()
            {
                ApiType = SD.ApiType.GET,
                Data = model,
                Url = SD.AuthAPIBase + "/api/auth/register"
            });
        }
    }
}