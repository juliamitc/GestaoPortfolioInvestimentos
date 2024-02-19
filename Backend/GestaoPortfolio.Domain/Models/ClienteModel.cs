using GestaoPortfolio.Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Models
{
    public class Cliente : BaseEntity
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Conta { get; set; }
        public string? Agencia { get; set; }
        public PessoaEnum? TipoPessoa { get; set; }
        public string? DocumentoIdentificacao { get; set; }        
    }   
}
