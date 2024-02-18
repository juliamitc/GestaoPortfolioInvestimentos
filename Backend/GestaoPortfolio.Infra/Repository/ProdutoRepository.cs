using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Repository
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<Produto>> Listar(Produto produto)
        {
            var linq = from e in _entities select e;

            if (produto.CodigoProduto > 0)
            {
                linq = linq.Where(x => x.CodigoProduto == produto.CodigoProduto);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(produto.NomeProduto))
                {
                    linq = linq.Where(x => produto.NomeProduto == x.NomeProduto);
                }

                if (!string.IsNullOrWhiteSpace(produto.DescricaoProduto))
                {
                    linq = linq.Where(x => produto.DescricaoProduto == x.DescricaoProduto);
                }
            }

            return await linq.ToListAsync();
        }
    }
}
