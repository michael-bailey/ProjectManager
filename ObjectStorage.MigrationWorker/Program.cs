using ObjectStorage.GrpcApi.Data;
using ObjectStorage.MigrationWorker;
using ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
	.WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddSqlServerDbContext<ObjectStorageDatabaseContext>("object-database");

var host = builder.Build();
host.Run();