using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PimMedicationManagement.Data;
using PimMedicationManagement.Models;

namespace PimMedicationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EntregaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entrega>>> GetEntregas()
        {
            return await _context.Entregas
                .Include(e => e.Pedido)
                .Include(e => e.Entregador)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entrega>> GetEntrega(int id)
        {
            var entrega = await _context.Entregas
                .Include(e => e.Pedido)
                .Include(e => e.Entregador)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (entrega == null) return NotFound();
            return entrega;
        }

        [HttpPost]
        public async Task<ActionResult<Entrega>> PostEntrega(Entrega entrega)
        {
            _context.Entregas.Add(entrega);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEntrega), new { id = entrega.Id }, entrega);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntrega(int id, Entrega entrega)
        {
            if (id != entrega.Id) return BadRequest();
            _context.Entry(entrega).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Entregas.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] string novoStatus)
        {
            var entrega = await _context.Entregas.FindAsync(id);
            if (entrega == null) return NotFound();

            entrega.StatusLogistico = novoStatus;
            if (novoStatus == "Entregue")
                entrega.DataRealizada = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Status atualizado!", status = novoStatus });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrega(int id)
        {
            var entrega = await _context.Entregas.FindAsync(id);
            if (entrega == null) return NotFound();
            _context.Entregas.Remove(entrega);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
