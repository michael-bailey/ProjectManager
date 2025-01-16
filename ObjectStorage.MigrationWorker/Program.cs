using ObjectStorage.GrpcApi.Data;
using ObjectStorage.MigrationWorker;
using ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.AddServiceDefaults();

builder.AddSqlServerDbContext<ObjectStorageDatabaseContext>("object-database");

builder.Services.AddOpenTelemetry()
	.WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

var host = builder.Build();
host.Run();