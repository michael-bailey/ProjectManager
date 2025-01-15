using Microsoft.AspNetCore.Identity;

namespace ProjectManager.StaticWebApplication.Data;

public class Printable : IOwned
{
	
	public Guid Id { get; init; }
	
	public required string Name { get; set; }
	public required string Description { get; set; }
	
	public required Guid BlobId { get; set; }
	public Blob Blob { get; set; }
	
	public required Guid OwnerId { get; init; }
}