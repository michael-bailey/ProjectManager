using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Shared.Task;

[DataContract]
public class TaskData
{
	
	[DataMember]
	public required Guid Id { get; set; }
	
	[DataMember]
	public required int SimpleId { get; set; }
	
	[DataMember]
	public required string Title { get; set; }
	
	[DataMember]
	public required string Description { get; set; }

	[DataMember]
	public required DateTime CreateTime { get; set; }
	
	[DataMember]
	public required DateTime UpdateTime { get; set; }
	
	[DataMember]
	public DateOnly? DueDate { get; set; }

	[DataMember]
	public required TaskStatus Status { get; set; }
	
	public string TaskStatusString =>
		Status switch
		{
			TaskStatus.None => "String",
			TaskStatus.Planned => "Planned",
			TaskStatus.InProgress => "In Progress",
			TaskStatus.Closed => "Closed",
			_ => "UNKNOWN"
		};

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
		Id = id;
		SimpleId = simpleId;
		Title = title;
		Description = description;
		CreateTime = createTime;
		UpdateTime = updateTime;
		DueDate = dueDate;
		Status = status;
	}
}