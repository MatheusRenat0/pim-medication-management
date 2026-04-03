using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PimMedicationManagement.Data;
using PimMedicationManagement.Models;
using PimMedicationManagement.DTOs;

namespace PimMedicationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TratamentoController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Tratamento (Cria a Box Completa)
        [HttpPost]
        public async Task<IActionResult> PostTratamento(TratamentoCreateDto dto)
        {
            // 1. Criar o objeto principal do Tratamento
            var novoTratamento = new Tratamento
            {
                UsuarioId = dto.UsuarioId,
                ReceitaId = dto.ReceitaId,
                DataInicio = DateTime.Now,
                Ativo = true
            };

            // 2. Transformar os DTOs de Saches em Models reais e adicionar ao Tratamento
            foreach (var s in dto.Saches)
            {
                novoTratamento.Saches.Add(new Sache
                {
                    MedicamentoId = s.MedicamentoId,
                    Horario = s.Horario,
                    QuantidadeComprimidos = s.QuantidadeComprimidos
                });
            }

            _context.Tratamentos.Add(novoTratamento);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Box MedFlow configurada com sucesso!", tratamentoId = novoTratamento.Id });
        }

        // GET: api/Tratamento/Usuario/5 (Busca a Box de um paciente específico)
        [HttpGet("Usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Tratamento>>> GetTratamentosPorUsuario(int usuarioId)
        {
            return await _context.Tratamentos
                .Include(t => t.Saches)
                .ThenInclude(s => s.Medicamento)
                .Where(t => t.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}