using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Repository
{
    public class CarteiraRepository : BaseRepository<Carteira>, ICarteiraRepository
    {
        public CarteiraRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Carteira>> Listar(Carteira carteira)
        {
            var linq = from e in _entities select e;

            if (carteira.IdCliente > 0)
            {
                linq = linq.Where(x => x.IdCliente == carteira.IdCliente);
            }

            return await linq.ToListAsync();
        }
    }
}
