using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PimMedicationManagement.Data;
using PimMedicationManagement.Models;

namespace PimMedicationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstoqueController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estoque>>> GetEstoques()
        {
            return await _context.Estoques.Include(e => e.Medicamento).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estoque>> GetEstoque(int id)
        {
            var estoque = await _context.Estoques.Include(e => e.Medicamento).FirstOrDefaultAsync(e => e.Id == id);
            if (estoque == null) return NotFound();
            return estoque;
        }

        [HttpPost]
        public async Task<ActionResult<Estoque>> PostEstoque(Estoque estoque)
        {
            _context.Estoques.Add(estoque);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEstoque), new { id = estoque.Id }, estoque);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstoque(int id, Estoque estoque)
        {
            if (id != estoque.Id) return BadRequest();
            _context.Entry(estoque).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Estoques.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstoque(int id)
        {
            var estoque = await _context.Estoques.FindAsync(id);
            if (estoque == null) return NotFound();
            _context.Estoques.Remove(estoque);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
