using Monolegal.Domain.Repositories;
using Monolegal.Domain.Services;
using Monolegal.Infrastructure.Repositories;
using Monolegal.Infrastructure.Services;
using Monolegal.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));

builder.Services.AddScoped<IInvoiceRepository, MongoInvoiceRepository>();

builder.Services.AddScoped<IEmailService, BrevoEmailService>();

builder.Services.AddScoped<InvoiceProcessingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/invoices", async (IInvoiceRepository repository) =>
{
    var invoices = await repository.GetAllInvoicesAsync();
    return Results.Ok(invoices);
})
.WithName("GetInvoices")
.WithOpenApi();

app.MapPost("/api/invoices/process-reminders", async (InvoiceProcessingService processingService) =>
{
    try
    {
        var results = await processingService.ProcessPendingInvoicesAsync();

        bool hasFailures = results.Any(r => r.Contains("[FALLO]"));

        if (hasFailures)
        {
            return Results.BadRequest(new
            {
                message = "El proceso finalizó con algunas advertencias.",
                details = results
            });
        }

        return Results.Ok(new
        {
            message = "Vigilancia completada exitosamente.",
            details = results
        });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error crítico de sistema: {ex.Message}");
    }
})
.WithName("ProcessReminders")
.WithOpenApi();

app.Run();