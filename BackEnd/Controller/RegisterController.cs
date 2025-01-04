using Azure;
using BackEnd.Controller.Type;
using EntityLib;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Register;

namespace BackEnd.Controller;

[ApiController]
[Route("/api/[controller]")]
public class RegisterController(
	UserManager<UserEntity> userManager,
	SignInManager<UserEntity> signInManager,
	ILogger<RegisterController> logger) : PublicController
{
	
	[HttpPost]
	public async Task<IActionResult> Register(
		NewUserRegistrationInput model)
	{
		
		var user = await userManager.GetUserAsync(User);
		
		if (user != null)
		{
			logger.LogInformation($"Attept to register user when already logged in");
			return BadRequest("User already Logged In");
		}

		if (!ModelState.IsValid)
		{
			logger.LogInformation("Registration Failed, Sending BadRequest");
			return BadRequest(ModelState.ValidationState);
		}
		
		logger.LogInformation("Creating New User");
		
		user = new UserEntity
		{
			UserName = model.Username,
			Email = model.Email,
			FirstName = model.FirstName,
			LastName = model.LastName,
			// TODO: Set the middle name to be nullable
			MiddleNames = model.MiddleNames ?? ""
		};
		
		var result = await userManager.CreateAsync(user, model.Password);

		if (!result.Succeeded)
		{
			logger.LogInformation("User Creation failed, Sending BadRequest");
			return BadRequest(result.Errors);
		}
		
		logger.LogInformation("User Creation Successful");
		
		await signInManager.SignInAsync(user, true);

		return Ok(true);
	}
}