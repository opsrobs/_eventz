using eventz.Models;

namespace eventz.Repositories.Interfaces
{
    public interface IUserRepositorie
    {
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(Guid id);
        Task<UserModel> Create(UserModel user);
        Task<UserModel> Update(UserModel user, Guid id);
        Task<bool> Delete(Guid id);
        Task<bool> DataIsUnique(string cpf);


    }
}
