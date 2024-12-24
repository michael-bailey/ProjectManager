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
public class TaskEntity
{
	public Guid Id { get; init; }
	
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int SimpleId { get; init; } 
	
	[Required]
	[Column(TypeName = "varchar(256)")]
	public String Title { get; set; }
	
	[Column(TypeName = "varchar(1024)")]
	public String Description { get; set; }
	
	public DateTime CreateTime { get; set; }
	
	public DateTime UpdateTime { get; set; }
	
	public DateOnly? DueDate { get; set; }
	
	public TaskStatus Status { get; set; }
	
	/**
	 * Sets required fields 
	 */
	public static TaskEntity NewEntity(
		String title,
		String description = "",
		DateOnly? dueDate = null
	) {
		
		return new TaskEntity
		{
			Title = title,
			Description = description,
		
			CreateTime = DateTime.Now.ToUniversalTime(),
			UpdateTime = DateTime.Now.ToUniversalTime(),
			DueDate = dueDate,
		
			Status = TaskStatus.None
		};
	}

	public TaskData ToData()
	{
		var data = new TaskData
		{
			Id = this.Id,
			SimpleId = this.SimpleId,
			Title = this.Title,
			Description = this.Description,
			CreateTime = this.CreateTime,
			UpdateTime = this.UpdateTime,
			DueDate = this.DueDate,
			Status = this.Status
		};

		return data;
	}
}