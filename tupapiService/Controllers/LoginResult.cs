using tupapi.Shared.Interfaces;
using tupapiService.DataObjects;

namespace tupapiService.Controllers
{
    public class LoginResult : ILoginResult
    {
        public LoginResult(string authenticationToken, UserDTO user)
        {
            AuthenticationToken = authenticationToken;
            User = user;
        }

        public string AuthenticationToken { get; set; }
        public IUser User { get; set; }
    }
}