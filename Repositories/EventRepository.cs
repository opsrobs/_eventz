using AutoMapper;
using eventz.Data;
using eventz.DTOs;
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
        private readonly IMapper _mapper;

        public EventRepository(EventzDbContext eventzDbContext, IMapper mapper)
        {
            _dbContext = eventzDbContext;
            _events = new List<Event>();
            _mapper = mapper;
        }

        public async Task<Event> GetEventById(Guid eventId)
        {
            return await _dbContext.Event.FirstOrDefaultAsync(e => e.Id == eventId);
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadiusKm = 6371;

            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }

        private double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public async Task<List<Event>> GetAll()
        {
            return await _dbContext.Event
                .Where(x => x.localization.Id == x.localizationId)
                .Include(u => u.localization).ToListAsync();
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


        public async Task<List<EventWithLocalization>> GetEventByLocalization(LocalizationDto localization)
        {
            var query = _dbContext.Event
                .Join(
                    _dbContext.Localization,
                    e => e.localizationId,
                    l => l.Id,
                    (e, l) => new EventWithLocalization { Event = e, Localization = l }
                )
                .AsEnumerable() // Isso traz os dados do banco de dados para a memória
                .Where(ti => CalculateDistance(
                    lat1: localization.Latitude,
                    lon1: localization.Longitude,
                    lat2: ti.Localization.Latitude,
                    lon2: ti.Localization.Longitude) <= 1.0);

            return query.ToList();
        }

    }
}

