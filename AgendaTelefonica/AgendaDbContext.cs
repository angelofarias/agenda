using Microsoft.EntityFrameworkCore;
using AgendaTelefonica.Models;

namespace AgendaTelefonica
{
    public class AgendaDbContext : DbContext
    {
        public DbSet<Agenda> Contatos => Set<Agenda>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=c:\\sqlite\\db\\agenda.db");
        }
    }
}
