using Microsoft.AspNetCore.Identity;
using ProjectManager.StaticWebApplication.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
		
builder.AddSqlServerDbContext<ApplicationDbContext>(connectionName: "static-database");

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
		
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
	{
		options.SignIn.RequireConfirmedAccount = false;
	})
	.AddEntityFrameworkStores<ApplicationDbContext>();
		
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();

app.MapRazorPages()
	.WithStaticAssets();

app.Run();