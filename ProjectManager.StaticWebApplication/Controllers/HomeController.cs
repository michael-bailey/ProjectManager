using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.StaticWebApplication.Data;
using ProjectManager.StaticWebApplication.Models;

namespace ProjectManager.StaticWebApplication.Controllers;

public class HomeController(
	ApplicationDbContext context,
	SignInManager<IdentityUser> signInManager,
	UserManager<IdentityUser> userManager): Controller
{
	public async Task<IActionResult> Index()
	{
		var isSignedIn = signInManager.IsSignedIn(User);
		
		return View(new HomeModel(isSignedIn));
	}
}