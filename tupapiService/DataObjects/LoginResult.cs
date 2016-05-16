using System;
using tupapi.Shared.Interfaces;

namespace tupapiService.DataObjects
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

        public override string ToString()
        {
            return "# Authentication Token:" + Environment.NewLine + " # User Id:" + Environment.NewLine + User.Id;
        }
    }
}