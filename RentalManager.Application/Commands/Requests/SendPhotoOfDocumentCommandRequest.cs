using System.Text.Json.Serialization;

namespace RentalManager.Application.Commands.Requests;

public class SendPhotoOfDocumentCommandRequest
{
    [JsonIgnore]
    public string? Id { get; set; }

    [JsonInclude, JsonPropertyName("imagem_cnh")]
    public string ImagemBase64 { get; set; }
}
