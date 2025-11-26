using Microsoft.AspNetCore.Mvc;
using AgendaTelefonica.Repositories;
using AgendaTelefonica.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace AgendaTelefonica.Controllers
{
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaRepository agendaRepository;
        public AgendaController(IAgendaRepository agendaRepository)
            => this.agendaRepository = agendaRepository;

        [Route("/api/contatos")] // GET
        public IActionResult GetAllContatos()
        {
            return Ok(agendaRepository.GetAll());
        }

        [Route("/api/contatos/{id}")] // GET Id
        public IActionResult GetContatoById(int id)
        {
            var agenda = agendaRepository.GetById(id);
            if (agenda == null)
                return NotFound();

            return Ok(agenda);
        }

        [Route("/api/contatos/nomes")] // GET Nome
        public IActionResult GetContatoByName([FromQuery] string nome)
        {
                      
            var resultado = agendaRepository.GetAll().Where(name => name.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToArray();
            if (!resultado.Any())
                return NotFound();

            var array = new[] { resultado };
            return Ok(resultado);
        }

        [HttpPost("/api/contatos")] // POST
        // Model Binding
        public IActionResult CreateAgenda([FromBody] Agenda agenda)
        {

           if (agenda.Nome == null || agenda.Telefone == null)
                return BadRequest();

           var contatoAgenda = agendaRepository.GetById(agenda.Id);
            if (contatoAgenda != null)
                return Conflict(contatoAgenda);
           
           var mesmoNome = agendaRepository.GetAll().FirstOrDefault(name => name.Nome.Contains(agenda.Nome, StringComparison.OrdinalIgnoreCase));
            if (mesmoNome != null)
                return Conflict(mesmoNome);

           agenda = agendaRepository.Create(agenda);
           return Created();
        }

        [HttpPut("/api/contatos/{id}")] // PUT
        // Model Binding
        public IActionResult CreateAgenda([FromBody] Agenda agenda, int id)
        {
            if (id != agenda.Id)
                return BadRequest();

            var contatoAgenda = agendaRepository.GetById(agenda.Id);
            if (contatoAgenda == null)
                return NotFound();

            var mesmoNome = agendaRepository.GetAll().FirstOrDefault(name => name.Nome.Contains(agenda.Nome, StringComparison.OrdinalIgnoreCase));
            if (mesmoNome != null)
                return Conflict(mesmoNome);

            var retorno = agendaRepository.Update(contatoAgenda, agenda);
            return Ok(retorno);
        }

        [HttpDelete("/api/contatos/{id}")]
        public IActionResult Delete(int id)
        {
            var contatoAgenda = agendaRepository.GetById(id);
            if (contatoAgenda == null)
                return NotFound();

            agendaRepository.Delete(id);
            return NoContent();
        }
    }
}
