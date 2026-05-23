namespace PimMedicationManagement.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        public string Nome { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
        
        public string SenhaHash { get; set; } = string.Empty;
        
        // Propriedades adicionais do usuário
        public string Cpf { get; set; } = string.Empty;
        
        public string Tipo { get; set; } = string.Empty;
        
        public DateTime DataCadastro { get; set; }
    }
}