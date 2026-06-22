namespace Monolegal.Domain.Services;

public interface IEmailService
{
    Task SendReminderAsync(string clientEmail, string clientName, string newStatus);
}