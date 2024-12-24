using BackEnd.Hubs;
using EntityLib.Context;
using Microsoft.AspNetCore.ResponseCompression;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddAuthentication();

builder.AddServiceDefaults();

builder.AddSqlServerDbContext<DatabaseContext>(connectionName: "database");

builder.Services.AddSignalR(opts =>
{
    opts.EnableDetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapHub<TaskHub>("/taskhub");

app.Run();