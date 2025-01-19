using Google.Protobuf;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ObjectStorage.GrpcApi.Data;
using ObjectStorage.GrpcApi.Repositories;
using ObjectStorage.GrpcLib;
using Object = ObjectStorage.GrpcLib.Object;

namespace ObjectStorage.GrpcApi.Services;

public class ObjectStorageService(
	ILogger<ObjectStorageService> logger,
	IObjectDataRepository repository)
	: GrpcLib.ObjectStorage.ObjectStorageBase
{
	public override async Task<UploadResult> UploadObject(
		UploadRequest request,
		ServerCallContext context)
	{
		logger.LogInformation(
			"got upload request from {Peer} Started object upload", context.Peer);

		logger.LogInformation("Creating object");


		var data = request.Content.ToByteArray();

		logger.LogInformation("Checking content length");
		if (data.Length < 1)
			return new UploadResult
			{
				Error = "Zero length content, is not allowed"
			};
		
		Guid id;
		
		try
		{
			logger.LogInformation("Attempting save");
			id = await repository.SaveData(data);
		}
		catch (Exception e)
		{
			logger.LogError(e, "error saving object");
			return new UploadResult
			{
				Error = e.Message
			};
		}

		return new UploadResult
		{
			Id = id.ToString(),
		};
	}

	public override async Task<DownloadResult> DownloadObject(
		DownloadRequest request, ServerCallContext context)
	{
		try
		{
			logger.LogInformation(
				"got download request from {Peer}, for {Id}", context.Peer, request.Id);

			logger.LogInformation("Fetching object");

			var id = Guid.Parse(request.Id);

			var data = await repository.GetDataAsync(id);

			if (data is null)
				return new DownloadResult
				{
					Error = "Data is not found"
				};

			return new DownloadResult
			{
				Content = new Object
				{
					Content = ByteString.CopyFrom(data),
					Id = id.ToString()
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