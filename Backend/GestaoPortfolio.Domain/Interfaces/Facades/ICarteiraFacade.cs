using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Domain.Interfaces.Facades
{
    public interface ICarteiraFacade
    {
        Task<Carteira> IncluirPosicao(Carteira carteira);
        Task<Carteira> AlterarPosicao(Carteira carteira);
        Task ExcluirPosicao(int id);
    }
}
