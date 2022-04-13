using catalogo_jogos.BLL.Infra;
using catalogo_jogos.DTOs.Request;
using catalogo_jogos.DTOs.Response;
using catalogo_jogos.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace catalogo_jogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameResponseDTO>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1,50)] int quantidade = 5)
        {
            var result = await _jogoService.Obter(pagina, quantidade);

            if (result.Count() == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{idJogo}")]
        public async Task<ActionResult<GameResponseDTO>> ObterPorId([FromRoute] Guid idJogo)
        {
            var result = await _jogoService.ObterPorId(idJogo);

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GameResponseDTO>> Inserir([FromBody] GameRequestDTO jogo)
        {
            try
            {
                var result = await _jogoService.Inserir(jogo);
                return Ok(result);
            }
            catch (JogoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome nesta produtora");
            }
        }

        [HttpPut]
        [Route("{idJogo}")]
        public async Task<ActionResult> Atualizar([FromRoute] Guid idJogo, [FromBody] GameRequestDTO jogo)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, jogo);
                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Esse jogo não existe, verifique as informações");
            }
        }

        [HttpPatch]
        [Route("{idJogo}/preco/{preco}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoService.AtualizarPreco(idJogo, preco);
                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Esse jogo não existe, verifique as informações");
            }
        }

        [HttpDelete]
        [Route("{idJogo}")]
        public async Task<ActionResult> Excluir([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.Remover(idJogo);
                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Esse jogo não existe, verifique as informações");
            }
        }
    }
   }
