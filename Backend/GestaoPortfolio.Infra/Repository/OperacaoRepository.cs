using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GestaoPortfolio.Infra.Repository
{
    public class OperacaoRepository : BaseRepository<Operacao>, IOperacaoRepository
    {
        public OperacaoRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Operacao>> ListarExtrato(Operacao operacao)
        {
            var linq = from e in _entities select e;
            IEnumerable<ExtratoProduto> extrato = new List<ExtratoProduto>();
            IEnumerable<Operacao> resultado = new List<Operacao>();

            if (operacao.IdCliente > 0)
            {
                linq = linq.Where(x => x.IdCliente == operacao.IdCliente);
            }
            else if (operacao.CodigoProduto > 0) {
                linq = linq.Where(x => x.CodigoProduto == operacao.CodigoProduto);
            }
            else
            {
                resultado = await GetAll();
                return resultado;
            }

            return await linq.ToListAsync();
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
