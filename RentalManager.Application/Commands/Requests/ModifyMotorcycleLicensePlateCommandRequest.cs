using System.Text.Json.Serialization;

namespace RentalManager.Application.Commands.Requests;

public class ModifyMotorcycleLicensePlateCommandRequest
{
    [JsonInclude, JsonPropertyName("identificador")]
    public string MotorcycleId { get; set; }
    [JsonInclude, JsonPropertyName("placa")]
    public string LicencePlate { get; set; }
}
