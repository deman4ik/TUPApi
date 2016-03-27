using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tupapi.Shared.Interfaces
{
    public interface ILoginResult
    {
        JwtSecurityToken AuthenticationToken { get; }
        IUser User { get; }
    }
}
