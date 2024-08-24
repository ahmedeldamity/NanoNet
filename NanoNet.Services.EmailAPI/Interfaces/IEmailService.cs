using NanoNet.Services.EmailAPI.Dtos;

namespace NanoNet.Services.EmailAPI.Interfaces;
public interface IEmailService
{
    Task EmailCartAndLog(CartDto cartDto);
}