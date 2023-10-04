using eventz.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Cryptography;
using System.Text;

namespace eventz.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CPF { get; set; }
        public PersonModel PersonID{ get; set; }

        public UserModel()
        {
            Id = Guid.NewGuid();
            PersonID = new PersonModel(); 
            PersonID.Roles = RolesEnum.User;
        }


        public UserModel(Guid id,  DateTime? dateOfBirth, string? cPF, PersonModel personID)
        {
            Id = id;
            DateOfBirth = dateOfBirth;
            CPF = cPF;
            PersonID = personID;
        }
    }
}
