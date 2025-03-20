using Desafio_Jogo_De_Baralho.Interfaces;
using Desafio_Jogo_De_Baralho.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Desafio_Jogo_De_Baralho.Exceptions;

namespace Desafio_Jogo_De_Baralho.Services
{
    public class ClienteApiService : IClienteAPIService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://deckofcardsapi.com/api/deck/";

        public ClienteApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Baralho> CriarBaralhoAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}new/");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Erro ao criar baralho.");
            }
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Baralho>(content)!;
        }

        public async Task<List<Carta>> DistribuirCartasAsync(string deckId, int quantidade)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}{deckId}/draw/?count={quantidade}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Erro ao distribuir cartas.");
            }
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<DistribuirCartasResponse>(content);
            return result!.Cartas;
        }

        public async Task<Baralho> EmbaralharCartasAsync(string deckId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}{deckId}/shuffle/");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Erro ao embaralhar cartas.");
            }
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Baralho>(content)!;
        }

        public async Task<Baralho> FinalizarJogoAsync(string deckId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}{deckId}/return/");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Erro ao finalizar jogo.");
            }
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Baralho>(content)!;
        }

        public async Task<Baralho> ObterBaralhoAsync(string deckId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}{deckId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Baralho>(content)!;
        }
    }
}