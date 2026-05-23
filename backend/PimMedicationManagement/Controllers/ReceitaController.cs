using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PimMedicationManagement.Data;
using PimMedicationManagement.Models;

namespace PimMedicationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ReceitaController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // POST: api/Receita/Upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadReceita([FromForm] int usuarioId, IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            // 1. Definir onde salvar (Pasta wwwroot/uploads)
            string pastaUploads = Path.Combine(_environment.ContentRootPath, "wwwroot", "uploads");
            
            if (!Directory.Exists(pastaUploads))
                Directory.CreateDirectory(pastaUploads);

            // 2. Gerar nome único para o arquivo (evita sobrepor fotos com mesmo nome)
            string nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(arquivo.FileName)}";
            string caminhoCompleto = Path.Combine(pastaUploads, nomeArquivo);

            // 3. Salvar o arquivo físico no Windows
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            // 4. Salvar o registro no Banco de Dados MySQL
            var novaReceita = new Receita
            {
                UsuarioId = usuarioId,
                CaminhoArquivo = $"/uploads/{nomeArquivo}",
                Status = "Pendente",
                DataEnvio = DateTime.Now
            };

            _context.Receitas.Add(novaReceita);
            await _context.SaveChangesAsync();

            return Ok(new { 
                message = "Receita enviada com sucesso!", 
                receitaId = novaReceita.Id,
                url = novaReceita.CaminhoArquivo 
            });
        }

        // GET: api/Receita/Pendentes (Para o Farmacêutico ver o que precisa aprovar)
        [HttpGet("pendentes")]
        public async Task<ActionResult<IEnumerable<Receita>>> GetReceitasPendentes()
        {
            return await _context.Receitas
                .Where(r => r.Status == "Pendente")
                .Include(r => r.Usuario)
                .ToListAsync();
        }

        // GET: api/Receita/usuario/5 (Receitas de um paciente específico)
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Receita>>> GetReceitasPorUsuario(int usuarioId)
        {
            return await _context.Receitas
                .Where(r => r.UsuarioId == usuarioId)
                .Include(r => r.Usuario)
                .OrderByDescending(r => r.DataEnvio)
                .ToListAsync();
        }

        // PUT: api/Receita/Analise/5 (Farmacêutico aprova ou recusa)
        [HttpPut("analise/{id}")]
        public async Task<IActionResult> AnalisarReceita(int id, [FromBody] string novoStatus)
        {
            var receita = await _context.Receitas.FindAsync(id);
            if (receita == null) return NotFound();

            receita.Status = novoStatus; // "Aprovada" ou "Recusada"
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}