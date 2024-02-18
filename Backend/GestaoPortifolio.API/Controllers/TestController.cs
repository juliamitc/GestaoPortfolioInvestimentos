using GestaoPortfolio.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolio.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class TestController : ControllerBase
    {
        private readonly IProdutoRepository produtoRepository;

        public TestController(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        [HttpGet]
        [Route("teste")]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(Ok("Olá mundo"));
        }

        [HttpGet]
        [Route("produto")]
        public async Task<IActionResult> GetProduto()
        {
            return await Task.FromResult(Ok(produtoRepository.GetAll()));
        }
    }
}
