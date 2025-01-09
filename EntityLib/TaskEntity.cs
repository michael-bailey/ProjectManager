using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shared.Task;
using TaskStatus = Shared.Task.TaskStatus;

namespace EntityLib;

/**
 * # Task Entity
 *
 * Defines the table storage for tasks.
 *
 * utilises a generated int id for ease of use, whilst being backed by a [Guid]
 */
[Table("Task")]
[Index(nameof(SimpleId), IsUnique = true)]
public class TaskEntity : IOwned<UserEntity>
{
	
	// Fields
	public Guid Id { get; init; }
	
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int SimpleId { get; init; } 
	
	[Column(TypeName = "varchar(256)")]
	public required string Title { get; set; }
	
	[Column(TypeName = "varchar(1024)")]
	public required string Description { get; set; }
	
	public required DateTime CreateTime { get; init; }
	public required DateTime UpdateTime { get; set; }
	public required DateOnly? DueDate { get; set; }
	public required TaskStatus Status { get; set; }
	
	public required Guid OwnerId { get; set; }
	
	// Edges
	
	public UserEntity Owner { get; set; } = null!;
	
	public static TaskEntity NewEntity(Guid userId, NewTaskInput inputs) 
	{
		return new TaskEntity
		{
			Title = inputs.Title,
			Description = inputs.Description,

			CreateTime = DateTime.Now.ToUniversalTime(),
			UpdateTime = DateTime.Now.ToUniversalTime(),
			DueDate = inputs.DueDate,

			Status = inputs.Status,
			
			OwnerId = userId
		};
	}

	public TaskData ToData()
	{
		var data = new TaskData
		{
			Id = Id,
			SimpleId = SimpleId,
			Title = Title,
			Description = Description,
			CreateTime = CreateTime,
			UpdateTime = UpdateTime,
			DueDate = DueDate,
			Status = Status
		};

		return data;
	}

	
}