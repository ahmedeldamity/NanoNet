using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NanoNet.Services.AuthAPI.Models;

namespace NanoNet.Services.AuthAPI.Data;
public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<AppUser>(options);