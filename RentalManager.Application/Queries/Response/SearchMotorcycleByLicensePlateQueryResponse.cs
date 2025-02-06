using System.Text.Json.Serialization;

namespace RentalManager.Application.Queries.Response;

public class SearchMotorcycleByLicensePlateQueryResponse
{
    public List<SearchMotorcycleByLicensePlateQueryResponseItem> Item { get; set; }
}

public class SearchMotorcycleByLicensePlateQueryResponseItem
{
    [JsonInclude, JsonPropertyName("identificador")]
    public string Identifier { get; set; }
    [JsonInclude, JsonPropertyName("ano")]
    public int Year { get; set; }
    [JsonInclude, JsonPropertyName("modelo")]
    public string Model { get; set; }
    [JsonInclude, JsonPropertyName("placa")]
    public string LicencePlate { get; set; }
}
