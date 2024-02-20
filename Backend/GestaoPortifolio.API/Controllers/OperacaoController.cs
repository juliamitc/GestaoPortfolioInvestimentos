
using GestaoPortfolio.Application.Kafka;
using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Kafka;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static GestaoPortfolio.Domain.Models.Enum.EventoEnum;
using static GestaoPortfolio.Domain.Models.Enum.StatusEnum;

namespace GestaoPortifolio.API.Controllers
{
    public class OperacaoController : BaseController
    {
        private readonly IOperacaoFacade operacaoFacade;
        private readonly IOperacaoRepository operacaoRepository;
        private readonly IProdutorKafka produtorKafka;
        private readonly IAtualizarCarteiraEstoqueService atualizarCarteiraEstoque;
        public OperacaoController(IProdutorKafka produtor, IOperacaoFacade operacaoFacade, IOperacaoRepository operacaoRepository, IHttpContextAccessor httpContextAccessor, IAtualizarCarteiraEstoqueService atualizarCarteiraEstoqueService) : base(httpContextAccessor)
        {
            produtorKafka = produtor;
            this.operacaoFacade = operacaoFacade;
            this.operacaoRepository = operacaoRepository;
            this.atualizarCarteiraEstoque = atualizarCarteiraEstoqueService;
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

        [HttpPost]
        [Route("incluir/venda")]
        public async Task<IActionResult> PostOperacaoVenda([FromBody] Operacao operacao)
        {
            operacao.Evento = Evento.Venda;
            operacao.DataOperacao = DateTime.Now;
            operacao.Status = Status.Gravada;

            await produtorKafka.ProduzirMensagem("ORDEM_VENDA", userId?.ToString(), JsonConvert.SerializeObject(operacao));
            var resultado = await atualizarCarteiraEstoque.TratarOperacao(operacao);
            return Ok(resultado);
        }


        [HttpPost]
        [Route("incluir/compra")]
        public async Task<IActionResult> PostOperacaoCompra([FromBody] Operacao operacao)
        {
            operacao.Evento = Evento.Compra;
            operacao.DataOperacao = DateTime.Now;
            operacao.Status = Status.Gravada;

            await produtorKafka.ProduzirMensagem("ORDEM_COMPRA", userId?.ToString(), JsonConvert.SerializeObject(operacao));
            var resultado = await atualizarCarteiraEstoque.TratarOperacao(operacao);
            return Ok(resultado);
        }
    }
}
