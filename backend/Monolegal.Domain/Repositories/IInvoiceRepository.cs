using Monolegal.Domain.Entities;

namespace Monolegal.Domain.Repositories;

public interface IInvoiceRepository
{
    Task<List<Invoice>> GetAllInvoicesAsync();
}