using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PimMedicationManagement.Data;

namespace PimMedicationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var stats = new
            {
                totalUsuarios = await _context.Usuarios.CountAsync(),
                totalMedicamentos = await _context.Medicamentos.CountAsync(),
                totalPedidos = await _context.Pedidos.CountAsync(),
                pedidosPendentes = await _context.Pedidos.CountAsync(p => p.Status == "Pendente"),
                totalEntregas = await _context.Entregas.CountAsync(),
                entregasPendentes = await _context.Entregas.CountAsync(e => e.StatusLogistico == "Pendente"),
                receitasPendentes = await _context.Receitas.CountAsync(r => r.Status == "Pendente"),
                estoqueBaixo = await _context.Estoques.CountAsync(e => e.QuantidadeDisponivel <= e.NivelMinimoAlerta),
                medicamentosVencidos = await _context.Medicamentos.CountAsync(m => m.DataValidade < DateTime.Now),
                medicamentosVencendo = await _context.Medicamentos.CountAsync(m => m.DataValidade >= DateTime.Now && m.DataValidade <= DateTime.Now.AddDays(30))
            };

            return Ok(stats);
        }
    }
}
