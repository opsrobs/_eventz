using eventz.Data;
using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eventz.Repositories
{


    public class LocalizationRepository : ILocalizationRepository
    {
        private readonly EventzDbContext _dbContext;

        public LocalizationRepository(EventzDbContext eventzDbContext)
        {
            _dbContext = eventzDbContext;
        }
        public async Task<Localization> Create(Localization localization)
        {
            await _dbContext.Localization.AddAsync(localization);
            await _dbContext.SaveChangesAsync();
            return localization;
        }

        public async Task<Localization> GetLocalById(Guid id)
        {
            return await _dbContext.Localization.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Localization> Update(Localization localization, Guid id)
        {
            Localization localizationID = await GetLocalById(id);
            if (localizationID == null)
            {
                throw new InvalidOperationException("Event {id} not found");
            }

            localizationID.Latitude = localization.Latitude;
            localizationID.Longitude = localization.Longitude;

            _dbContext.Localization.Update(localizationID);
            await _dbContext.SaveChangesAsync();

            return localizationID;
        }
    }
}
