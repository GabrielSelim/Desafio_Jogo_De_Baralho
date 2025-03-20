using Desafio_Jogo_De_Baralho.Exceptions;
using Desafio_Jogo_De_Baralho.Interfaces;
using Desafio_Jogo_De_Baralho.Models;

namespace Desafio_Jogo_De_Baralho.Services
{
    public class JogoServico : IJogoService
    {
        private readonly IClienteAPIService _clienteApi;
        private const int MaxJogadores = 10;

        public JogoServico(IClienteAPIService clienteApi)
        {
            _clienteApi = clienteApi;
        }

        public async Task<Baralho> CriarBaralhoAsync()
        {
            try
            {
                return await _clienteApi.CriarBaralhoAsync();
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                throw new ApiException("A API está fora do ar. Tente novamente mais tarde.");
            }
        }

        public async Task<List<Jogador>> DistribuirCartasAsync(string deckId, int numeroDeJogadores)
        {
            if (numeroDeJogadores < 2)
            {
                throw new ApiException("O número de jogadores deve ser pelo menos 2.");
            }

            if (numeroDeJogadores > MaxJogadores)
            {              
                throw new ApiException($"O número máximo de jogadores é {MaxJogadores}.");
            }

            try
            {
                var baralho = await _clienteApi.ObterBaralhoAsync(deckId);
                if (baralho == null)
                {
                    throw new ApiException($"Baralho com ID {deckId} não encontrado.");
                }

                if (baralho.CartasRestantes != 52)
                {
                    throw new ApiException("As cartas só podem ser distribuídas novamente se o baralho for embaralhado.");
                }

                var jogadores = new List<Jogador>();
                for (int i = 0; i < numeroDeJogadores; i++)
                {
                    var cartas = await _clienteApi.DistribuirCartasAsync(deckId, 5);
                    jogadores.Add(new Jogador { Nome = $"Jogador {i + 1}", Cartas = cartas });
                }

                return jogadores;
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                throw new ApiException("A API está fora do ar. Tente novamente mais tarde.");
            }
        }

        public async Task<Baralho> ObterBaralhoAsync(string deckId)
        {
            try
            {
                return await _clienteApi.ObterBaralhoAsync(deckId);
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                throw new ApiException("A API está fora do ar. Tente novamente mais tarde.");
            }
        }

        public async Task<Baralho> EmbaralharCartasAsync(string deckId)
        {
            try
            {
                return await _clienteApi.EmbaralharCartasAsync(deckId);
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                throw new ApiException("A API está fora do ar. Tente novamente mais tarde.");
            }
        }

        public async Task<(List<(Jogador jogador, Carta carta)> vencedores, string resultado)> CompararCartasAsync(List<Jogador> jogadores)
        {
            var valores = new Dictionary<string, int>
            {
                { "2", 2 }, { "3", 3 }, { "4", 4 }, { "5", 5 }, { "6", 6 }, { "7", 7 }, { "8", 8 }, { "9", 9 }, { "10", 10 },
                { "JACK", 11 }, { "QUEEN", 12 }, { "KING", 13 }, { "ACE", 14 }
            };

            List<(Jogador jogador, Carta carta)> vencedores = new List<(Jogador jogador, Carta carta)>();
            int maiorValor = 0;

            foreach (var jogador in jogadores)
            {
                Carta melhorCarta = null;
                int valorMelhorCarta = 0;

                foreach (var carta in jogador.Cartas)
                {
                    ValidarCarta(carta, valores);

                    if (valores[carta.Valor] > valorMelhorCarta)
                    {
                        valorMelhorCarta = valores[carta.Valor];
                        melhorCarta = carta;
                    }
                }

                if (melhorCarta != null)
                {
                    if (valorMelhorCarta > maiorValor)
                    {
                        maiorValor = valorMelhorCarta;
                        vencedores.Clear();
                        vencedores.Add((jogador, melhorCarta));
                    }
                    else if (valorMelhorCarta == maiorValor)
                    {
                        vencedores.Add((jogador, melhorCarta));
                    }
                }
            }

            string resultado = vencedores.Count > 1 ? "Empate" : "Vitória";

            return (vencedores, resultado);
        }

        private void ValidarCarta(Carta carta, Dictionary<string, int> valores)
        {
            if (carta == null || string.IsNullOrEmpty(carta.Valor) || string.IsNullOrEmpty(carta.Naipe) || string.IsNullOrEmpty(carta.Codigo) || string.IsNullOrEmpty(carta.Imagem))
            {
                throw new ApiException("Carta inválida: todos os campos devem ser preenchidos.");
            }

            if (!valores.ContainsKey(carta.Valor))
            {
                throw new ApiException($"Valor de carta inválido: {carta.Valor}");
            }
        }

        public async Task<Baralho> FinalizarJogoAsync(string deckId)
        {
            try
            {
                var response = await _clienteApi.FinalizarJogoAsync(deckId);
                return response;
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                throw new ApiException("A API está fora do ar. Tente novamente mais tarde.");
            }
        }

        public object CriarResponseCompararCartas(List<(Jogador jogador, Carta carta)> vencedores, string resultado)
        {
            var response = vencedores.Select(v => new { v.jogador.Nome, Carta = v.carta }).ToList();
            return new { vencedores = response, resultado };
        }
    }
}