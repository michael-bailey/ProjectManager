using ProjectManager.StaticWebApplication.Data;
using ServiceDefaults;
using StaticWorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
	.WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddSqlServerDbContext<ApplicationDbContext>("static-database");
builder.EnrichSqlServerDbContext<ApplicationDbContext>(settings =>
{
	settings.DisableHealthChecks = false;
});

var host = builder.Build();
host.Run();