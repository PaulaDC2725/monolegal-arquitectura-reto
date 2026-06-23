using System.Net;
using System.Net.Mail;
using System.Net.Security;
using Microsoft.Extensions.Options;
using Monolegal.Domain.Services;
using Monolegal.Infrastructure.Configuration;

namespace Monolegal.Infrastructure.Services;

public class BrevoEmailService(IOptions<EmailConfiguration> options) : IEmailService
{
    private readonly EmailConfiguration _config = options.Value;

    public async Task SendReminderAsync(string clientEmail, string clientName, string newStatus)
    {
        ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

        using var client = new SmtpClient();
        Console.WriteLine($"[SECURITY DEBUG] Password in config is null/empty: {string.IsNullOrEmpty(_config.SmtpPassword)}");
        Console.WriteLine($"[SECURITY DEBUG] Password preview: {_config.SmtpPassword?.Substring(0, 5)}...");
        Console.WriteLine($"[DEBUG CONFIG] Server: '{_config.SmtpServer}', Port: {_config.SmtpPort}, User: '{_config.SmtpUsername}', Pass Length: {_config.SmtpPassword?.Length ?? 0}, Correo remitente: '{_config.FromEmail}', Nombre remitente: '{_config.FromName}'");

        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        client.Host = _config.SmtpServer;
        client.Port = _config.SmtpPort;
        client.Credentials = new NetworkCredential(_config.SmtpUsername, _config.SmtpPassword);
        client.EnableSsl = true;

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(_config.FromEmail, _config.FromName),
            Subject = $"Actualización de Estado Procesal: {newStatus}",
            Body = $"Estimado {clientName},\n\nEl sistema de automatización de Monolegal ha detectado un movimiento en su expediente. El nuevo estado asignado es: '{newStatus}'.\n\nAtentamente,\nPlataforma de Automatización."
        };

        mailMessage.To.Add(clientEmail);

        await client.SendMailAsync(mailMessage);
    }
}