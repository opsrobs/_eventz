namespace eventz.SecurityServices.Interfaces
{
    public interface ISecurityService
    {
        Task<string> EncryptPassword(string password);
    }
}
