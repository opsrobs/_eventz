namespace eventz.Models
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public UserToken()
        {
            Id = Guid.NewGuid();
        }

        public UserToken(Guid id, string email, string token, string refreshToken)
        {
            Id = id;
            Email = email;
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}
