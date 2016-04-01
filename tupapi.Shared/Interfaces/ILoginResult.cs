using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupapi.Shared.DataObjects;

namespace tupapi.Shared.Interfaces
{
    public interface ILoginResult
    {
        string AuthenticationToken { get; }
        IUser User { get; }
    }
}
