using System.Text.Json.Serialization;

namespace RentalManager.Application.Queries.Request;

public class CalculateRentValueByDateQueryRequest
{
    [JsonIgnore]
    public string? RentId { get; set; }
    [JsonInclude, JsonPropertyName("data_devolucao")]
    public DateTime EndDate { get; set; }
}
