namespace eventz.Models
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public UserToken()
        {
            Id = Guid.NewGuid();
        }

        public UserToken(Guid id, string username, string token, string refreshToken)
        {
            Id = id;
            Username = username;
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}
