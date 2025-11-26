using System.Collections.Generic;
using AgendaTelefonica.Models;

namespace AgendaTelefonica.Repositories
{
    public interface IAgendaRepository
    {
        IEnumerable<Agenda> GetAll();
        Agenda GetById(int id);
        Agenda Create(Agenda agenda);
        bool Update(Agenda existingAgenda,Agenda agenda);
        bool Delete(int id);


    }
}
