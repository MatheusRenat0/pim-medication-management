using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.ComponentModel.DataAnnotations;

namespace PimMedicationManagement.Models
{
    public class Medicamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string PrincipioAtivo { get; set; } = string.Empty;

        [Required]
        public string Lote { get; set; } = string.Empty;

        [Required]
        public DateTime DataFabricacao { get; set; }

        [Required]
        public DateTime DataValidade { get; set; }

        public string Tarja { get; set; } = string.Empty; 
    }
}