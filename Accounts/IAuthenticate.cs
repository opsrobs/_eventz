using eventz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eventz.Accounts
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<bool> UserExists(string email);
        public string GenerateToken(Guid id, string email);
        public string GenerateToken(IEnumerable<Claim> claims);

        public ClaimsPrincipal GetTokenData(string token);
        public Task<UserToken> RefreshToken(UserToken userToken);
    }
}
