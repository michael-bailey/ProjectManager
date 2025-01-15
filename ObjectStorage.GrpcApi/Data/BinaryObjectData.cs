namespace ObjectStorage.GrpcApi.Data;

/**
 * Stores binary data in the database
 */
public class BinaryObjectData
{
	public required string Name { get; init; }
	public required byte[] Content { get; init; }
}