using System.Text.Json.Serialization;

namespace Desafio_Jogo_De_Baralho.Models
{
    public class Baralho
    {
        [JsonPropertyName("deck_id")]
        public required string Id { get; set; }

        [JsonPropertyName("shuffled")]
        public bool Embaralhado { get; set; }

        [JsonPropertyName("remaining")]
        public int CartasRestantes { get; set; }

        [JsonPropertyName("cards")]
        public List<Carta> Cartas { get; set; } = new List<Carta>();
    }
}