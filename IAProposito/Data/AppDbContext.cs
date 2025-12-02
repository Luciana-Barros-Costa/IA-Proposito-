using Microsoft.EntityFrameworkCore;
using IAProposito.Models;

namespace IAProposito.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Mensagem> Mensagens { get; set; }
    }
}
