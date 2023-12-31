﻿using eventz.Accounts;
using eventz.DTOs;
using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eventz.Repositories
{
    public class PersonRepositorie : IPersonRepositorie
    {
        private readonly Data.EventzDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IAuthenticate _authenticate;
        public PersonRepositorie(Data.EventzDbContext eventzDbContext, IConfiguration configuration, IAuthenticate authenticate)
        {
            _dbContext = eventzDbContext;
            _configuration = configuration;
            _authenticate = authenticate;
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

            personID.Name = person.Name;
            personID.Email = person.Email;
            personID.UpdatedAt = DateTime.Now;

            _dbContext.Person.Update(personID);
            await _dbContext.SaveChangesAsync();

            return personID;
        }
        public async Task<PersonModel> GetDataFromLogin(PersonToDtoLogin loginDetails)
        {
            return _dbContext.Person.FirstOrDefault(x => x.Email == loginDetails.Email);
        }


        public async Task<bool> UsernameIsUnique(string email)
        {
            if (!await _dbContext.Person.AnyAsync(x => x.Email == email))
            {
                return true;
            }
            return false;
        }
    }
}
