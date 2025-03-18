using System.Text.Json.Serialization;

namespace Desafio_Jogo_De_Baralho.Models
{
    public class Carta
    {
        [JsonPropertyName("code")]
        public string Codigo { get; set; }

        [JsonPropertyName("image")]
        public string Imagem { get; set; }

        [JsonPropertyName("value")]
        public string Valor { get; set; }

        [JsonPropertyName("suit")]
        public string Naipe { get; set; }
    }
}