using eventz.Models;

namespace eventz.Repositories.Interfaces
{
    public interface IHomeRepository
    {
        Task<Home> GetHomeById(Guid homeId);
        Task<Home> Create(Home home);
        Task<Home> Update(Home home, Guid id);
        Task<List<Home>> GetAll();
        Task<bool> Delete(Guid eventId);
    }
}
