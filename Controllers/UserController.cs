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
    public class UserController : ControllerBase
    {
        private readonly IUserRepositorie _repositorie;
        private readonly IPersonRepositorie _personRepositorie;
        private readonly IAuthenticate _authenticate;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;



        public UserController(IUserRepositorie repositorie, IPersonRepositorie personRepositorie, IMapper mapper, IAuthenticate authenticate, ISecurityService securityService)
        {
            _repositorie = repositorie;
            _personRepositorie = personRepositorie;
            _mapper = mapper;
            _authenticate = authenticate;
            _securityService = securityService;
        }



        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserRequestDto userRequest)
        {
            if (!await _repositorie.DataIsUnique(userRequest.CPF))
            {
                return BadRequest("CPF já está cadastrado!");
            }

            userRequest.Person.Id = Guid.NewGuid();
            userRequest.Person.Password = await _securityService.EncryptPassword(userRequest.Person.Password);
            userRequest.Person.Roles = Enums.RolesEnum.User;

            UserModel userModel = _mapper.Map<UserModel>(userRequest);
            userModel.PersonId = userRequest.Person.Id;
            userModel = await _repositorie.Create(userModel);

            var userDto = _mapper.Map<UserDto>(userModel);
            var token = _authenticate.GenerateToken(userModel.Person.Id, userModel.Person.Email);

            return Ok(new { User = userDto, Token = token });
        }



        //[HttpPost]
        //[Route("Login")]
        //public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserModel user)
        //{
        //    var userLoggin = await _authenticate.AuthenticateAsync(user.Person.Username, user.Person.Password);
        //    if (userLoggin == false )
        //    {
        //        return NotFound("Usuario ou senha inválidos!");
        //    }
        //    var token = _authenticate.GenerateToken(user.Person.Id, user.Person.Email);

        //    return token;
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update([FromBody] UserToDtoUpdate userModel, Guid id)
        {
            //userModel.Person.Id= id;  
            if (await _repositorie.DataIsUnique(userModel.CPF))
            {
                var isUpdated = _mapper.Map<UserModel>(userModel);
                isUpdated.Id = id;
                UserModel user = await _repositorie.Update(isUpdated, id);
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);

            }
            else
                return BadRequest("CPF já está cadastro");

        }

        [HttpGet]
        public async Task<ActionResult<List<UserToDtoList>>> GetAllUsers()
        {
            List<UserModel> users = await _repositorie.GetAllUsers();
            List<UserToDtoList> userDtos = _mapper.Map<List<UserToDtoList>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserModel>>> GetUserById(Guid id)
        {
            UserModel user = await _repositorie.GetUserById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<UserModel>>> Delete(Guid id)
        {
            bool deleted = await _repositorie.Delete(id);
            return Ok(deleted);
        }
    }
}
