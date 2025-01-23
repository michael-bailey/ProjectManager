using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObjectStorage.GrpcLib;

namespace ObjectStorage.WWW.Pages;

public class DownloadModel(
	ObjectStorage.GrpcLib.ObjectStorage.ObjectStorageClient client,
	ILogger<DownloadModel> logger) : PageModel
{
	
	public async Task<IActionResult> OnGet(string id)
	{
		logger.LogInformation("Attempting to download file");
		
		logger.LogInformation("Calling Grpc service");
		var response = await client.DownloadObjectAsync(new DownloadRequest()
		{
			Id = id
		});
		
		switch (response.ResultCase)
		{
			case DownloadResult.ResultOneofCase.None:
				logger.LogError("Response was of type None");
				break;
			case DownloadResult.ResultOneofCase.Content:
				logger.LogInformation("Got contents back");
				var contents = response.Content.Content.ToByteArray()!;
				return File(contents, "application/octet-stream", "data");
			case DownloadResult.ResultOneofCase.Error:
				logger.LogError("Got error response {Error}", response.Error);
				break;
		}

		return NotFound();
	}
}