using eventz.SecurityServices.Interfaces;

namespace eventz.SecurityServices
{
    public class SecurityService : ISecurityService
    {
        async public Task<string> EncryptPassword(string password)
        {
            try
            {
                string encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                return encryptedPassword;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu uma exceção: {ex.Message}");
                return null;
            }

        }
    }
}
