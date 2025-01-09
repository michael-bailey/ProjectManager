using System.ComponentModel.DataAnnotations;

namespace Shared.SignIn;


public class UserSignInInput
{
	
	[Required(ErrorMessage = "Username is required")]
	public required string Username { get; set; }
	
	[Required(ErrorMessage = "Password is required.")]
	[DataType(DataType.Password)]
	[StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 6 and 100 characters.")]
	public required string Password { get; set; }
	
}