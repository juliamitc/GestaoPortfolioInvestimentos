using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GestaoPortfolio.Domain.Models
{
    [Table("POSICAO_CLIENTE")]
    public class Carteira : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        [Column("codigo_posicao_cliente")]
        public int Id { get; set; }
        [Column("codigo_cliente")]
        public int IdCliente { get; set; }
        [Column("nome_cliente")]
        public string NomeCliente { get; set; }
        [Column("codigo_operacao")]
        public int IdOperacao { get; set; }
        [Column("codigo_oferta")]
        public int CodigoOferta { get; set; }
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

