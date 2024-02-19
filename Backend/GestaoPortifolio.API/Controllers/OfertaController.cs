using GestaoPortfolio.Domain.Kafka;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestaoPortifolio.API.Controllers
{
    [Authorize]
    public class OfertaController : BaseController
    {
        private readonly IProdutorKafka produtorKafka;

        public OfertaController(IProdutorKafka produtor, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            produtorKafka = produtor;
        }

        [HttpPost]
        public async Task<IActionResult> Contratar([FromBody] Oferta oferta)
        {
            await produtorKafka.ProduzirMensagem("ORDEM_COMPRA", userId?.ToString(), JsonConvert.SerializeObject(oferta));

            return Ok();
        }
    }
}
