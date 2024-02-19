using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolio.API.Controllers
{
    public class OperacaoController : BaseController
    {
        private readonly IOperacaoFacade operacaoFacade;
        private readonly IOperacaoRepository operacaoRepository;

        public OperacaoController(IOperacaoFacade operacaoFacade, IOperacaoRepository operacaoRepository)
        {
            this.operacaoFacade = operacaoFacade;
            this.operacaoRepository = operacaoRepository;
        }

        [HttpGet]
        [Route("operacao")]
        public async Task<IActionResult> GetOperacao([FromQuery] Operacao operacao)
        {
            var resultado = await operacaoRepository.Listar(operacao);
            return Ok(resultado);
        }

        [HttpPost]
        [Route("operacao")]
        public async Task<IActionResult> PostOperacao([FromBody] Operacao operacao)
        {
            var resultado = await operacaoFacade.Inserir(operacao);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("operacao")]
        public async Task<IActionResult> PutOperacao([FromBody] Operacao operacao)
        {
            var resultado = await operacaoFacade.Alterar(operacao);
            return Ok(resultado);
        }

        [HttpDelete]
        [Route("operacao/{id}")]
        public async Task<IActionResult> DeleteOperacao([FromRoute] int id)
        {
            await operacaoFacade.Excluir(id);
            return Ok();
        }
    }
}
