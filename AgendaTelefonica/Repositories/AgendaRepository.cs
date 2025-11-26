using AgendaTelefonica.Models;

namespace AgendaTelefonica.Repositories
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly AgendaDbContext dbContext;

        public AgendaRepository(AgendaDbContext dbContext) { 
            this.dbContext = dbContext;
        }
        public IEnumerable<Agenda> GetAll()
        {
            return dbContext.Contatos;
        }

        public Agenda GetById(int id)
        {
            return dbContext.Contatos.Find(id);
        }
        public Agenda Create(Agenda agenda)
        {
            dbContext.Contatos.Add(agenda);
            dbContext.SaveChanges();
            
            return agenda;
        }
        public bool Update(Agenda existingAgenda, Agenda agenda)
        {
            existingAgenda.Nome = agenda.Nome;
            existingAgenda.Telefone = agenda.Telefone;
            dbContext.Contatos.Update(existingAgenda);
            dbContext.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var agenda = dbContext.Contatos.Find(id);
            if (agenda != null)
            {
                dbContext.Contatos.Remove(agenda);
                dbContext.SaveChanges();
            }
            return true;
        }
    }
}
