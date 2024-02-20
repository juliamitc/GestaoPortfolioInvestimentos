using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static GestaoPortfolio.Domain.Models.Enum.EventoEnum;
using static GestaoPortfolio.Domain.Models.Enum.StatusEnum;

namespace GestaoPortfolio.Domain.Models
{
    [Table("OPERACAO")]
    public class Operacao : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_operacao")]
        [JsonIgnore]
        public int IdOperacao { get; set; }
        [Column("codigo_oferta")]
        public int CodigoOferta { get; set; } 
        [Column("tipo_evento")]
        [JsonIgnore]
        public Evento Evento { get; set; }
        [Column("quantidade_operacao")]
        public int QuantidadeOperacao { get; set; }
        [Column("quantidade_disponivel_estoque")]
        public int QuantidadeDisponivelEstoque { get; set; }
        [Column("valor_preco_unitario")]
        public double ValorPrecoUnitario { get; set; }
        [Column("valor_total_operacao")]
        [JsonIgnore]
        public double ValorTotalOperacao { get; set; }
        [Column("status")]
        [JsonIgnore]
        public Status Status { get; set; }
        [Column("data_operacao")]
        [JsonIgnore]
        public DateTime? DataOperacao { get; set; }
        [Column("id_cliente")]
        public int IdCliente { get; set; }

    }
}
