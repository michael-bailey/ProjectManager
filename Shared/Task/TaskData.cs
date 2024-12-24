using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Shared.Task;

[DataContract]
public class TaskData
{
	
	[DataMember]
	public Guid Id { get; set; }
	
	[DataMember]
	public int SimpleId { get; set; }
	
	[DataMember]
	public string Title { get; set; }
	
	[DataMember]
	public string Description { get; set; }

	[DataMember]
	public DateTime CreateTime { get; set; }
	
	[DataMember]
	public DateTime UpdateTime { get; set; }
	
	[DataMember]
	public DateOnly? DueDate { get; set; }

	[DataMember]
	public TaskStatus Status { get; set; }
	
	public string TaskStatusString
	{
		get
		{
			return Status switch
			{
				TaskStatus.None => "String",
				TaskStatus.Planned => "Planned",
				TaskStatus.InProgress => "In Progress",
				TaskStatus.Closed => "Closed",
				_ => "UNKNOWN"
			};
		}
	}
	
	public Uri TaskUri => new($"/task/T{SimpleId}");

	[JsonConstructor]
	public TaskData()
	{ }

	public TaskData(
		Guid id,
		int simpleId,
		string title,
		string description,
		DateTime createTime,
		DateTime updateTime,
		DateOnly dueDate,
		TaskStatus status) : this()
	{
		this.Id = id;
		this.SimpleId = simpleId;
		this.Title = title;
		this.Description = description;
		this.CreateTime = createTime;
		this.UpdateTime = updateTime;
		this.DueDate = dueDate;
		this.Status = status;
	}
}