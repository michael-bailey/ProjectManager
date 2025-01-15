using System.ComponentModel.DataAnnotations;

namespace ProjectManager.StaticWebApplication.Models.Forms;

public class CreatePrintableFormData
{
	[Microsoft.Build.Framework.Required]
	[Length(1, 64)]
	public required string Name { get; init; }
	
	[MaxLength(1024)]
	public required string Description { get; init; }
}