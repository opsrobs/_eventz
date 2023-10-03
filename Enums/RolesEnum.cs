using System.ComponentModel;

namespace eventz.Enums
{
    public enum RolesEnum
    {
        [Description("Perfil administrador")]
        Admin = 0,
        [Description("Perfil de usuario")]
        User = 1,
        [Description("Perfil de organizador do evento")]
        Employee = 2,

    }
}
