using eventz.Data;
using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eventz.Repositories
{
    public class UserTokenRepositorie : IUserTokenRepositorie
    {
        private readonly EventzDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public UserTokenRepositorie(EventzDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public async Task<UserToken> CreateToken(UserToken userToken)
        {
            await _dbContext.Token.AddAsync(userToken);
            await _dbContext.SaveChangesAsync();
            return userToken;
        }

        public async Task<bool> DeleteToken(Guid id)
        {
            UserToken userToken = await GetToken(id);

            if (userToken == null)
            {
                throw new InvalidOperationException("Token not found");
            }


            _dbContext.Token.Remove(userToken);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UserToken> GetToken(Guid id)
        {
            return await _dbContext.Token.FirstOrDefaultAsync(x => x.Id == id);
        }
        
            public async Task<UserToken> GetTokenByRefresh(string refresh)
        {
            return await _dbContext.Token.FirstOrDefaultAsync(x => x.RefreshToken == refresh);
        }
    }
}
