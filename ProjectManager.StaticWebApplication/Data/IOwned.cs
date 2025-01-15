using Microsoft.AspNetCore.Identity;

namespace ProjectManager.StaticWebApplication.Data;

public interface IOwned
{
	public Guid OwnerId { get; init; }
}