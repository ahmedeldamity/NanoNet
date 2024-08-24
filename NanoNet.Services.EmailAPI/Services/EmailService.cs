using Microsoft.EntityFrameworkCore;
using NanoNet.Services.EmailAPI.Data;
using NanoNet.Services.EmailAPI.Dtos;
using NanoNet.Services.EmailAPI.Interfaces;
using NanoNet.Services.EmailAPI.Models;
using System.Text;

namespace NanoNet.Services.EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        private DbContextOptions<EmailDbContext> _dbOptions;

        public EmailService(DbContextOptions<EmailDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public async Task EmailCartAndLog(CartDto cartDto)
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("<br/>Cart Emial Requested ");
            message.AppendLine("<br/>Total " + cartDto.CartHeader.TotalPrice);
            message.Append("<br/>");
            message.Append("<ul>");
            foreach (var item in cartDto.CartItems)
            {
                message.Append("<li>");
                message.Append(item.Product!.Name + " - " + item.Count);
                message.Append("</li>");
            }
            message.Append("</ul>");

            await LogEmail(message.ToString(), cartDto.CartHeader.Email!);
        }

        private async Task<bool> LogEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLogger = new EmailLogger()
                {
                    Email = email,
                    Message = message,
                    EmailMessage = DateTime.Now
                };

                await using var _db = new EmailDbContext(_dbOptions);
                await _db.EmailLoggers.AddAsync(emailLogger);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
