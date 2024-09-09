using System.Text.Json.Serialization;

namespace Unistream.TestTask.Common;

public class InsertTransactionModel
{
    [JsonPropertyName("id")]
    public Guid? Id { get; set; }

    [JsonPropertyName("operationDate")]
    public DateTime? OperationDate { get; set; }

    [JsonPropertyName("amount")]
    [JsonRequired]
    public decimal Amount { get; set; }
}


