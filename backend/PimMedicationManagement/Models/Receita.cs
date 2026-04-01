namespace PimMedicationManagement.Models
{
    public class Receita
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public string CaminhoArquivo { get; set; } = string.Empty;
        public DateTime DataEnvio { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pendente"; 
        public string ObservacoesFarmaceutico { get; set; } = string.Empty;
    }
}