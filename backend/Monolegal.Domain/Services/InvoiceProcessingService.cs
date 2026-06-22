using Monolegal.Domain.Entities;
using Monolegal.Domain.Repositories;

namespace Monolegal.Domain.Services;

public class InvoiceProcessingService
{
    private readonly IInvoiceRepository _repository;
    private readonly IEmailService _emailService;

    public InvoiceProcessingService(IInvoiceRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public async Task ProcessPendingInvoicesAsync()
    {
        var pendingInvoices = await _repository.GetPendingRemindersAsync();

        foreach (var invoice in pendingInvoices)
        {
            if (string.IsNullOrEmpty(invoice.Id)) continue;

            string nextStatus = invoice.Status switch
            {
                "primerrecordatorio" => "segundorecordatorio",
                "segundorecordatorio" => "desactivado",
                _ => invoice.Status
            };

            if (nextStatus != invoice.Status)
            {
                try
                {
                    await _emailService.SendReminderAsync(invoice.ClientEmail, invoice.ClientName, nextStatus);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ADVERTENCIA SMTP] No se pudo enviar el mail a {invoice.ClientName}. Razón: {ex.Message}");
                }

                await _repository.UpdateStatusAsync(invoice.Id, nextStatus);

                await Task.Delay(2000); 
            }
        }
    }
}