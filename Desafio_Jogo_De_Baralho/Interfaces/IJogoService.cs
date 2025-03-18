using Desafio_Jogo_De_Baralho.Models;

namespace Desafio_Jogo_De_Baralho.Interfaces
{
    public interface IJogoService
    {
        Task<Baralho> CriarBaralhoAsync();
        Task<List<Jogador>> DistribuirCartasAsync(string deckId, int numeroDeJogadores);
        Task<Baralho> EmbaralharCartasAsync(string deckId);
        Task<(List<(Jogador jogador, Carta carta)> vencedores, string resultado)> CompararCartasAsync(List<Jogador> jogadores);
        Task<Baralho> FinalizarJogoAsync(string deckId);
    }
}