using System.Configuration;
using ProjectManager.LibEntity;
using ProjectManager.LibEntity.Authentication;
using ProjectManager.LibEntity.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ServiceDefaults;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddAuthentication();

builder.AddServiceDefaults();

if (builder.Environment.IsDevelopment())
{
	builder.Services.AddDbContext<DatabaseContext>(opts =>
	{
		opts.UseSqlite(builder.Configuration.GetConnectionString("SqliteMemory"));
	});
}
else
{
	builder.AddSqlServerDbContext<DatabaseContext>("");
}


builder.Services.AddIdentity<UserEntity, UserRole>(options =>
{
	// options.SignIn.RequireConfirmedEmail = true;
	options.User.RequireUniqueEmail = true;
	options.Password.RequiredLength = 8;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireDigit = false;
}).AddEntityFrameworkStores<DatabaseContext>();

builder.Services
	.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddBearerToken(
		opts =>
		{
			opts.BearerTokenExpiration = TimeSpan.FromDays(7);
		});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();