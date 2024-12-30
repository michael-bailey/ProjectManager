using Microsoft.AspNetCore.Identity;

namespace EntityLib;

// Add profile data for application users by adding properties to the ProjectManagerUser class
public class UserEntity : IdentityUser<Guid>
{
	
	[ProtectedPersonalData]
	public string FirstName { get; set; }
	
	[ProtectedPersonalData]
	public string LastName { get; set; }
	
	[ProtectedPersonalData]
	public string MiddleNames { get; set; }
}

