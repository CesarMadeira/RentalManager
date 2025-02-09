using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentalManager.Application.Commands.Requests;

public class RentMotorcycleCommandRequest
{
    [JsonIgnore]
    public string? Id { get; set; }

    [Required]
    [JsonInclude, JsonPropertyName("entregador_id")]
    public string DeliveryPersonId { get; set; }

    [Required]
    [JsonInclude, JsonPropertyName("moto_id")]
    public string MotorcycleId { get; set; }

    [Required]
    [JsonInclude, JsonPropertyName("data_inicio")]
    public DateTime Start { get; set; }

    [Required]
    [JsonInclude, JsonPropertyName("plano")]
    public int Plan { get; set; }
}
