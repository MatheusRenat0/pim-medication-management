namespace PimMedicationManagement.Models
{
    public class Tratamento
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int ReceitaId { get; set; }
        public Receita? Receita { get; set; }

        public DateTime DataInicio { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;
        
        // Relacionamento: Um tratamento tem vários sachês configurados
        public List<Sache> Saches { get; set; } = new List<Sache>();
    }
}