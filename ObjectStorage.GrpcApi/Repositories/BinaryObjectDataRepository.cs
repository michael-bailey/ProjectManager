using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using ObjectStorage.GrpcApi.Data;

namespace ObjectStorage.GrpcApi.Repositories;

public class BinaryObjectDataRepository(
	ObjectStorageDatabaseContext context,
	ILogger<BinaryObjectDataRepository> logger)
	: IObjectDataRepository
{
	
	/// <summary>
	///	Saves object data to database.
	/// </summary>
	/// <param name="requestContent">Content to be stored.</param>
	/// <exception cref="DbUpdateException">Error with saving data to the database</exception>
	/// <exception cref="DbUpdateConcurrencyException">Error saving data, due to a concurrency, like a race condition.</exception>
	/// <returns>ID of the object.</returns>
	public async Task<Guid> SaveData(byte[] requestContent)
	{
		
		logger.LogInformation("Creating Data object");
		
		var data = new BinaryObjectData
		{
			Content = requestContent
		};

		logger.LogInformation("Saving object to context");
		
		await context.AddAsync(data);
		await context.SaveChangesAsync();

		logger.LogInformation("Returning object id");
		
		return data.Id;
	}
	
	/// <summary>
	/// Gets data from the database for id.
	/// </summary>
	/// <param name="id">The id of the data</param>
	/// <returns>The BinaryObjectData, or null if no data found for Id.</returns>
	public async Task<byte[]?> GetDataAsync(Guid id)
	{
		logger.LogInformation("Retrieving data from {Id}", id);

		var data = await context.BinaryObjectDataSet
			.FirstOrDefaultAsync(obj => obj.Id == id);

		if (data is null)
			logger.LogInformation("No data for {Id}", id);
		
		logger.LogInformation("Returning data");
		
		return data?.Content;
	}
}