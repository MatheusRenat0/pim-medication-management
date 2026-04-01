using Microsoft.EntityFrameworkCore;
using PimMedicationManagement.Models;

namespace PimMedicationManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Entrega> Entregas { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Tratamento> Tratamentos { get; set; }
        public DbSet<Sache> Saches { get; set; }
    }
}