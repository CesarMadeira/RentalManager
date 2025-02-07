using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace RentalManager.Application.Commands.Requests;

public class RegisterDeliveryPersonCommandRequest
{
    [JsonInclude, JsonPropertyName("identificador")]
    public string Id { get; set; }
    [JsonInclude, JsonPropertyName("nome")]
    public string Name { get; set; }
    [JsonInclude, JsonPropertyName("cnpj")]
    public string CNPJ { get; set; }
    [JsonInclude, JsonPropertyName("data_nascimento")]
    public DateTime DateOfBirth { get; set; }
    [JsonInclude, JsonPropertyName("numero_cnh")]
    public string DocumentNumber { get; set; }
    [JsonInclude, JsonPropertyName("tipo_cnh")]
    public string DocumentType { get; set; }
    [JsonInclude, JsonPropertyName("imagem_cnh")]
    public string DocumentImage { get; set; }
}
