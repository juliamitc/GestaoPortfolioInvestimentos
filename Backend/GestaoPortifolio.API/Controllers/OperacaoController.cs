
using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using static GestaoPortfolio.Domain.Models.Enum.EventoEnum;
using static GestaoPortfolio.Domain.Models.Enum.StatusEnum;

namespace GestaoPortifolio.API.Controllers
{
    public class OperacaoController : BaseController
    {
        private readonly IOperacaoFacade operacaoFacade;
        private readonly IOperacaoRepository operacaoRepository;

        public OperacaoController(IOperacaoFacade operacaoFacade, IOperacaoRepository operacaoRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.operacaoFacade = operacaoFacade;
            this.operacaoRepository = operacaoRepository;
        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> GetOperacao([FromQuery] Operacao operacao)
        {
            var resultado = await operacaoRepository.Listar(operacao);
            return Ok(resultado);
        }

        [HttpPost]
        [Route("incluir")]
        public async Task<IActionResult> PostOperacao([FromBody] Operacao operacao)
        {
            var resultado = await operacaoFacade.Inserir(operacao);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("alterar")]
        public async Task<IActionResult> PutOperacao([FromBody] Operacao operacao)
        {
            var resultado = await operacaoFacade.Alterar(operacao);
            return Ok(resultado);
        }

        [HttpDelete]
        [Route("excluir/{id}")]
        public async Task<IActionResult> DeleteOperacao([FromRoute] int id)
        {
            await operacaoFacade.Excluir(id);
            return Ok();
        }

        [HttpPost]
        [Route("incluir/venda")]
        public async Task<IActionResult> PostOperacaoVenda([FromBody] Operacao operacao)
        {
            operacao.Evento = Evento.Venda;
            operacao.DataOperacao = DateTime.Now;
            operacao.Status = Status.Resgatada;
            var resultado = await operacaoFacade.Inserir(operacao);
            return Ok(resultado);
        }


        [HttpPost]
        [Route("incluir/compra")]
        public async Task<IActionResult> PostOperacaoCompra([FromBody] Operacao operacao)
        {
            operacao.Evento = Evento.Compra;
            operacao.DataOperacao = DateTime.Now;
            operacao.Status = Status.Gravada;
            var resultado = await operacaoFacade.Inserir(operacao);
            return Ok(resultado);
        }
    }
}
