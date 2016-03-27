using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupapi.Shared.Interfaces;

namespace tupapi.Shared.DataObjects
{
    public class LoginResult : ILoginResult
    {
        public LoginResult(JwtSecurityToken authenticationToken, IUser user)
        {
            AuthenticationToken = authenticationToken;
            User = user;
        }
        public JwtSecurityToken AuthenticationToken { get;  }
        public IUser User { get;  }


    }
}
