using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimMedicationManagement.Models
{
    // RF07 – Registro de Movimentação de Estoque
    // Cada vez que o estoque muda (entrada ou saída), gravamos aqui o histórico completo.
    public class MovimentacaoEstoque
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MedicamentoId { get; set; }

        [ForeignKey("MedicamentoId")]
        public Medicamento? Medicamento { get; set; }

        // "Entrada" ou "Saida"
        [Required]
        [StringLength(20)]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public DateTime DataMovimentacao { get; set; } = DateTime.Now;

        // Quem ou o que causou essa movimentação (ex: "Pedido #12", "Reposição manual", "Tratamento #5")
        [StringLength(255)]
        public string Observacao { get; set; } = string.Empty;
    }
}
