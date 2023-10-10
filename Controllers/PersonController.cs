using AutoMapper;
using eventz.Accounts;
using eventz.DTOs;
using eventz.Models;
using eventz.Repositories;
using eventz.Repositories.Interfaces;
using eventz.SecurityServices.Interfaces;
using eventz.Utils;
using Microsoft.AspNetCore.Mvc;

namespace eventz.Controllers
{
    [Route("Login")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IPersonRepositorie _repositorie;
        private readonly IUserTokenRepositorie _userTokenRepositorie;
        private readonly IAuthenticate _authenticate;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepositorie repositorie, IMapper mapper, IAuthenticate authenticate, ISecurityService securityService, IUserTokenRepositorie userTokenRepositorie)
        {
            _repositorie = repositorie;
            _mapper = mapper;
            _authenticate = authenticate;
            _securityService = securityService;
            _userTokenRepositorie = userTokenRepositorie;
        }


        [HttpPost]
        public async Task<ActionResult<PersonDto>> Authenticate([FromBody] PersonToDtoLogin login)
        {
            var userLoggin = await _authenticate.AuthenticateAsync(login.Email, login.Password);
            if (userLoggin == false)
            {
                return NotFound(Messages.PasswordFailure);
            }
            PersonModel person = await _repositorie.GetDataFromLogin(login);
            var token = _authenticate.GenerateToken(person);

            UserToken userToken = new UserToken
            {
                Token = token,
                RefreshToken = Guid.NewGuid().ToString(),
                Email = login.Email
            };

            var refreshToken = await _userTokenRepositorie.CreateToken(userToken);
            ResponseUserDtoToken response = new ResponseUserDtoToken
            {
                Token = refreshToken.Token,
                RefreshToken = refreshToken.RefreshToken
            };

            return Ok(response);
        }
    }
}
