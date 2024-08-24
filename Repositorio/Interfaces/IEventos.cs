using Calendario.Models;

namespace Calendario.Repositorio.Interfaces
{
    public interface IEventos
    {

        Task<IEnumerable<Eventos>> GetAllEventosAsync();
        Task CreateEventoAsync(Eventos evento);

        Task UpdateEventoAsync(Eventos evento);

        Task<Eventos> GetEventoByIdAsync(int id);
        Task DeleteEventoAsync(int id);
    


    }
}
