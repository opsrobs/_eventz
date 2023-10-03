using eventz.Models;

namespace eventz.Repositories.Interfaces
{
    public interface IPersonRepositorie
    {
        Task<PersonModel> GetPersonById(Guid id);
        Task<PersonModel> Create(PersonModel person);
        Task<PersonModel> Update(PersonModel person, Guid id);
        Task<bool> UsernameIsUnique(PersonModel person);
    }
}
