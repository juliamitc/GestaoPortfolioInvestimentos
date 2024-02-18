using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Application.Services
{
    public class IncluirProdutoService : IIncluirProdutoService
    {
        private readonly IProdutoRepository produtoRepository;

        public IncluirProdutoService(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        public async Task<Produto> Incluir(Produto produto)
        {
            await produtoRepository.Insert(produto);

            return produto;
        }
    }
}
