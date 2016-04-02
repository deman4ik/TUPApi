using tupapi.Shared.Interfaces;

namespace tupapi.Shared.DataObjects
{
    public class LoginResult : ILoginResult
    {
        public LoginResult(string authenticationToken, User user)
        {
            AuthenticationToken = authenticationToken;
            User = user;
        }

        public string AuthenticationToken { get; set; }
        public IUser User { get; set; }
    }
}