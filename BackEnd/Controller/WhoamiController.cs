using BackEnd.Attribute;
using BackEnd.Controller.Type;
using EntityLib;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controller;

[Project(Name = "Whoami")]
[ApiController]
[Route("/api/[controller]")]
public class WhoamiController(
	UserManager<UserEntity> userManager,
	SignInManager<UserEntity> signInManager,
	ILogger<RegisterController> logger) : PublicController
{
	
	[HttpGet]
	public async Task<ActionResult<string>> Whoami()
	{
		if (!signInManager.IsSignedIn(User))
			return NotFound("No one signed in");
		
		var user = await userManager.GetUserAsync(User);

		if (user is null)
			return NotFound("User wasn't found");
		
		
		return user.UserName!;
	}
	

	
	
}