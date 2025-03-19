using Desafio_Jogo_De_Baralho.Controllers;
using Desafio_Jogo_De_Baralho.Interfaces;
using Desafio_Jogo_De_Baralho.Models;
using Desafio_Jogo_De_Baralho.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Desafio_Jogo_De_Baralho.Testes
{
    public class JogoControllerTests
    {
        private readonly Mock<IJogoService> _jogoServiceMock;
        private readonly JogoController _controller;

        public JogoControllerTests()
        {
            _jogoServiceMock = new Mock<IJogoService>();
            _controller = new JogoController(_jogoServiceMock.Object);
        }

        [Fact(DisplayName = "CriarBaralho deve retornar OkResult com o Baralho criado")]
        public async Task CriarBaralho_ReturnsOkResult_WithBaralho()
        {
            var baralho = new Baralho { Id = "deck1", Embaralhado = true, CartasRestantes = 52 };
            _jogoServiceMock.Setup(service => service.CriarBaralhoAsync()).ReturnsAsync(baralho);

            var result = await _controller.CriarBaralho();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Baralho>(okResult.Value);
            Assert.Equal(baralho.Id, returnValue.Id);
        }

        [Fact(DisplayName = "DistribuirCartas deve retornar BadRequest se a API estiver fora do ar")]
        public async Task DistribuirCartas_ReturnsBadRequest_IfApiIsDown()
        {
            var deckId = "deck1";
            var numeroDeJogadores = 4;

            _jogoServiceMock.Setup(service => service.DistribuirCartasAsync(deckId, numeroDeJogadores))
                .ThrowsAsync(new ApiException("A API está fora do ar. Tente novamente mais tarde."));

            var result = await _controller.DistribuirCartas(deckId, numeroDeJogadores);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var returnValue = Assert.IsType<ApiErrorResponse>(badRequestResult.Value);
            Assert.Equal("A API está fora do ar. Tente novamente mais tarde.", returnValue.Mensagem);
        }

        [Fact(DisplayName = "DistribuirCartas deve retornar OkResult com a lista de jogadores")]
        public async Task DistribuirCartas_ReturnsOkResult_WithJogadores()
        {
            var jogadores = new List<Jogador>
            {
                new Jogador { Nome = "Jogador 1", Cartas = new List<Carta>() },
                new Jogador { Nome = "Jogador 2", Cartas = new List<Carta>() }
            };
            _jogoServiceMock.Setup(service => service.DistribuirCartasAsync("deck1", 2)).ReturnsAsync(jogadores);

            var result = await _controller.DistribuirCartas("deck1", 2);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Jogador>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact(DisplayName = "DistribuirCartas deve retornar BadRequest se o número de jogadores for menor que 1")]
        public async Task DistribuirCartas_ReturnsBadRequest_IfNumeroDeJogadoresIsLessThanOne()
        {
            var deckId = "deck1";
            var numeroDeJogadores = 0;

            _jogoServiceMock.Setup(service => service.DistribuirCartasAsync(deckId, numeroDeJogadores))
                .ThrowsAsync(new ApiException("O número de jogadores deve ser pelo menos 1."));

            var result = await _controller.DistribuirCartas(deckId, numeroDeJogadores);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var returnValue = Assert.IsType<ApiErrorResponse>(badRequestResult.Value);
            Assert.Equal("O número de jogadores deve ser pelo menos 1.", returnValue.Mensagem);
        }

        [Fact(DisplayName = "DistribuirCartas deve retornar BadRequest se o número de jogadores for maior que o máximo permitido")]
        public async Task DistribuirCartas_ReturnsBadRequest_IfNumeroDeJogadoresExceedsMax()
        {
            var deckId = "deck1";
            var numeroDeJogadores = 11;

            _jogoServiceMock.Setup(service => service.DistribuirCartasAsync(deckId, numeroDeJogadores))
                .ThrowsAsync(new ApiException("O número máximo de jogadores é 10."));

            var result = await _controller.DistribuirCartas(deckId, numeroDeJogadores);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var returnValue = Assert.IsType<ApiErrorResponse>(badRequestResult.Value);
            Assert.Equal("O número máximo de jogadores é 10.", returnValue.Mensagem);
        }

        [Fact(DisplayName = "DistribuirCartas deve retornar BadRequest se o baralho não for encontrado")]
        public async Task DistribuirCartas_ReturnsBadRequest_IfDeckNotFound()
        {
            var deckId = "invalidDeckId";
            var numeroDeJogadores = 4;

            _jogoServiceMock.Setup(service => service.DistribuirCartasAsync(deckId, numeroDeJogadores))
                .ThrowsAsync(new ApiException("Baralho com ID inválido não encontrado."));

            var result = await _controller.DistribuirCartas(deckId, numeroDeJogadores);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var returnValue = Assert.IsType<ApiErrorResponse>(badRequestResult.Value);
            Assert.Equal("Baralho com ID inválido não encontrado.", returnValue.Mensagem);
        }

        [Fact(DisplayName = "DistribuirCartas deve retornar BadRequest se o baralho não tiver 52 cartas")]
        public async Task DistribuirCartas_ReturnsBadRequest_IfDeckNotFull()
        {
            var deckId = "testDeckId";
            var numeroDeJogadores = 4;

            _jogoServiceMock.Setup(service => service.DistribuirCartasAsync(deckId, numeroDeJogadores))
                .ThrowsAsync(new ApiException("As cartas só podem ser distribuídas novamente se o baralho for embaralhado."));

            var result = await _controller.DistribuirCartas(deckId, numeroDeJogadores);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var returnValue = Assert.IsType<ApiErrorResponse>(badRequestResult.Value);
            Assert.Equal("As cartas só podem ser distribuídas novamente se o baralho for embaralhado.", returnValue.Mensagem);
        }

        [Fact(DisplayName = "EmbaralharCartas deve retornar OkResult com o Baralho embaralhado")]
        public async Task EmbaralharCartas_ReturnsOkResult_WithBaralho()
        {
            var baralho = new Baralho { Id = "deck1", Embaralhado = true, CartasRestantes = 52 };
            _jogoServiceMock.Setup(service => service.EmbaralharCartasAsync("deck1")).ReturnsAsync(baralho);

            var result = await _controller.EmbaralharCartas("deck1");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Baralho>(okResult.Value);
            Assert.True(returnValue.Embaralhado);
        }

        [Fact(DisplayName = "CompararCartas deve retornar OkResult com os vencedores")]
        public async Task CompararCartas_ReturnsOkResult_WithVencedores()
        {
            var jogadores = new List<Jogador>
            {
                new Jogador { Nome = "Jogador 1", Cartas = new List<Carta> { new Carta { Valor = "ACE" } } },
                new Jogador { Nome = "Jogador 2", Cartas = new List<Carta> { new Carta { Valor = "KING" } } }
            };
            var vencedores = new List<(Jogador jogador, Carta carta)>
            {
                (jogadores[0], jogadores[0].Cartas[0])
            };
            _jogoServiceMock.Setup(service => service.CompararCartasAsync(jogadores)).ReturnsAsync((vencedores, "Vitória"));

            var result = await _controller.CompararCartas(jogadores);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;

            Assert.NotNull(returnValue);

            var vencedoresProperty = returnValue.GetType().GetProperty("vencedores");
            var resultadoProperty = returnValue.GetType().GetProperty("resultado");

            Assert.NotNull(vencedoresProperty);
            Assert.NotNull(resultadoProperty);

            var vencedoresList = vencedoresProperty.GetValue(returnValue) as IEnumerable<dynamic>;
            var resultado = resultadoProperty.GetValue(returnValue) as string;

            Assert.NotNull(vencedoresList);
            Assert.Single(vencedoresList);

            var vencedor = vencedoresList.First();
            var nomeProperty = vencedor.GetType().GetProperty("Nome");
            var cartaProperty = vencedor.GetType().GetProperty("Carta");

            Assert.NotNull(nomeProperty);
            Assert.NotNull(cartaProperty);

            var nome = nomeProperty.GetValue(vencedor) as string;
            var carta = cartaProperty.GetValue(vencedor);

            var valorProperty = carta.GetType().GetProperty("Valor");
            Assert.NotNull(valorProperty);

            var valor = valorProperty.GetValue(carta) as string;

            Assert.Equal("Vitória", resultado);
            Assert.Equal("Jogador 1", nome);
            Assert.Equal("ACE", valor);
        }

        [Fact(DisplayName = "CompararCartas deve retornar OkResult com empate")]
        public async Task CompararCartas_ReturnsOkResult_WithEmpate()
        {
            var jogadores = new List<Jogador>
            {
                new Jogador { Nome = "Jogador 1", Cartas = new List<Carta> { new Carta { Valor = "ACE" } } },
                new Jogador { Nome = "Jogador 2", Cartas = new List<Carta> { new Carta { Valor = "ACE" } } }
            };
            var vencedores = new List<(Jogador jogador, Carta carta)>
            {
                (jogadores[0], jogadores[0].Cartas[0]),
                (jogadores[1], jogadores[1].Cartas[0])
            };
            _jogoServiceMock.Setup(service => service.CompararCartasAsync(jogadores)).ReturnsAsync((vencedores, "Empate"));

            var result = await _controller.CompararCartas(jogadores);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;

            Assert.NotNull(returnValue);

            var vencedoresProperty = returnValue.GetType().GetProperty("vencedores");
            var resultadoProperty = returnValue.GetType().GetProperty("resultado");

            Assert.NotNull(vencedoresProperty);
            Assert.NotNull(resultadoProperty);

            var vencedoresList = vencedoresProperty.GetValue(returnValue) as IEnumerable<dynamic>;
            var resultado = resultadoProperty.GetValue(returnValue) as string;

            Assert.NotNull(vencedoresList);
            Assert.Equal(2, vencedoresList.Count());

            var vencedor1 = vencedoresList.First();
            var vencedor2 = vencedoresList.Last();

            var nomeProperty1 = vencedor1.GetType().GetProperty("Nome");
            var cartaProperty1 = vencedor1.GetType().GetProperty("Carta");

            var nomeProperty2 = vencedor2.GetType().GetProperty("Nome");
            var cartaProperty2 = vencedor2.GetType().GetProperty("Carta");

            Assert.NotNull(nomeProperty1);
            Assert.NotNull(cartaProperty1);
            Assert.NotNull(nomeProperty2);
            Assert.NotNull(cartaProperty2);

            var nome1 = nomeProperty1.GetValue(vencedor1) as string;
            var carta1 = cartaProperty1.GetValue(vencedor1);

            var nome2 = nomeProperty2.GetValue(vencedor2) as string;
            var carta2 = cartaProperty2.GetValue(vencedor2);

            var valorProperty1 = carta1.GetType().GetProperty("Valor");
            var valorProperty2 = carta2.GetType().GetProperty("Valor");

            Assert.NotNull(valorProperty1);
            Assert.NotNull(valorProperty2);

            var valor1 = valorProperty1.GetValue(carta1) as string;
            var valor2 = valorProperty2.GetValue(carta2) as string;

            Assert.Equal("Empate", resultado);
            Assert.Equal("Jogador 1", nome1);
            Assert.Equal("ACE", valor1);
            Assert.Equal("Jogador 2", nome2);
            Assert.Equal("ACE", valor2);
        }

        [Fact(DisplayName = "CompararCartas deve retornar BadRequest com valor de carta inválido")]
        public async Task CompararCartas_ReturnsBadRequest_WithInvalidCardValue()
        {
            var jogadores = new List<Jogador>
            {
                new Jogador { Nome = "Jogador 1", Cartas = new List<Carta> { new Carta { Valor = "INVALID" } } }
            };
            _jogoServiceMock.Setup(service => service.CompararCartasAsync(jogadores)).ThrowsAsync(new ApiException("Valor de carta inválido: INVALID"));

            var result = await _controller.CompararCartas(jogadores);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = badRequestResult.Value;

            Assert.NotNull(returnValue);

            var mensagemProperty = returnValue.GetType().GetProperty("mensagem");

            Assert.NotNull(mensagemProperty);

            var mensagem = mensagemProperty.GetValue(returnValue) as string;

            Assert.Equal("Valor de carta inválido: INVALID", mensagem);
        }

        [Fact(DisplayName = "FinalizarJogo deve retornar OkResult com o Baralho finalizado")]
        public async Task FinalizarJogo_ReturnsOkResult_WithBaralho()
        {
            var baralho = new Baralho { Id = "deck1", Embaralhado = false, CartasRestantes = 0 };
            _jogoServiceMock.Setup(service => service.FinalizarJogoAsync("deck1")).ReturnsAsync(baralho);

            var result = await _controller.FinalizarJogo("deck1");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Baralho>(okResult.Value);
            Assert.Equal(0, returnValue.CartasRestantes);
        }
    }
}