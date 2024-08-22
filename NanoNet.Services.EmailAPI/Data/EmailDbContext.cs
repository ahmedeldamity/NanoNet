using Microsoft.EntityFrameworkCore;

namespace NanoNet.Services.EmailAPI.Data
{
    public class EmailDbContext: DbContext
    {
        public EmailDbContext(DbContextOptions<EmailDbContext> options): base(options)
        {
            
        }
    }
}
