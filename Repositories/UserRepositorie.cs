using eventz.Data;
using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eventz.Repositories
{
    public class UserRepositorie : IUserRepositorie
    {
        private readonly EventzDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public UserRepositorie(EventzDbContext userDbContext, IConfiguration configuration) 
        {
            _dbContext = userDbContext;
            _configuration = configuration;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }
        public async Task<bool> DataIsUnique(UserModel user)
        {
            if (!await _dbContext.Users.AnyAsync(x => x.CPF == user.CPF) || !await _dbContext.Users.AnyAsync(x => x.CNPJ == user.CNPJ))
            {
                return true;
            }
            return false;
        }
        public async Task<UserModel> GetUserById(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserModel> Create(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Delete(Guid id)
        {
            UserModel userId = await GetUserById(id);

            if (userId == null)
            {
                throw new InvalidOperationException("User not found");
            }

            
            _dbContext.Users.Remove(userId);
            await _dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<UserModel> Update(UserModel user, Guid id)
        {
            UserModel userId =  await GetUserById(id);

            if(userId == null) 
            {
                throw new InvalidOperationException("User not found");
            }

            userId.FirstName = user.FirstName;
            userId.LastName = user.LastName;
            userId.Email = user.Email;
            userId.Username = user.Username;
            userId.UpdatedAt = DateTime.Now;

            _dbContext.Users.Update(userId);
            await _dbContext.SaveChangesAsync();

            return userId;
        }

       

      
    }
}
