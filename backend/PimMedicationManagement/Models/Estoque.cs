using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimMedicationManagement.Models
{
    public class Estoque
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MedicamentoId { get; set; }

        [ForeignKey("MedicamentoId")]
        public Medicamento? Medicamento { get; set; }

        [Required]
        public int QuantidadeDisponivel { get; set; }

        [Required]
        public int NivelMinimoAlerta { get; set; }
    }
}