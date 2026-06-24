using Microsoft.Extensions.Logging;
using Monolegal.Domain.Constants;
using Monolegal.Domain.Entities;
using Monolegal.Domain.Repositories;

namespace Monolegal.Domain.Services;

public class InvoiceProcessingService : IInvoiceProcessingService
{
    private readonly IInvoiceRepository _repository;
    private readonly IEmailService _emailService;
    private readonly ILogger<InvoiceProcessingService> _logger;

    public InvoiceProcessingService(
        IInvoiceRepository repository,
        IEmailService emailService,
        ILogger<InvoiceProcessingService> logger)
    {
        _repository = repository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<List<string>> ProcessPendingInvoicesAsync()
    {
        _logger.LogInformation("Iniciando el procesamiento automático de recordatorios procesales.");
        var results = new List<string>();

        var pendingInvoices = await _repository.GetPendingRemindersAsync();

        if (pendingInvoices == null || pendingInvoices.Count == 0)
        {
            _logger.LogInformation("No se encontraron facturas pendientes para notificar.");
            results.Add("No hay expedientes pendientes de notificación.");
            return results;
        }

        foreach (var invoice in pendingInvoices)
        {
            _logger.LogInformation("Evaluando factura ID: {InvoiceId}, Estado actual: {Status}", invoice.Id, invoice.Status);
            string previousStatus = invoice.Status;

            if (invoice.TransitionToNextStage())
            {
                try
                {
                    await _emailService.SendReminderAsync(invoice.ClientEmail, invoice.ClientName, invoice.Status);
                    results.Add($"[ÉXITO] Factura {invoice.Id}: Transición de '{previousStatus}' a '{invoice.Status}'. Correo enviado.");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Fallo de transporte SMTP al enviar correo para factura {InvoiceId}. El estado se persistirá de todas formas.", invoice.Id);
                    results.Add($"[ADVERTENCIA] Factura {invoice.Id}: Transición a '{invoice.Status}'. Falló el envío de correo, pero el estado fue actualizado en BD.");
                }

                await _repository.UpdateStatusAsync(invoice.Id, invoice.Status);
            }
            else
            {
                _logger.LogWarning("La factura {InvoiceId} se encuentra en un estado no transicionable.", invoice.Id);
            }
        }

        return results;
    }
}
