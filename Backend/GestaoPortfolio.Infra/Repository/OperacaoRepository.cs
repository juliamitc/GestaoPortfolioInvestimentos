using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Repository
{
    public class OperacaoRepository : BaseRepository<Operacao>, IOperacaoRepository
    {
        public OperacaoRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Operacao>> Listar(Operacao operacao)
        {
            var linq = from e in _entities select e;

            if (operacao.IdOperacao > 0)
            {
                linq = linq.Where(x => x.IdCliente == operacao.IdCliente);
            }
            else
            {
                await GetAll();
            }

            return await linq.ToListAsync();
        }
    }
}
