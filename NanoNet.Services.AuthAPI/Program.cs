using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NanoNet.Services.AuthAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Identity Context
builder.Services.AddDbContext<IdentityContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
});

// We need to register three services of identity (UserManager - RoleManager - SignInManager)
// but we don't need to register all them one by one
// because we have method (AddIdentity) that will register the three services
// --- this method has another overload take action to if you need to configure any option of identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
{
	option.Password.RequireLowercase = true;
	option.Password.RequireUppercase = false;
	option.Password.RequireDigit = false;
	option.Password.RequireNonAlphanumeric = true;
	option.Password.RequiredUniqueChars = 3;
	option.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<IdentityContext>();
// ? this because the three services talking to another Store Services
// such as (UserManager talk to IUserStore to take all services like createAsync)
// so we allowed dependency injection to this services too

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
