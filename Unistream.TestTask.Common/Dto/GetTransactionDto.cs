using System.Text.Json.Serialization;

namespace Unistream.TestTask.Common.Dto;

public class GetTransactionDto
{
    [JsonPropertyName("id")]
    [JsonRequired]
    public Guid Id { get; set; }

    [JsonPropertyName("operationDate")]
    [JsonRequired]
    public DateTime OperationDate { get; set; }

    [JsonPropertyName("amount")]
    [JsonRequired]
    public decimal Amount { get; set; }
}
