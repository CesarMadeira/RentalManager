using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentalManager.Application.Commands.Requests;

public class RegisterNewMotorcycleCommandRequest
{
    [Required]
    [JsonInclude, JsonPropertyName("identificador")]
    public string Id { get; set; }

    [Required]
    [MinLength(4)]
    [JsonInclude, JsonPropertyName("ano")]
    public int Year { get; set; }

    [Required]
    [MinLength(4)]
    [JsonInclude, JsonPropertyName("modelo")]
    public string Model { get; set; }

    [Required]
    [MinLength(7)]
    [JsonInclude, JsonPropertyName("placa")]
    public string LicencePlate { get; set; }
}
