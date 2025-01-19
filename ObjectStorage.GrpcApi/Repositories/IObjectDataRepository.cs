using Google.Protobuf;
using ObjectStorage.GrpcApi.Data;

namespace ObjectStorage.GrpcApi.Repositories;

public interface IObjectDataRepository
{
	
	/// <summary>
	///	Saves object data to database.
	/// </summary>
	/// <remarks>
	///		<para>
	///		This may throw exceptions, depending on implementation.
	///		</para>
	/// </remarks>
	/// <param name="requestContent">Content to be stored.</param>
	/// <returns>ID of the object.</returns>
	Task<Guid> SaveData(byte[] requestContent);

	/// <summary>
	/// Gets the binary data from stored object.  
	/// </summary>
	/// <remarks>
	///		<para>
	///		This may throw exceptions, depending on implementation.
	///		</para>
	/// </remarks>
	/// <param name="id">The id of the object holding the data</param>
	/// <returns>The byte array of the data stored, or null if non-existent</returns>
	Task<byte[]?> GetDataAsync(Guid id);
}