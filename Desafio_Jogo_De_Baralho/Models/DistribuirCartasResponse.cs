using System.Text.Json.Serialization;

namespace Desafio_Jogo_De_Baralho.Models
{
    public class DistribuirCartasResponse
    {
        [JsonPropertyName("cards")]
        public required List<Carta> Cartas { get; set; }
    }
}