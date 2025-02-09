﻿using System.Text.Json.Serialization;

namespace RentalManager.Application.Commands.Responses
{
    public class RentMotorcycleCommandResponse
    {
        [JsonInclude, JsonPropertyName("identificador")]
        public string? Id { get; set; }

        [JsonInclude, JsonPropertyName("entregador_id")]
        public string DeliveryPersonId { get; set; }

        [JsonInclude, JsonPropertyName("moto_id")]
        public string MotorcycleId { get; set; }

        [JsonInclude, JsonPropertyName("data_inicio")]
        public DateTime Start { get; set; }        
        
        [JsonInclude, JsonPropertyName("data_termino")]
        public DateTime? Finish { get; set; }

        [JsonInclude, JsonPropertyName("data_previsao_termino")]
        public DateTime EndForecast { get; set; }

        [JsonInclude, JsonPropertyName("plano")]
        public int Plan { get; set; }
    }
}
