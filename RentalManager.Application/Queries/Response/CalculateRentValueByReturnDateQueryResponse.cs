using System.Text.Json.Serialization;

namespace RentalManager.Application.Queries.Response;

public class CalculateRentValueByReturnDateQueryResponse
{
    [JsonInclude, JsonPropertyName("valor_locacao")]
    public decimal RentalValue { get; set; }
}
