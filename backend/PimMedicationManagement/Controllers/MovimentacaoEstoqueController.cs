using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PimMedicationManagement.Data;
using PimMedicationManagement.Models;

namespace PimMedicationManagement.Controllers
{
    // RF07 – Registro de Movimentação de Estoque
    // Este controller permite consultar todo o histórico de entradas e saídas
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoEstoqueController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovimentacaoEstoqueController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MovimentacaoEstoque – Lista todas as movimentações (mais recentes primeiro)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimentacaoEstoque>>> GetMovimentacoes()
        {
            return await _context.MovimentacoesEstoque
                .Include(m => m.Medicamento)
                .OrderByDescending(m => m.DataMovimentacao)
                .ToListAsync();
        }

        // GET: api/MovimentacaoEstoque/Medicamento/5 – Histórico de um medicamento específico
        [HttpGet("Medicamento/{medicamentoId}")]
        public async Task<ActionResult<IEnumerable<MovimentacaoEstoque>>> GetPorMedicamento(int medicamentoId)
        {
            return await _context.MovimentacoesEstoque
                .Include(m => m.Medicamento)
                .Where(m => m.MedicamentoId == medicamentoId)
                .OrderByDescending(m => m.DataMovimentacao)
                .ToListAsync();
        }

        // POST: api/MovimentacaoEstoque – Registra uma entrada ou saída manual
        [HttpPost]
        public async Task<IActionResult> PostMovimentacao(MovimentacaoEstoque mov)
        {
            // Busca o registro de estoque desse medicamento
            var estoque = await _context.Estoques
                .FirstOrDefaultAsync(e => e.MedicamentoId == mov.MedicamentoId);

            if (estoque == null)
                return BadRequest(new { mensagem = "Não existe registro de estoque para este medicamento." });

            // Atualiza a quantidade conforme o tipo de movimentação
            if (mov.Tipo == "Entrada")
            {
                estoque.QuantidadeDisponivel += mov.Quantidade;
            }
            else if (mov.Tipo == "Saida")
            {
                if (estoque.QuantidadeDisponivel < mov.Quantidade)
                    return BadRequest(new { mensagem = "Estoque insuficiente para esta saída." });

                estoque.QuantidadeDisponivel -= mov.Quantidade;
            }
            else
            {
                return BadRequest(new { mensagem = "Tipo deve ser 'Entrada' ou 'Saida'." });
            }

            mov.DataMovimentacao = DateTime.Now;

            _context.MovimentacoesEstoque.Add(mov);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Movimentação registrada!", estoqueAtual = estoque.QuantidadeDisponivel });
        }
    }
}
