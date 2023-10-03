using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eventz.Repositories
{
    public class PersonRepositorie : IPersonRepositorie
    {
        private readonly Data.EventzDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public PersonRepositorie(Data.EventzDbContext eventzDbContext, IConfiguration configuration)
        {
            _dbContext = eventzDbContext;
            _configuration = configuration;
        }
        public async Task<PersonModel> Create(PersonModel person)
        {
            await _dbContext.Person.AddAsync(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }

        public async Task<PersonModel> GetPersonById(Guid id)
        {
            return await _dbContext.Person.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PersonModel> Update(PersonModel person, Guid id)
        {
            PersonModel personID = await GetPersonById(id);

            if (personID == null)
            {
                throw new InvalidOperationException("Person {id} not found");
            }

            personID.FirstName = person.FirstName;
            personID.LastName = person.LastName;
            personID.Username = person.Username;
            personID.UpdatedAt = DateTime.Now;

            _dbContext.Person.Update(personID);
            await _dbContext.SaveChangesAsync();

            return personID;
        }

        public async Task<bool> UsernameIsUnique(PersonModel person)
        {
            if (!await _dbContext.Person.AnyAsync(x => x.Username == person.Username))
            {
                return true;
            }
            return false;
        }
    }
}
