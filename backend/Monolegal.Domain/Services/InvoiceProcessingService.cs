using Monolegal.Domain.Entities;
using Monolegal.Domain.Repositories;

namespace Monolegal.Domain.Services;

public class InvoiceProcessingService(IInvoiceRepository repository, IEmailService emailService)
{
    private readonly IInvoiceRepository _repository = repository;
    private readonly IEmailService _emailService = emailService;

    public async Task<List<string>> ProcessPendingInvoicesAsync()
    {
        Console.WriteLine("[DEBUG] Iniciando búsqueda de facturas pendientes en MongoDB...");
        var pendingInvoices = await _repository.GetPendingRemindersAsync();
        var results = new List<string>();

        Console.WriteLine($"[DEBUG] Se han encontrado {pendingInvoices.Count} facturas para procesar.");

        if (!pendingInvoices.Any())
        {
            results.Add("No hay expedientes pendientes.");
            return results;
        }

        foreach (var invoice in pendingInvoices)
        {
            Console.WriteLine($"[DEBUG] Procesando factura: {invoice.Id} | Estado actual: {invoice.Status} | Cliente: {invoice.ClientName}");

            string nextStatus = invoice.Status switch
            {
                "primerrecordatorio" => "segundorecordatorio",
                "segundorecordatorio" => "desactivado",
                _ => invoice.Status
            };

            if (nextStatus == invoice.Status)
            {
                Console.WriteLine($"[DEBUG] La factura {invoice.Id} ya alcanzó su estado final ({invoice.Status}). Saltando...");
                continue;
            }

            try
            {
                Console.WriteLine($"[DEBUG] Intentando enviar correo a: {invoice.ClientEmail}...");
                await _emailService.SendReminderAsync(invoice.ClientEmail, invoice.ClientName, nextStatus);
                Console.WriteLine($"[DEBUG] Correo enviado exitosamente a {invoice.ClientEmail}.");

                Console.WriteLine($"[DEBUG] Actualizando estado en MongoDB para ID: {invoice.Id} a '{nextStatus}'...");
                await _repository.UpdateStatusAsync(invoice.Id!, nextStatus);
                Console.WriteLine($"[DEBUG] Base de datos actualizada correctamente para factura {invoice.Id}.");

                results.Add($"[ÉXITO] Factura {invoice.Id}: Correo enviado, estado -> {nextStatus}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR CRÍTICO] Factura {invoice.Id}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[DETALLE ERROR] {ex.InnerException.Message}");
                }
                results.Add($"[FALLO] Factura {invoice.Id}: {ex.Message}");
            }
        }

        Console.WriteLine("[DEBUG] Proceso de vigilancia finalizado.");
        return results;
    }
}