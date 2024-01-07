using eventz.Data;
using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace eventz.Repositories
{
    public class EventRepository : IEventRepository
    {
        private List<Event> _events;
        private readonly EventzDbContext _dbContext;

        public EventRepository(EventzDbContext eventzDbContext)
        {
            _dbContext = eventzDbContext;
            _events = new List<Event>();
        }

        public async Task<Event> GetEventById(Guid eventId)
        {
            return await _dbContext.Event.FirstOrDefaultAsync(e => e.Id == eventId);
        }

        public async Task<List<Event>> GetAll()
        {
            return await _dbContext.Event
                .Where(x => x.ThisLocalization.Id == x.ThisLocalizationId)
                .Include(u => u.ThisLocalization).ToListAsync();
                //.Include(u => u.Person).ToListAsync();
        }

        public async Task<Event> Create(Event newEvent)
        {
            await _dbContext.Event.AddAsync(newEvent);
            await _dbContext.SaveChangesAsync();
            return newEvent;
        }

        public async Task<Event>Update(Event updatedEvent, Guid id)
        {
            Event eventId = await GetEventById(id);
            if (eventId == null)
            {
                throw new InvalidOperationException("Event {id} not found");
            }

            eventId.Name = updatedEvent.Name;
            eventId.ImageUrl = updatedEvent.ImageUrl;

            _dbContext.Event.Update(eventId);
            await _dbContext.SaveChangesAsync();

            return eventId;
        }

        public async Task<bool> Delete(Guid id)
        {
            Event eventId = await GetEventById(id);

            if(eventId == null)
            {
                throw new InvalidOperationException($"Unable to delete event {id}");
            }

            _dbContext.Event.Remove(eventId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

