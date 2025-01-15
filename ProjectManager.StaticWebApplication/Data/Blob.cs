using System.ComponentModel.DataAnnotations;

namespace ProjectManager.StaticWebApplication.Data;

public class Blob
{
	public Guid Id { get; init; }

	[DataType("varbinary(max)")]
	public required byte[] Content { get; init; }
}