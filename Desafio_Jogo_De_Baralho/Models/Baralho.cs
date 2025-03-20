using System.Text.Json.Serialization;

namespace Desafio_Jogo_De_Baralho.Models
{
    public class Baralho
    {
        [JsonPropertyName("deck_id")]
        public required string Id { get; set; }

        [JsonPropertyName("shuffled")]
        public required bool Embaralhado { get; set; }

        [JsonPropertyName("remaining")]
        public required int CartasRestantes { get; set; }

        [JsonPropertyName("cards")]
        public required List<Carta> Cartas { get; set; } = new List<Carta>();
    }
}