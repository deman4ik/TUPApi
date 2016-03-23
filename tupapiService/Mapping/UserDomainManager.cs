using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using tupapiService.DataObjects;
using tupapiService.Models;

namespace tupapiService.Mapping
{
    public class UserDomainManager : GenericMappedEntityDomainManager<UserDTO,User>
    {
        public UserDomainManager(TupapiContext context, HttpRequestMessage request) : base(context, request)
        {
        }

        public UserDomainManager(TupapiContext context, HttpRequestMessage request, bool enableSoftDelete) : base(context, request, enableSoftDelete)
        {
        }
    }
}