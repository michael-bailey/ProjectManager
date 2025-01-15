namespace ProjectManager.StaticWebApplication.Models;

public class PrintableModel
{
	public required Guid Id { get; init; }
	public required string Name { get; set; }
	public required string Description { get; set; }
}