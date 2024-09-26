using Microsoft.EntityFrameworkCore;
using NanoNet.Services.EmailAPI.Models;

namespace NanoNet.Services.EmailAPI.Data;
public class EmailDbContext(DbContextOptions<EmailDbContext> options) : DbContext(options)
{
    public DbSet<EmailLogger> EmailLoggers { get; set; }
}