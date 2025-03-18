using Desafio_Jogo_De_Baralho.Models;

namespace Desafio_Jogo_De_Baralho.Interfaces
{
    public interface IClienteAPIService
    {
        Task<Baralho> CriarBaralhoAsync();
        Task<List<Carta>> DistribuirCartasAsync(string deckId, int quantidade);
        Task<Baralho> EmbaralharCartasAsync(string deckId);
        Task<Baralho> FinalizarJogoAsync(string deckId);
        Task<Baralho> ObterBaralhoAsync(string deckId);
    }
}