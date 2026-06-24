using Monolegal.Domain.Constants;

namespace Monolegal.Domain.Entities;

public class Invoice
{
    public string? Id { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientEmail { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    
    public string Status { get; set; } = InvoiceStatus.PrimerRecordatorio;

    public bool TransitionToNextStage()
    {
        if (Status == InvoiceStatus.PrimerRecordatorio)
        {
            Status = InvoiceStatus.SegundoRecordatorio;
            return true;
        }

        if (Status == InvoiceStatus.SegundoRecordatorio)
        {
            Status = InvoiceStatus.Desactivado;
            return true;
        }

        return false;
    }
}