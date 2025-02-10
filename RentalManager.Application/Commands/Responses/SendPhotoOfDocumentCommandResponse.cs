using System.Text.Json.Serialization;

namespace RentalManager.Application.Commands.Responses;

public class SendPhotoOfDocumentCommandResponse
{
    [JsonInclude, JsonPropertyName("url")]
    public string URL { get; set; }
}
