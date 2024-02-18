using GestaoPortfolio.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Carteira> Carteira { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Operacao> Operacao { get; set; }
    }
}
