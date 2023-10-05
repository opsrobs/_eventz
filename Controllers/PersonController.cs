using AutoMapper;
using eventz.Accounts;
using eventz.DTOs;
using eventz.Models;
using eventz.Repositories.Interfaces;
using eventz.SecurityServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eventz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IPersonRepositorie _repositorie;
        private readonly IAuthenticate _authenticate;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepositorie repositorie, IMapper mapper, IAuthenticate authenticate, ISecurityService securityService)
        {
            _repositorie = repositorie;
            _mapper = mapper;
            _authenticate = authenticate;
            _securityService = securityService;
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> Create([FromBody] PersonModel personModel)
        {
            if (await _repositorie.UsernameIsUnique(personModel))
            {
                personModel.Id = Guid.NewGuid();
                string encrypted = await _securityService.EncryptPassword(personModel.Password);
                personModel.Password = encrypted;

                PersonModel newPerson = await _repositorie.Create(personModel);

                var userDto = _mapper.Map<PersonDto>(newPerson);
                var token = _authenticate.GenerateToken(personModel.Id, personModel.Email);

                return Ok(new { User = userDto, Token = token });
            }
            else
            {
                return BadRequest("Username não disponivel!");
            }
        }



        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<PersonDto>> Authenticate([FromBody] PersonToDtoLogin login)
        {
            var userLoggin = await _authenticate.AuthenticateAsync(login.Username, login.Password);
            if (userLoggin == false)
            {
                return NotFound("Usuario ou senha inválidos!");
            }
            PersonModel person = await _repositorie.GetDataFromLogin(login);
            var token = _authenticate.GenerateToken(person.Id, person.Email);
            var personDto = _mapper.Map<PersonDto>(person);

            return Ok(new { User = personDto, Token = token });
        }
    }
}
