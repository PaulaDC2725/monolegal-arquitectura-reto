using Moq;
using Xunit;
using Monolegal.Domain.Entities;
using Monolegal.Domain.Repositories;
using Monolegal.Domain.Services;

namespace Monolegal.Tests.Services;

public class InvoiceProcessingServiceTests
{
    [Fact]
    public async Task ProcessPendingInvoices_WhenStatusIsPrimerRecordatorio_ShouldTransitionToSegundoYEnviarCorreo()
    {
        // 1. ARRANGE (Preparar el escenario falso)
        var mockRepo = new Mock<IInvoiceRepository>();
        var mockEmailService = new Mock<IEmailService>();

        var facturaFalsa = new Invoice 
        { 
            Id = "6671a1b2c3d4e5f6a7b8c9d0", 
            ClientEmail = "pabloperez@empresa.com", 
            ClientName = "Pablo Pérez", 
            Status = "primerrecordatorio" 
        };

        mockRepo.Setup(r => r.GetPendingRemindersAsync())
                .ReturnsAsync(new List<Invoice> { facturaFalsa });

        var sut = new InvoiceProcessingService(mockRepo.Object, mockEmailService.Object);

        await sut.ProcessPendingInvoicesAsync();


        mockRepo.Verify(r => r.UpdateStatusAsync(facturaFalsa.Id, "segundorecordatorio"), Times.Once);

        mockEmailService.Verify(e => e.SendReminderAsync(
            "pabloperez@empresa.com", 
            "Pablo Pérez", 
            "segundorecordatorio"), Times.Once);
    }
}