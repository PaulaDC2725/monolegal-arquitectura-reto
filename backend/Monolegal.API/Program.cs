using Monolegal.Domain.Repositories;
using Monolegal.Domain.Services;
using Monolegal.Infrastructure.Repositories;
using Monolegal.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IInvoiceRepository, MongoInvoiceRepository>();
builder.Services.AddScoped<IEmailService, MailtrapEmailService>();
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
    await processingService.ProcessPendingInvoicesAsync();
    return Results.Ok(new { message = "Vigilancia ejecutada: Correos enviados y Mongo actualizado correctamente." });
})
.WithName("ProcessReminders")
.WithOpenApi();


app.Run();