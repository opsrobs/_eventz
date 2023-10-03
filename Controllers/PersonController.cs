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
    }
}
