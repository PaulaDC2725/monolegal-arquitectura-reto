using Microsoft.Extensions.Logging;
using Moq;
using Monolegal.Domain.Constants;
using Monolegal.Domain.Entities;
using Monolegal.Domain.Services;
using Monolegal.Domain.Repositories;
using Xunit;

namespace Monolegal.Tests;

public class InvoiceProcessingServiceTests
{
    private readonly Mock<IInvoiceRepository> _repositoryMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<ILogger<InvoiceProcessingService>> _loggerMock;
    private readonly InvoiceProcessingService _service;

    public InvoiceProcessingServiceTests()
    {
        _repositoryMock = new Mock<IInvoiceRepository>();
        _emailServiceMock = new Mock<IEmailService>();
        _loggerMock = new Mock<ILogger<InvoiceProcessingService>>();

        _service = new InvoiceProcessingService(
            _repositoryMock.Object,
            _emailServiceMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task ProcessPendingInvoices_WhenInvoicesExist_ShouldTransitionToSegundoYEnviarCorreo()
    {
        var invoice = new Invoice { Id = "60a1", ClientEmail = "test@legal.com", Status = InvoiceStatus.PrimerRecordatorio };
        _repositoryMock.Setup(repo => repo.GetPendingRemindersAsync()).ReturnsAsync(new List<Invoice> { invoice });

        var result = await _service.ProcessPendingInvoicesAsync();

        Assert.Contains(result, r => r.Contains("[ÉXITO]"));
        Assert.Equal(InvoiceStatus.SegundoRecordatorio, invoice.Status);
        _emailServiceMock.Verify(e => e.SendReminderAsync("test@legal.com", It.IsAny<string>(), InvoiceStatus.SegundoRecordatorio), Times.Once);
        _repositoryMock.Verify(r => r.UpdateStatusAsync("60a1", InvoiceStatus.SegundoRecordatorio), Times.Once);
    }

    [Fact]
    public async Task ProcessPendingInvoices_WhenNoInvoices_ShouldReturnEmptyNotification()
    {
        _repositoryMock.Setup(repo => repo.GetPendingRemindersAsync()).ReturnsAsync(new List<Invoice>());

        var result = await _service.ProcessPendingInvoicesAsync();

        Assert.Single(result);
        Assert.Contains("No hay expedientes pendientes", result[0]);
        _emailServiceMock.Verify(e => e.SendReminderAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ProcessPendingInvoices_WhenEmailFails_ShouldStillUpdateStatusInDatabase()
    {
        var invoice = new Invoice { Id = "80b2", ClientEmail = "fail@legal.com", Status = InvoiceStatus.PrimerRecordatorio };
        _repositoryMock.Setup(repo => repo.GetPendingRemindersAsync()).ReturnsAsync(new List<Invoice> { invoice });

        _emailServiceMock.Setup(e => e.SendReminderAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new InvalidOperationException("Connection timed out"));

        var result = await _service.ProcessPendingInvoicesAsync();

        Assert.Contains(result, r => r.Contains("[ADVERTENCIA]"));
        Assert.Equal(InvoiceStatus.SegundoRecordatorio, invoice.Status);
        _repositoryMock.Verify(r => r.UpdateStatusAsync("80b2", InvoiceStatus.SegundoRecordatorio), Times.Once);
    }
}