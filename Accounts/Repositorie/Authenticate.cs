using eventz.Data;
using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eventz.Accounts.Repositorie
{
    public class Authenticate : IAuthenticate
    {
        private readonly EventzDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IUserTokenRepositorie _userTokenRepositorie;
        public Authenticate(EventzDbContext userDbContext, IConfiguration configuration, IUserTokenRepositorie userTokenRepositorie)
        {
            _dbContext = userDbContext;
            _configuration = configuration;
            _userTokenRepositorie = userTokenRepositorie;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            var user = await _dbContext.Person.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) return false;

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) return false;

            return true;
        }

        public async Task<UserToken> RefreshToken(UserToken userToken)
        {
            if (userToken == null || string.IsNullOrEmpty(userToken.Token))
            {
                throw new ArgumentNullException(nameof(userToken), "UserToken or its Token property cannot be null or empty.");
            }

            var principal = GetTokenData(userToken.Token);
            if (principal == null || !principal.Identity.IsAuthenticated)
            {
                throw new SecurityTokenException("Invalid token!");
            }

            var savedRefreshToken = await _userTokenRepositorie.GetToken(userToken.Id);
            if (savedRefreshToken == null || savedRefreshToken.RefreshToken != userToken.RefreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token!");
            }

            var newJwtToken = GenerateToken(principal.Claims);
            var newRefreshJwt = Guid.NewGuid().ToString();

            await _userTokenRepositorie.DeleteToken(userToken.Id);
            var newToken = new UserToken
            {
                Token = newJwtToken,
                RefreshToken = newRefreshJwt,
                Email = userToken.Email
            };
            return await _userTokenRepositorie.CreateToken(newToken);
        }


        public string GenerateToken(Guid id, string email)
        {
            var claims = new[]
            {
                new Claim("id",id.ToString()),
                new Claim("email",email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var privateKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_configuration["jwt:SecretKey"]));

            var credentials = new SigningCredentials
                (privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_configuration["jwt:SecretKey"]));

            var credentials = new SigningCredentials
                (key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal GetTokenData(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_configuration["jwt:SecretKey"])),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
                throw new InvalidOperationException("Invalid Token!");


            return principal;
        }

        public async Task<bool> UserExists(string email)
        {
            var user = await _dbContext.Person.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) return false;

            return true;
        }
    }
}
