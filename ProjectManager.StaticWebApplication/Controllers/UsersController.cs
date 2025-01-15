using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.StaticWebApplication.Models;

namespace ProjectManager.StaticWebApplication.Controllers;

[Authorize]
public class UsersController(
	UserManager<IdentityUser> userManager) : Controller
{
	
	public async Task<IActionResult> Index()
	{
		string[] usernames = (from user in userManager.Users select user.UserName).ToArray();
		
		return View(new UsersModel(usernames));
	}
}