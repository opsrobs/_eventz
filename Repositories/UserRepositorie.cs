using eventz.Data;
using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await _dbContext.Users.Where(x => x.Person.Id == x.PersonId).Include(u => u.Person).ToListAsync();
        }
        public async Task<bool> DataIsUnique(string cpf)
        {
            if (!await _dbContext.Users.AnyAsync(x => x.CPF == cpf))
            {
                return true;
            }
            return false;
        }
        public async Task<UserModel> GetUserById(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                await _dbContext.Entry(user).Reference(u => u.Person).LoadAsync();
            }

            return user;
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
            UserModel userId = await GetUserById(id);

            if (userId == null)
            {
                throw new InvalidOperationException("User not found");
            }

            userId.CPF = user.CPF;
            userId.DateOfBirth = user.DateOfBirth;
            userId.Person.Name = user.Person.Name;
            userId.Person.Email = user.Person.Email;
            userId.Person.UpdatedAt = DateTime.Now;

            _dbContext.Users.Update(userId);
            await _dbContext.SaveChangesAsync();

            return userId;
        }




    }
}
