using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Monolegal.Domain.Entities;
using Monolegal.Domain.Repositories;

namespace Monolegal.Infrastructure.Repositories;

internal class MongoInvoiceModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("clientId")]
    public string ClientId { get; set; } = string.Empty;

    [BsonElement("clientName")]
    public string ClientName { get; set; } = string.Empty;

    [BsonElement("clientEmail")]
    public string ClientEmail { get; set; } = string.Empty;

    [BsonElement("amount")]
    public decimal Amount { get; set; }

    [BsonElement("status")]
    public string Status { get; set; } = string.Empty;
}

public class MongoInvoiceRepository : IInvoiceRepository
{
    private readonly IMongoCollection<MongoInvoiceModel> _invoiceCollection;

    public MongoInvoiceRepository()
    {
      
      var connectionString = "mongodb+srv://Paula:Monolegal2026@monolegalcluster.5jb1jxr.mongodb.net/?appName=MonolegalCluster";
        
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("MonolegalDB");
        _invoiceCollection = database.GetCollection<MongoInvoiceModel>("Invoices");
    }

    public async Task<List<Invoice>> GetAllInvoicesAsync()
    {
        
           var mongoData = await _invoiceCollection.Find(_ => true).ToListAsync();

         return mongoData.Select(m => new Invoice
        {
            Id = m.Id,
            ClientId = m.ClientId,
            ClientName = m.ClientName,
            ClientEmail = m.ClientEmail,
            Amount = m.Amount,
            Status = m.Status
        }).ToList();
    }
}