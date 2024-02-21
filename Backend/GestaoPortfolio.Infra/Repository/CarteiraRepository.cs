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
            else
            {
                await GetAll();
            }

            return await linq.ToListAsync();
        }
        public Carteira Carteira { get; set; }
        public async Task<IEnumerable<Carteira>> ListarVencimentos(DateTime dataVencimento)
        {
            var linq = from e in _entities
                       where e.DataVencimento.Date == dataVencimento.Date
                       select e;

            return await linq.ToListAsync();
        }

        public Carteira BuscarCarteiraCliente(int idOferta, int idCliente)
        {
            Carteira carteira = new Carteira();

            var linq = from e in _entities select e;
            linq = linq.Where(x => x.IdCliente == idCliente && x.CodigoOferta == idOferta);
            carteira = linq.FirstOrDefault();
            return carteira;
        }
    }
}
