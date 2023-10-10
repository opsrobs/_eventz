using eventz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eventz.Accounts
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<bool> UserExists(string email);
        public string GenerateToken(PersonModel person);
        public string GenerateToken(IEnumerable<Claim> claims);

        public ClaimsPrincipal GetTokenData(string token);
        public Task<UserToken> RefreshToken(UserToken userToken);
    }
}
