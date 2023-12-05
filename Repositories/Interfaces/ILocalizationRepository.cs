using eventz.Models;

namespace eventz.Repositories.Interfaces
{
    public interface ILocalizationRepository
    {
        Task<Localization> Create(Localization localization);
        Task<Localization> Update(Localization localization, Guid id);
        Task<Localization> GetLocalById(Guid id);
    }
}
