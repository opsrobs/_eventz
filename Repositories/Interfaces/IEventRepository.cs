using eventz.Models;

namespace eventz.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> GetEventById(Guid eventId);
        Task<Event> Create(Event newEvent);
        Task<Event> Update(Event updatedEvent, Guid id);
        Task<List<Event>> GetAll();
        Task<bool> Delete(Guid eventId);
    }
}
