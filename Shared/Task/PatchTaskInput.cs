using System.ComponentModel.DataAnnotations;

namespace Shared.Task;

public class PatchTaskInput
{

	[Length(1, 256)]
	public string? Title { get; init; } = null;

	[MaxLength(1024)] 
	public string? Description { get; init; } = null;

	[EnumDataType(typeof(TaskStatus))]
	public TaskStatus? Status { get; init; } = null;

	public DateOnly? DueDate { get; init; } = null;
	
	public Guid? OwnerId { get; init; } = null;

}