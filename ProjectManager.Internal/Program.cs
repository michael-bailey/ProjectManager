using ProjectManager.LibEntity;
using ProjectManager.LibEntity.Context;

var builder = WebApplication.CreateBuilder(args);

builder.AddSqlServerDbContext<DatabaseContext>("database");

builder.Services
	.AddDefaultIdentity<UserEntity>(options =>
	{
		options.SignIn.RequireConfirmedAccount = false;
		options.Password.RequireDigit = false;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequiredLength = 16;
	})
	.AddEntityFrameworkStores<DatabaseContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();

app.MapControllerRoute(
	name : "areas",
	pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages()
	.WithStaticAssets();

app.Run();