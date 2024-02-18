using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Models
{
    public class Carteira : BaseEntity
    {
        public int IdCliente { get; set; }
        public int IdOperacao { get; set; }
        public string? NomeCliente { get; set; }
        public double SaldoOperacao { get; set; }
        public DateTime DataVencimentoOperacao { get; set; }

    }
}
