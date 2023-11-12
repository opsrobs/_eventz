using AutoMapper;
using eventz.Accounts;
using eventz.DTOs;
using eventz.Models;
using eventz.Repositories.Interfaces;
using eventz.SecurityServices.Interfaces;
using eventz.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace eventz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepositorie _repositorie;
        private readonly IPersonRepositorie _personRepositorie;
        private readonly IUserTokenRepositorie _userTokenRepositorie;
        private readonly IAuthenticate _authenticate;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;



        public UserController(IUserRepositorie repositorie, IPersonRepositorie personRepositorie, IMapper mapper, IAuthenticate authenticate, ISecurityService securityService, IUserTokenRepositorie userTokenRepositorie)
        {
            _repositorie = repositorie;
            _personRepositorie = personRepositorie;
            _mapper = mapper;
            _authenticate = authenticate;
            _securityService = securityService;
            _userTokenRepositorie = userTokenRepositorie;       
        }

        private bool IsAuthorizedUser(Guid id)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdFromToken == id.ToString();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<ResponseUserDtoToken>> Create([FromBody] PersonToDtoCreate userRequest)
        {
            if (!await _personRepositorie.UsernameIsUnique(userRequest.Email))
            {
                return BadRequest(new { error = Messages.EmailError });
            }

            UserModel userModel = _mapper.Map<UserModel>(userRequest);
            if (userRequest.Roles == Enums.RolesEnum.Employee)
            {
                userModel.Person.Roles = Enums.RolesEnum.Employee;
            }
            userModel.Person.Roles = Enums.RolesEnum.User;
            if (string.IsNullOrEmpty(userModel.Person.Password))
            {
                return BadRequest(new { error = Messages.EmprtyPassword });
            }

            try
            {
                userModel.Person.Password = await _securityService.EncryptPassword(userRequest.Password);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error encrypting password." });
            }

            await _repositorie.Create(userModel);

            var token = _authenticate.GenerateToken(new PersonModel
            {
                Id = userModel.PersonId,
                Email = userModel.Person.Email,
                Roles = userModel.Person.Roles
            });

            try
            {
                UserToken userToken = await CreateUserTokenAsync(userRequest.Email, token);
                return Ok(new ResponseUserDtoToken
                {
                    Token = userToken.Token,
                    RefreshToken = userToken.RefreshToken
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error creating user token." });
            }
        }

        private async Task<UserToken> CreateUserTokenAsync(string email, string token)
        {
            UserToken userToken = new UserToken
            {
                Token = token,
                RefreshToken = Guid.NewGuid().ToString(),
                Email = email
            };

            return await _userTokenRepositorie.CreateToken(userToken);
        }


        [HttpPost]
        [Route("Refresh")]
        public async Task<UserToken> RefreshToken(string refreshToken)
        {
            UserToken token = await _userTokenRepositorie.GetTokenByRefresh(refreshToken);
            return await _authenticate.RefreshToken(token);
        }
        [HttpPut("{id}")]
        [Authorize(Roles ="User")]
        public async Task<ActionResult<UserDto>> Update([FromBody] UserToDtoUpdate userModel, Guid id)
        {
            var error = "";
            if (!IsAuthorizedUser(id))
            {
                return Forbid(Messages.AcessoNegado);
            }

            if (await _repositorie.DataIsUnique(userModel.CPF))
            {
                var isUpdated = _mapper.Map<UserModel>(userModel);
                isUpdated.Id = id;
                UserModel user = await _repositorie.Update(isUpdated, id);
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);

            }
            else
                return BadRequest(new { 
                error,
                    V = Messages.CpfError
                });

        }

        [HttpGet]
        //[Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<UserToDtoList>>> GetAllUsers()
        {
            List<UserModel> users = await _repositorie.GetAllUsers();
            List<UserToDtoList> userDtos = _mapper.Map<List<UserToDtoList>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            if (!IsAuthorizedUser(id))
            {
                return Forbid(Messages.AcessoNegado);
            }

            var user = await _repositorie.GetUserByPersonId(id);
            return Ok(_mapper.Map<UserDto>(user));
        }


        [HttpDelete("{id}")]
        [Authorize(Roles ="User")]
        public async Task<ActionResult<List<UserModel>>> Delete(Guid id)
        {
            bool deleted = await _repositorie.Delete(id);
            return Ok(deleted);
        }
    }
}
