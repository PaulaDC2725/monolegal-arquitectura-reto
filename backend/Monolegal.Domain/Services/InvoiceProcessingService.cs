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

        var invoice = pendingInvoices.FirstOrDefault();

        if (invoice != null)
        {
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
                    await _repository.UpdateStatusAsync(invoice.Id!, nextStatus);
                    Console.WriteLine($"[ÉXITO] Correo enviado a {invoice.ClientEmail}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[FALLO] No se pudo enviar el correo: {ex.Message}");
                }
            }
        }
    }
}