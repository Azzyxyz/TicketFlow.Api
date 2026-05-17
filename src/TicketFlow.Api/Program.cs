using System.Text.Json.Serialization;
using TicketFlow.Api.Application.Notifications;
using TicketFlow.Api.Application.Reports;
using TicketFlow.Api.Application.Repositories;
using TicketFlow.Api.Application.Services;
using TicketFlow.Api.Domain.Interfaces;
using TicketFlow.Api.Domain.Models;
using TicketFlow.Api.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRepository<Ticket>, InMemoryRepository<Ticket>>();
builder.Services.AddSingleton<IRepository<Customer>, InMemoryRepository<Customer>>();
builder.Services.AddSingleton<IRepository<SupportAgent>, InMemoryRepository<SupportAgent>>();

builder.Services.AddSingleton<INotificationChannel, ConsoleNotificationChannel>();
builder.Services.AddSingleton<INotificationChannel, EmailNotificationChannel>();
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddSingleton<ReflectionMetadataService>();
builder.Services.AddSingleton<IReportExporter, TextReportExporter>();
builder.Services.AddSingleton<IReportExporter, CsvReportExporter>();
builder.Services.AddSingleton<ReportService>();

builder.Services.AddSingleton<TicketService>(serviceProvider =>
{
    var service = new TicketService(
        serviceProvider.GetRequiredService<IRepository<Ticket>>(),
        serviceProvider.GetRequiredService<IRepository<Customer>>(),
        serviceProvider.GetRequiredService<IRepository<SupportAgent>>()
    );

    var notificationService = serviceProvider.GetRequiredService<NotificationService>();
    service.TicketChanged += notificationService.HandleTicketChangedAsync;

    return service;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

await DemoDataSeeder.SeedAsync(app.Services);

app.Run();
