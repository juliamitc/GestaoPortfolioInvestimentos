using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolio.API.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly IClienteFacade clienteFacade;
        private readonly IClienteRepository clienteRepository;

        public ClienteController(IClienteFacade clienteFacade, IClienteRepository clienteRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.clienteFacade = clienteFacade;
            this.clienteRepository = clienteRepository;
        }

        [HttpGet]
        [Route("cliente")]
        public async Task<IActionResult> GetCliente([FromQuery] Cliente cliente)
        {
            var resultado = await clienteRepository.Listar(cliente);
            return Ok(resultado);
        }

        [HttpPost]
        [Route("cliente")]
        public async Task<IActionResult> PostCliente([FromBody] Cliente cliente)
        {
            var resultado = await clienteFacade.Inserir(cliente);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("cliente")]
        public async Task<IActionResult> PutCliente([FromBody] Cliente cliente)
        {
            var resultado = await clienteFacade.Alterar(cliente);
            return Ok(resultado);
        }

        [HttpDelete]
        [Route("cliente/{id}")]
        public async Task<IActionResult> DeleteCliente([FromRoute] int id)
        {
            await clienteFacade.Excluir(id);
            return Ok();
        }
    }
}
