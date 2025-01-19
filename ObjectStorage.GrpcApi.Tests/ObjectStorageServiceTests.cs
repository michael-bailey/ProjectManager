// ReSharper disable UnusedType.Global

using Google.Protobuf;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Moq;
using ObjectStorage.GrpcApi.Repositories;
using ObjectStorage.GrpcApi.Services;
using ObjectStorage.GrpcLib;


namespace ObjectStorage.GrpcApi.Tests;

public class ObjectStorageServiceTests
{
	private static readonly Guid DataGuid = Guid.NewGuid();
	private ILogger<ObjectStorageService> _logger;

	[SetUp]
	public void SetUp()
	{
		using var loggerFactory = LoggerFactory.Create(loggingBuilder =>
			loggingBuilder
				.SetMinimumLevel(LogLevel.Trace)
				.AddConsole());

		_logger = loggerFactory.CreateLogger<ObjectStorageService>();
	}

	// no need for larger 
	internal static object[][] TestUploadingDataData =
	[
		[new byte[1], UploadResult.ResultOneofCase.Id],
		[Array.Empty<byte>(), UploadResult.ResultOneofCase.Error],
		[new byte[Constants.MAX_UPLOAD_SIZE], UploadResult.ResultOneofCase.Id]
	];

	[TestCaseSource(nameof(TestUploadingDataData))]
	public async Task TestUploadingData(byte[] byteArray,
		UploadResult.ResultOneofCase expected)
	{
		var repositoryMock = new Mock<IObjectDataRepository>();

		repositoryMock.Setup(lib => lib.SaveData(byteArray))
			.Returns(Task.FromResult(DataGuid));

		var repository = repositoryMock.Object;

		var callContext = new Mock<ServerCallContext>();
		callContext.SetupAllProperties();

		var service = new ObjectStorageService(_logger, repository);

		var request = new UploadRequest
		{
			Content = ByteString.CopyFrom(byteArray)
		};

		var res = await service.UploadObject(request, callContext.Object);

		Assert.That(expected, Is.EqualTo(res.ResultCase));
	}

	internal static object?[][] TestDownloadingDataData =
	[
		[
			DataGuid.ToString(), new byte[10], DownloadResult.ResultOneofCase.Content
		],
		[DataGuid.ToString(), null, DownloadResult.ResultOneofCase.Error],
		["", new byte[10], DownloadResult.ResultOneofCase.Error]
	];

	[TestCaseSource(nameof(TestDownloadingDataData))]
	public async Task TestDownloadData(string stringId,
		byte[]? byteArray,
		DownloadResult.ResultOneofCase expected)
	{
		var repositoryMock = new Mock<IObjectDataRepository>();
		repositoryMock.Setup(lib => lib.GetDataAsync(DataGuid))
			.Returns(Task.FromResult(byteArray));
		var repository = repositoryMock.Object;

		var callContext = new Mock<ServerCallContext>();
		callContext.SetupAllProperties();

		var service = new ObjectStorageService(_logger, repository);

		var request = new DownloadRequest
		{
			Id = stringId
		};

		var res = await service.DownloadObject(request, callContext.Object);

		Assert.That(expected, Is.EqualTo(res.ResultCase));
	}
}