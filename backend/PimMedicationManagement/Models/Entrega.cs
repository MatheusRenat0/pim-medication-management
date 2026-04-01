using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimMedicationManagement.Models
{
    public class Entrega
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PedidoId { get; set; }

        [ForeignKey("PedidoId")]
        public Pedido? Pedido { get; set; }

        [Required]
        public int EntregadorId { get; set; }

        [ForeignKey("EntregadorId")]
        public Usuario? Entregador { get; set; }

        [Required]
        [StringLength(255)]
        public string EnderecoDestino { get; set; } = string.Empty;

        [Required]
        public DateTime DataPrevista { get; set; }

        public DateTime? DataRealizada { get; set; }

        [Required]
        public string StatusLogistico { get; set; } = "Pendente";
    }
}