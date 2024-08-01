using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NanoNet.Services.AuthAPI.Models;

namespace NanoNet.Services.AuthAPI.Data
{
    public class IdentityContext: IdentityDbContext<AppUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options): base(options) { }
    }
}
