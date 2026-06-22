using Monolegal.Domain.Entities;

namespace Monolegal.Domain.Repositories;

public interface IInvoiceRepository
{
    Task<List<Invoice>> GetAllInvoicesAsync();
    Task<List<Invoice>> GetPendingRemindersAsync();
    Task UpdateStatusAsync(string invoiceId, string newStatus);
}