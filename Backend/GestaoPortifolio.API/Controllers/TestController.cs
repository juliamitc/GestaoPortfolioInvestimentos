using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolio.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(Ok("Olá mundo"));
        }
    }
}
