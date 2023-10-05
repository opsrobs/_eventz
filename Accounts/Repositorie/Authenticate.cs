using eventz.Data;
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
        public Authenticate(EventzDbContext userDbContext, IConfiguration configuration)
        {
            _dbContext = userDbContext;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var user = await _dbContext.Person.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return false;

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) return false;

            return true;
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
                ValidateAudience = true,
                ValidateIssuer = true,
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

        public async Task<bool> UserExists(string username)
        {
            var user = await _dbContext.Person.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return false;

            return true;
        }
    }
}
