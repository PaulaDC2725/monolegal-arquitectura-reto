namespace Monolegal.Domain.Entities;

public class Invoice
{
    public string? Id { get; set; } 
    public string ClientId { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ClientEmail { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty; 
}