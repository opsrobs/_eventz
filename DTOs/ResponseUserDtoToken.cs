using System.Xml.Linq;

namespace eventz.DTOs
{
    public record ResponseUserDtoToken
    {
        //public ResponseUserDtoToken() { } // Construtor sem parâmetros

        //public ResponseUserDtoToken(string email, string? name, DateTime? dateOfBirth)
        //{
        //    Email = email;
        //    Name = name;
        //    DateOfBirth = dateOfBirth;
        //}

        //public string Email { get; init; }
        //public string? Name { get; init; }
        //public DateTime? DateOfBirth { get; init; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
   
}
