namespace eventz.Models
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public int Username { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }

        public UserToken()
        {
            Id = Guid.NewGuid();
        }

        public UserToken(Guid id, int username, string token, DateTime expiryDate)
        {
            Id = id;
            Username = username;
            Token = token;
            ExpiryDate = expiryDate;
        }
    }
}
