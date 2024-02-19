using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Application.Services
{
    public class ExcluirPosicaoService : IExcluirPosicaoService
    {
        private readonly ICarteiraRepository carteiraRepository;

        public ExcluirPosicaoService(ICarteiraRepository carteiraRepository)
        {
            this.carteiraRepository = carteiraRepository;
        }

        public async Task ExcluirPosicao(int id)
        {
            await carteiraRepository.Delete(id);
        }
    }
}
