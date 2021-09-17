using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Controllers.V1
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

        /// <summary>
        /// Método para obter uma lista de jogos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina =1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogoService.Obter(pagina, quantidade);

            if (jogos.Count() == 0)
                return NoContent();

            return Ok(jogos);
        }

        /// <summary>
        /// Método para retornar um jogo específico pelo id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid id)
        {
            var jogo = await _jogoService.Obter(id);

            if (jogo == null)
                return NoContent();

            return Ok();
        }

        /// <summary>
        /// Método para inserir um novo jogo
        /// </summary>
        /// <param name="jogo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> Inserir([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoService.Inserir(jogoInputModel);
                return Created("", jogo);
            }
            catch (Exception)
            {

                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
            
        }

        /// <summary>
        /// Método para atualizar informações de um jogo cadastrado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jogo"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar([FromRoute] Guid id, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await _jogoService.Atualizar(id, jogoInputModel);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Jogo não encontrado");
            }
            
        }

        /// <summary>
        /// Método para atualizar uma informação específica de um jogo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="preco"></param>
        /// <returns></returns>
        [HttpPatch("{id:guid}/preco/{preco:double}")]
        public async Task<ActionResult> Atualizar([FromRoute] Guid id, [FromBody] double preco)
        {
            try
            {
                await _jogoService.Atualizar(id, preco);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Jogo não encontrado");
            }

        }

        /// <summary>
        /// Método para remover um jogo pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Remover([FromRoute] Guid id)
        {
            try
            {
                await _jogoService.Remover(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Jogo não encontrado");
            }

        }


    }
}
