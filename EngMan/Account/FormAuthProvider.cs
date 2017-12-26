using System.Web.Security;
namespace EngMan.Account
{
    public class FormAuthProvider: IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = Membership.ValidateUser(username, password);
            if (result)
                FormsAuthentication.SetAuthCookie(username, false);
            return result;
        }
    }
}