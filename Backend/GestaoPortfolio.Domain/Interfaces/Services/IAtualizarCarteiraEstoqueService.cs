using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Interfaces.Services
{
    public interface IAtualizarCarteiraEstoqueService
    {
        public Task<Operacao> TratarOperacao(Operacao operacao);
        public void AtualizarCarteiraEstoqueVenda(Oferta oferta, Carteira carteira, Operacao operacao);
        public void AtualizarCarteiraEstoqueCompra(Oferta oferta, Operacao operacao, Cliente cliente);
    }
}
