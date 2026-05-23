using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PimMedicationManagement.Data;
using PimMedicationManagement.Models;
using PimMedicationManagement.DTOs;

namespace PimMedicationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // RF03 – Método auxiliar para verificar o perfil de quem está fazendo a requisição
        private async Task<Usuario?> GetUsuarioLogado()
        {
            // O frontend envia o ID do usuário logado no header "X-User-Id"
            if (!Request.Headers.TryGetValue("X-User-Id", out var userIdHeader))
                return null;

            if (!int.TryParse(userIdHeader, out var userId))
                return null;

            return await _context.Usuarios.FindAsync(userId);
        }

        // ── Endpoints existentes (preservados) ──

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioCadastroDTO dto)
        {
            var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);
            if (usuarioExiste)
            {
                return BadRequest(new { mensagem = "Este e-mail já está em uso!" });
            }

            var novoUsuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Cpf = dto.Cpf,
                SenhaHash = dto.Senha,
                Tipo = "Paciente",
                DataCadastro = DateTime.Now
            };

            _context.Usuarios.Add(novoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Cadastro realizado com sucesso!", usuarioId = novoUsuario.Id, nome = novoUsuario.Nome, tipo = novoUsuario.Tipo });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.SenhaHash == dto.Senha);

            if (usuario == null)
            {
                return Unauthorized(new { mensagem = "E-mail ou senha incorretos." });
            }

            return Ok(new {
                mensagem = "Login aprovado!",
                usuarioId = usuario.Id,
                nome = usuario.Nome,
                tipo = usuario.Tipo
            });
        }

        // Endpoint para criar usuários de teste (Admin, Funcionário, Paciente)
        [HttpPost("seed")]
        public async Task<IActionResult> Seed()
        {
            if (await _context.Usuarios.AnyAsync())
                return BadRequest(new { mensagem = "Já existem usuários cadastrados. Seed cancelado." });

            var usuarios = new List<Usuario>
            {
                new Usuario { Nome = "Admin MedFlow", Email = "admin@medflow.com", Cpf = "111.111.111-11", SenhaHash = "admin123", Tipo = "Administrador", DataCadastro = DateTime.Now },
                new Usuario { Nome = "Carlos Funcionário", Email = "func@medflow.com", Cpf = "222.222.222-22", SenhaHash = "func123", Tipo = "Funcionario", DataCadastro = DateTime.Now },
                new Usuario { Nome = "Maria Paciente", Email = "paciente@medflow.com", Cpf = "333.333.333-33", SenhaHash = "pac123", Tipo = "Paciente", DataCadastro = DateTime.Now }
            };

            _context.Usuarios.AddRange(usuarios);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Usuários de teste criados com sucesso!", usuarios = usuarios.Select(u => new { u.Email, Senha = u.SenhaHash, u.Tipo }) });
        }

        // ── Novos endpoints CRUD ──
        // RF03 – Listar todos os usuários: somente Administradores
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var logado = await GetUsuarioLogado();
            if (logado == null || logado.Tipo != "Administrador")
                return StatusCode(403, new { mensagem = "Acesso negado. Somente administradores podem listar usuários." });

            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            return usuario;
        }

        // RF03 – Editar usuário: somente Administradores
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            var logado = await GetUsuarioLogado();
            if (logado == null || logado.Tipo != "Administrador")
                return StatusCode(403, new { mensagem = "Acesso negado. Somente administradores podem editar usuários." });

            if (id != usuario.Id) return BadRequest();
            _context.Entry(usuario).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(u => u.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        // RF03 – Excluir usuário: somente Administradores
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var logado = await GetUsuarioLogado();
            if (logado == null || logado.Tipo != "Administrador")
                return StatusCode(403, new { mensagem = "Acesso negado. Somente administradores podem excluir usuários." });

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}