using BackEnd.Controller.Type;
using EntityLib;
using EntityLib.Authentication;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.SignIn;

namespace BackEnd.Controller;


[Route("/api/signin")]
public class SignInController(
	SignInManager<UserEntity> signInManager,
	UserManager<UserEntity> userManager,
	RoleManager<UserRole> roleManager) : PublicController
{

	[HttpPost]
	public async Task<IActionResult> SignInUser(UserSignInInput input)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

		var user = await userManager.FindByNameAsync(input.Username);
		
		if (user is null)
			return NotFound("user with user name is not found");

		if (!await userManager.CheckPasswordAsync(user, input.Password))
			return Unauthorized("Password was incorrect");

		await signInManager.SignInAsync(user, true);
		return Ok();
	}

	[HttpDelete]
	public async Task<IActionResult> SignOutuser()
	{
		await signInManager.SignOutAsync();
		return Ok();
	}
}