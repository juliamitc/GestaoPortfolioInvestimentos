using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Application.Services
{
    public class AlterarPosicaoService : IAlterarPosicaoService
    {
        private readonly ICarteiraRepository carteiraRepository;

        public AlterarPosicaoService(ICarteiraRepository carteiraRepository)
        {
            this.carteiraRepository = carteiraRepository;
        }

        public async Task<Carteira> AlterarPosicao(Carteira carteira)
        {
            return await carteiraRepository.Update(carteira);
        }
    }
}
