using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectStorage.GrpcApi.Data;

/**
 * Stores binary data in the database
 */
public class BinaryObjectData
{
	
	public Guid Id { get; init; }
	
	public required byte[] Content { get; init; }
}