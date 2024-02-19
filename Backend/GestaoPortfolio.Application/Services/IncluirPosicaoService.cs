using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Application.Services
{
    public class IncluirPosicaoService : IIncluirPosicaoService
    {
        private readonly ICarteiraRepository carteiraRepository;

        public IncluirPosicaoService(ICarteiraRepository carteiraRepository)
        {
            this.carteiraRepository = carteiraRepository;
        }

        public async Task<Carteira> IncluirPosicao(Carteira carteira)
        {
            await carteiraRepository.Insert(carteira);

            return carteira;
        }
    }
}
