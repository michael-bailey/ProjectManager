using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shared.Register;

/**
 * Input model for user registration requests 
 */
public class NewUserRegistrationInput
{

	[Required(ErrorMessage = "Username is required")]
	public string Username { get; set; }
	
	[Required(ErrorMessage = "Username is required.")]
	[EmailAddress(ErrorMessage = "Invalid email address.")]
	public string Email { get; set; }
	
	[Required(ErrorMessage = "Password is required.")]
	[DataType(DataType.Password)]
	[StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 6 and 100 characters.")]
	public string Password { get; set; }
	
	[Required(ErrorMessage = "Required to enter password again.")]
	[DataType(DataType.Password)]
	[Compare("Password", ErrorMessage = "Passwords do not match.")]
	public string ConfirmPassword { get; set; }
	
	[Required(ErrorMessage = "First name is required.")]
	public string FirstName { get; set; }
	
	[Required(ErrorMessage = "Last name is required.")]
	public string LastName { get; set; }
	
	public string? MiddleNames { get; set; }
	
	
	
}
