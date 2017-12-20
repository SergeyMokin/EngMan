namespace EngMan.Account
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}
