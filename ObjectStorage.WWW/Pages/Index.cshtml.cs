using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObjectStorage.GrpcLib;

namespace ObjectStorage.WWW.Pages;

public class IndexModel(
	GrpcLib.ObjectStorage.ObjectStorageClient client,
	ILogger<IndexModel> logger)
	: PageModel
{

	public Guid? DownloadId;
	
	public string? UploadError;
	
	[BindProperty]
	public IFormFile Upload { get; set; } = null!;

	public async Task OnPost()
	{
		logger.LogInformation("Got new file upload");

		if (Upload.Length > int.MaxValue)
		{
			UploadError = "File is too large for uploading";
			return;
		}
		
		logger.LogInformation("opening reader stream");
		await using var reader = Upload.OpenReadStream();
		
		logger.LogInformation("Calling Grpc service");
		var response = await client.UploadObjectAsync(new UploadRequest
		{
			Content = await ByteString.FromStreamAsync(reader)
		});

		if (response is null)
		{
			logger.LogError("Response was null");
			UploadError = "Got an invalid response back, please try again";
			return;
		}
		
		switch (response.ResultCase)
		{
			case UploadResult.ResultOneofCase.None:
				logger.LogError("Response was of type None");
				UploadError = "Some how got None response, please try again";
				break;
			case UploadResult.ResultOneofCase.Id:
				logger.LogError("Got Id back");
				DownloadId = Guid.Parse(response.Id);
				break;
			case UploadResult.ResultOneofCase.Error:
				logger.LogError("Got error response {Error}", response.Error);
				UploadError = $"got error: {response.Error}";
				break;
		}
	}
}