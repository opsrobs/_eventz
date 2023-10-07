using eventz.Enums;

namespace eventz.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CPF { get; set; }
        public Guid PersonId { get; set; }
        public PersonModel Person { get; set; }

        public UserModel()
        {
            Id = Guid.NewGuid();
        }

        public UserModel(Guid id, DateTime? dateOfBirth, string? cPF, Guid personId, PersonModel person)
        {
            Id = id;
            DateOfBirth = dateOfBirth;
            CPF = cPF;
            PersonId = personId;
            Person = person;
        }
    }
}
