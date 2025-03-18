using Desafio_Jogo_De_Baralho.Models;
using Desafio_Jogo_De_Baralho.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Desafio_Jogo_De_Baralho.Exceptions;

namespace Desafio_Jogo_De_Baralho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JogoController : ControllerBase
    {
        private readonly IJogoService _jogoServico;

        public JogoController(IJogoService jogoServico)
        {
            _jogoServico = jogoServico;
        }

        [HttpPost("criar-baralho")]
        public async Task<ActionResult<Baralho>> CriarBaralho()
        {
            try
            {
                var baralho = await _jogoServico.CriarBaralhoAsync();
                return Ok(baralho);
            }
            catch (ApiException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost("distribuir-cartas")]
        public async Task<ActionResult<List<Jogador>>> DistribuirCartas([FromQuery] string deckId, [FromQuery] int numeroDeJogadores)
        {
            try
            {
                var jogadores = await _jogoServico.DistribuirCartasAsync(deckId, numeroDeJogadores);
                return Ok(jogadores);
            }
            catch (ApiException ex)
            {
                return BadRequest(new ApiErrorResponse { Mensagem = ex.Message });
            }
        }

        [HttpPost("embaralhar-cartas")]
        public async Task<ActionResult<Baralho>> EmbaralharCartas([FromQuery] string deckId)
        {
            try
            {
                var baralho = await _jogoServico.EmbaralharCartasAsync(deckId);
                return Ok(baralho);
            }
            catch (ApiException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost("comparar-cartas")]
        public async Task<ActionResult> CompararCartas([FromBody] List<Jogador> jogadores)
        {
            try
            {
                var (vencedores, resultado) = await _jogoServico.CompararCartasAsync(jogadores);
                var response = vencedores.Select(v => new { v.jogador.Nome, Carta = v.carta }).ToList();
                return Ok(new { vencedores = response, resultado });
            }
            catch (ApiException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost("finalizar-jogo")]
        public async Task<ActionResult<Baralho>> FinalizarJogo([FromQuery] string deckId)
        {
            try
            {
                var baralho = await _jogoServico.FinalizarJogoAsync(deckId);
                return Ok(baralho);
            }
            catch (ApiException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}