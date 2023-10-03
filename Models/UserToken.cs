namespace eventz.Models
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }

        public UserToken()
        {
            Id = Guid.NewGuid();
        }

        public UserToken(Guid id, int userId, string token, DateTime expiryDate)
        {
            Id = id;
            UserId = userId;
            Token = token;
            ExpiryDate = expiryDate;
        }
    }
}
