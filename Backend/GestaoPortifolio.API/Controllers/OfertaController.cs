using GestaoPortfolio.Application.Facades;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Kafka;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GestaoPortfolio.Infra.Repository;

namespace GestaoPortifolio.API.Controllers
{
    [Authorize]
    public class OfertaController : BaseController
    {
        private readonly IProdutorKafka produtorKafka;
        private readonly IOfertaFacade ofertaFacade;
        private readonly IOfertaRepository ofertaRepository;

        public OfertaController(IProdutorKafka produtor, IOfertaFacade ofertaFacade, IOfertaRepository ofertaRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            produtorKafka = produtor;
            this.ofertaFacade = ofertaFacade;
            this.ofertaRepository = ofertaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Contratar([FromBody] Oferta oferta)
        {
            oferta.DataUltimaAtualizacao = DateTime.Now;
            oferta.DataInsercao = DateTime.Now;
            await produtorKafka.ProduzirMensagem("ORDEM_COMPRA", userId?.ToString(), JsonConvert.SerializeObject(oferta));

            return Ok();
        }

        [HttpPost]
        [Route("criar")]
        public async Task<IActionResult> PostOferta([FromBody] Oferta oferta)
        {
            oferta.DataInsercao = DateTime.Now;
            oferta.DataUltimaAtualizacao = DateTime.Now;
            var resultado = await ofertaFacade.Inserir(oferta);
            return Ok(resultado);
        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> GetOferta([FromQuery] Oferta oferta)
        {
            var resultado = await ofertaRepository.Listar(oferta);
            return Ok(resultado);
        }
    }
}
