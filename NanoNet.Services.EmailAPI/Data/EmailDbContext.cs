using Microsoft.EntityFrameworkCore;
using NanoNet.Services.EmailAPI.Models;

namespace NanoNet.Services.EmailAPI.Data;
public class EmailDbContext: DbContext
{
    public EmailDbContext(DbContextOptions<EmailDbContext> options): base(options)
    {
        
    }
    public DbSet<EmailLogger> EmailLoggers { get; set; }
}