using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPortfolio.Domain.Models
{
    [Table("PRODUTO")]
    public class Produto : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("cd_produto")]
        public int CodigoProduto { get; set; }
        [Column("nm_produto")]
        public string NomeProduto { get; set; }
        [Column("desc_produto")]
        public string DescricaoProduto { get; set; }
    }
}
