namespace PimMedicationManagement.DTOs
{
    public class TratamentoCreateDto
    {
        public int UsuarioId { get; set; }
        public int ReceitaId { get; set; }
        public List<SacheDto> Saches { get; set; } = new();
    }

    public class SacheDto
    {
        public int MedicamentoId { get; set; }
        public string Horario { get; set; } = string.Empty;
        public int QuantidadeComprimidos { get; set; }
    }
}