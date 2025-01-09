using Microsoft.AspNetCore.Identity;

namespace EntityLib;

// Add profile data for application users by adding properties to the ProjectManagerUser class
public class UserEntity : IdentityUser<Guid>
{
	
	[ProtectedPersonalData]
	public required string FirstName { get; set; }
	
	[ProtectedPersonalData]
	public required string LastName { get; set; }
	
	[ProtectedPersonalData]
	public required string MiddleNames { get; set; }

	public required List<TaskEntity> Tasks { get; set; }
}

