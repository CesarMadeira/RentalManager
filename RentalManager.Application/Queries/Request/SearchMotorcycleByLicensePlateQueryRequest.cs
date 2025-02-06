using System.Text.Json.Serialization;

namespace RentalManager.Application.Queries.Request;

public class SearchMotorcycleByLicensePlateQueryRequest
{
    [JsonInclude, JsonPropertyName("placa")]
    public string LicencePlate { get; set; }
}
