using Moq;
using Monolegal.Domain.Entities;
using Monolegal.Domain.Repositories;
using Monolegal.Domain.Services;

namespace Monolegal.Tests;

public class UnitTest1
{
    [Fact]
    public async Task ProcessPendingInvoices_ShouldUpdateToSecondReminder_WhenStatusIsFirstReminder()
    {
        var mockRepo = new Mock<IInvoiceRepository>();
        var mockEmail = new Mock<IEmailService>();

        var facturaFalsa = new Invoice
        {
            Id = "123",
            ClientName = "Prueba",
            Status = "primerrecordatorio"
        };

        mockRepo.Setup(repo => repo.GetPendingRemindersAsync())
                .ReturnsAsync(new List<Invoice> { facturaFalsa });

        var service = new InvoiceProcessingService(mockRepo.Object, mockEmail.Object);

        await service.ProcessPendingInvoicesAsync();

        mockRepo.Verify(repo => repo.UpdateStatusAsync("123", "segundorecordatorio"), Times.Once);

        mockEmail.Verify(email => email.SendReminderAsync(It.IsAny<string>(), It.IsAny<string>(), "segundorecordatorio"), Times.Once);
    }
}