using Microsoft.VisualBasic;
using ObjectStorage.GrpcApi.Data;
using ObjectStorage.GrpcApi.Repositories;
using ObjectStorage.GrpcApi.Services;
using ServiceDefaults;
using Constants = ObjectStorage.GrpcLib.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<ObjectStorageDatabaseContext>("object-database");

// Add services to the container.
builder.Services.AddGrpc(opts =>
{
	opts.MaxReceiveMessageSize = Constants.MAX_UPLOAD_SIZE;
	opts.MaxSendMessageSize = Constants.MAX_UPLOAD_SIZE;
});

builder.AddServiceDefaults();

builder.Services.AddScoped< IObjectDataRepository, BinaryObjectDataRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ObjectStorageService>();
app.MapGet("/",
	() =>
		"Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();