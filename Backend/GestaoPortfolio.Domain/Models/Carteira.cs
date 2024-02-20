using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Models
{
    [Table("POSICAO_CLIENTE")]
    public class Carteira : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        [Column("id_posicao")]
        public int Id { get; set; }
        [Column("id_cliente")]
        public int IdCliente { get; set; }
        [Column("nome_cliente")]
        public string NomeCliente { get; set; }
        [Column("id_operacao")]
        public int IdOperacao { get; set; }
        [Column("codigo_produto")]
        public int CodigoProduto { get; set; }
        [Column("nome_papel")]
        public string Papel { get; set; }
        [Column("quantidade")]
        public int Quantidade { get; set; }
        [Column("valor_preco_unitario")]
        public double PrecoUnitario { get; set; }
        [Column("valor_total_operacao")]
        public double ValorTotalOperacao { get; set; }
        [Column("data_vencimento")]
        public DateTime DataVencimento { get; set; } 
    }
}

