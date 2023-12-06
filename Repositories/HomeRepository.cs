using eventz.Data;
using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eventz.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly EventzDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public HomeRepository(EventzDbContext userDbContext, IConfiguration configuration)
        {
            _dbContext = userDbContext;
            _configuration = configuration;
        }
        public async Task<Home> Create(Home home)
        {
            await _dbContext.Home.AddAsync(home);
            await _dbContext.SaveChangesAsync();
            return home;
        }

        public async Task<bool> Delete(Guid eventId)
        {
            Home home = await GetHomeById(eventId);

            if (home == null)
            {
                throw new InvalidOperationException("User not found");
            }


            _dbContext.Home.Remove(home);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Home> GetHomeById(Guid id)
        {
            var home = await _dbContext.Home.FirstOrDefaultAsync(x => x.Id == id);

            if (home != null)
            {
                await _dbContext.Entry(home).Reference(u => u.Categories).LoadAsync();
            }

            return home;
        }

        public Task<Home> Update(Home home, Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Home>> GetAll()
        {
            return await _dbContext.Home
                .Include(x => x.Sections)
                .ThenInclude(s => s.Events) // Inclui os Events dentro das Sections
                .Where(x => x.Sections.Any(s => s.Events.Any(e => e.Name == "xpto"))) // Filtra Homes com um Event específico
                .ToListAsync();
        }
    }
}
