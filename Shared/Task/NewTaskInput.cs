using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Shared.Task;


public class NewTaskInput {
	
	[Required, Length(1, 256)]
	public required string Title { get; init; }

	[Required, MaxLength(1024)] public string Description { get; init; } = "";

	[Required, EnumDataType(typeof(TaskStatus))]
	public TaskStatus Status { get; init; } = TaskStatus.None;

	public DateOnly? DueDate { get; init; } = null;
	
};
