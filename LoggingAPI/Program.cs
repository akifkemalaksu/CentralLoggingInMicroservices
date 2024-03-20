using LoggingAPI.Controllers;
using LoggingAPI.OptionModels;
using LoggingAPI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharedModels.Extensions;
using SharedModels.OptionModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<LogDatabaseSettings>(builder.Configuration.GetSection(nameof(LogDatabaseSettings)));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<LogDatabaseSettings>>().Value);

builder.AddMessageBrokerService();

builder.Services.AddKeyedSingleton<ILogService, BloggingAPILogService>(nameof(BloggingController));
builder.Services.AddKeyedSingleton<ILogService, ContentAPILogService>(nameof(ContentController));

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
