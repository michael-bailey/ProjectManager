
using Grpc.Net.Client;
using ObjectStorage.GrpcLib;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddRazorPages();
builder.Services
	.AddGrpcClient<ObjectStorage.GrpcLib.ObjectStorage.ObjectStorageClient>(opts =>
	{
		opts.Address = new Uri("http://object-storage");
		opts.ChannelOptionsActions.Add(o =>
		{
			o.MaxSendMessageSize = Constants.MAX_UPLOAD_SIZE;
			o.MaxReceiveMessageSize = Constants.MAX_UPLOAD_SIZE;
		});
	});

var app = builder.Build();

app.UseExceptionHandler("/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
	.WithStaticAssets();

app.Run();