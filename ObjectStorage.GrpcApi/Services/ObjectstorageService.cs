using Google.Protobuf;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ObjectStorage.GrpcApi.Data;
using ObjectStorage.GrpcLib;
using Object = ObjectStorage.GrpcLib.Object;

namespace ObjectStorage.GrpcApi.Services;

public class ObjectStorageService(
	ILogger<ObjectStorageService> logger,
	ObjectStorageDatabaseContext dataContext)
	: GrpcLib.ObjectStorage.ObjectStorageBase
{
	public override async Task<UploadResult> UploadObject(UploadRequest request,
		ServerCallContext context)
	{
		try
		{
			logger.LogInformation(
				"got upload request from {Peer} Started object upload", context.Peer);

			logger.LogInformation("Creating object");
			var data = new BinaryObjectData
			{
				Content = request.Content.ToByteArray()
			};

			logger.LogInformation("Saving object to context");
			await dataContext.AddAsync(data);
			await dataContext.SaveChangesAsync();

			return new UploadResult
			{
				Id = data.Id.ToString()
			};
		}
		catch (Exception e)
		{
			return new UploadResult
			{
				Error = e.Message
			};
		}
	}

	public override async Task<DownloadResult> DownloadObject(
		DownloadRequest request, ServerCallContext context)
	{
		try
		{
			logger.LogInformation(
				"got download request from {Peer}, for {Id}", context.Peer, request.Id);

			logger.LogInformation("Fetching object");
			var obj = await dataContext.BinaryObjectDataSet
				.FirstAsync(objectData => objectData.Id == Guid.Parse(request.Id));

			return new DownloadResult
			{
				Content = new Object
				{
					Content = ByteString.CopyFrom(obj.Content),
					Id = obj.Id.ToString()
				}
			};
		}
		catch (FormatException e)
		{
			logger.LogError(e, "{Message}", e.Message);

			return new DownloadResult
			{
				Error = e.Message
			};
		}
		catch (InvalidOperationException e)
		{
			logger.LogError(e, "{Message}", e.Message);

			return new DownloadResult
			{
				Error = e.Message
			};
		}
		catch (Exception e)
		{
			logger.LogError(e, "{Message}", e.Message);

			return new DownloadResult
			{
				Error = e.Message
			};
		}
	}
}