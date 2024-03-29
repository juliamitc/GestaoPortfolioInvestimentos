﻿using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolio.API.Controllers
{
    public class CarteiraController : BaseController
    {
        private readonly ICarteiraFacade carteiraFacade;
        private readonly ICarteiraRepository carteiraRepository;

        public CarteiraController(ICarteiraFacade carteiraFacade, ICarteiraRepository carteiraRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.carteiraFacade = carteiraFacade;
            this.carteiraRepository = carteiraRepository;
        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> GetCarteira([FromQuery] Carteira carteira)
        {
            var resultado = await carteiraRepository.Listar(carteira);
            return Ok(resultado);
        }

        [HttpPost]
        [Route("incluir")]
        public async Task<IActionResult> PostCarteira([FromBody] Carteira carteira)
        {
            var resultado = await carteiraFacade.IncluirPosicao(carteira);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("alterar")]
        public async Task<IActionResult> PutCarteira([FromBody] Carteira carteira)
        {
            var resultado = await carteiraFacade.AlterarPosicao(carteira);
            return Ok(resultado);
        }

        [HttpDelete]
        [Route("excluir/{idPosicao}")]
        public async Task<IActionResult> DeleteCarteira([FromRoute] int idPosicao)
        {
            await carteiraFacade.ExcluirPosicao(idPosicao);
            return Ok();
        }
    }
}
