namespace PimMedicationManagement.Models
{
    public class Sache
    {
        public int Id { get; set; }
        public int TratamentoId { get; set; }
        public Tratamento? Tratamento { get; set; }

        public int MedicamentoId { get; set; }
        public Medicamento? Medicamento { get; set; }

        public string Horario { get; set; } = string.Empty; 
        public int QuantidadeComprimidos { get; set; } 
    }
}