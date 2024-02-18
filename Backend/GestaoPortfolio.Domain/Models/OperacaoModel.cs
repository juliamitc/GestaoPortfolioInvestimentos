using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Models
{
    public class Operacao : BaseEntity
    {
        public int IdCliente { get; set; }
        public int IdOperacao { get; set; }
        public string? Status { get; set; }
        public string? Produto { get; set; }
        public double ValorTotalOperacao { get; set; }
        public string? Evento { get; set; }
        public DateTime DataVencimentoOperacao { get; set; }

    }
}
