using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimMedicationManagement.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario? Cliente { get; set; }

        [Required]
        public DateTime DataPedido { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = "Pendente";

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }
    }
}