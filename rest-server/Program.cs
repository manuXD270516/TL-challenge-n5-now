using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Application;
using Shared;
using Shared.config;
using rest_server.Middlewares;
using rest_server.Extensions;
using Intregation;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
// Add services to the container.
//builder.Services.Configure<AppSettings>(config.GetSection(string.Empty));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddScoped<ApiLoggerMiddleware>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceInfrastructure(config);
builder.Services.AddSharedInfrastructure(config);
builder.Services.AddIntegrations(config);

builder.Services.AddApiVersioning();
builder.Services.AddRestApiServices();

var app = builder.Build();
//var config = app.Configuration;


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseErrorHandlingMiddleware();
app.UseApiLoggerMiddleware();
app.UseKafkaIntegrationMiddleware();

//app.UseSerilog2(config);
//app.UseSerilog(builder.Services);

app.Run();

public partial class Program { }