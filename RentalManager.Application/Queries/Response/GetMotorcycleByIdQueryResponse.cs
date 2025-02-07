using System.Text.Json.Serialization;

namespace RentalManager.Application.Queries.Response
{
    public class GetMotorcycleByIdQueryResponse
    {
        [JsonInclude, JsonPropertyName("identificador")]
        public string Id { get; set; }
        [JsonInclude, JsonPropertyName("ano")]
        public int Year { get; set; }
        [JsonInclude, JsonPropertyName("modelo")]
        public string Model { get; set; }
        [JsonInclude, JsonPropertyName("placa")]
        public string LicencePlate { get; set; }
    }
}
