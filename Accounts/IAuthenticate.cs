namespace eventz.Accounts
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string username, string password);
        Task<bool> UserExists(string username);
        public string GenerateToken(Guid id, string email);
    }
}
