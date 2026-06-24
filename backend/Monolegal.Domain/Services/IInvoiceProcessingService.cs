namespace Monolegal.Domain.Services;

public interface IInvoiceProcessingService
{
    Task<List<string>> ProcessPendingInvoicesAsync();
}