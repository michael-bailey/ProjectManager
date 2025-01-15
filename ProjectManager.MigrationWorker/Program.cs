using ProjectManager.LibEntity.Context;
using ProjectManager.MigrationWorker;
using ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
	.WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddSqlServerDbContext<DatabaseContext>("database");

var host = builder.Build();
host.Run();