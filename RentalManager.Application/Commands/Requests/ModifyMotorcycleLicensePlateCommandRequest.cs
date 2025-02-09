using System.Text.Json.Serialization;

namespace RentalManager.Application.Commands.Requests;

public class ModifyMotorcycleLicensePlateCommandRequest
{
    [JsonIgnore]
    public string? MotorcycleId { get; set; }

    [JsonInclude, JsonPropertyName("placa")]
    public string LicencePlate { get; set; }
}
