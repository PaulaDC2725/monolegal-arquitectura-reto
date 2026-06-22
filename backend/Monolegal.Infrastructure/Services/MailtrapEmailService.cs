using System.Net;
using System.Net.Mail;
using Monolegal.Domain.Services;

namespace Monolegal.Infrastructure.Services;

public class MailtrapEmailService : IEmailService
{
    public async Task SendReminderAsync(string clientEmail, string clientName, string newStatus)
    {
        using var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential("030f025e3bdae6", "1c44b2bf9cb34b"),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress("notificaciones@monolegal.co", "Vigilancia Monolegal"),
            Subject = $"Actualización de Estado Procesal: {newStatus}",
           Body = $"Estimado {clientName},\n\nEl sistema de inteligencia artificial de Monolegal ha detectado un movimiento en su expediente. El nuevo estado asignado es: '{newStatus}'.\n\nPara ver el detalle, ingrese a su portal web.\n\nAtentamente,\nPlataforma de Automatización."
        };
        mailMessage.To.Add(clientEmail);

        await client.SendMailAsync(mailMessage);
    }
}