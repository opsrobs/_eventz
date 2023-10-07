using eventz.Models;

namespace eventz.Repositories.Interfaces
{
    public interface IUserTokenRepositorie
    {
        Task<UserToken> GetToken(Guid id);
        Task<UserToken> CreateToken(UserToken userToken);
        Task<bool> DeleteToken(Guid id);
    }
}
