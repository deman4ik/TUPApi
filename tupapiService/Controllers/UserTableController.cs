using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using tupapi.Shared.DataObjects;
using tupapi.Shared.Enums;
using tupapiService.Authentication;
using tupapiService.DataObjects;
using tupapiService.Helpers.ExceptionHelpers;
using tupapiService.Mapping;
using tupapiService.Models;

namespace tupapiService.Controllers
{
    [Authorize]
    public class UserTableController : TableController<UserDTO>
    {
        private TupapiContext _context;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
           
                base.Initialize(controllerContext);
                _context = new TupapiContext();
               
                DomainManager = new UserDomainManager(_context, Request, true);
        }

        //public IQueryable<UserDTO> GetAllUsers()
        //{
        //    return Query();
        //}

        public SingleResult<UserDTO> GetUser()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = BaseAuth.GetUserId(_context, claimsPrincipal);
            return Lookup(userId);
        }


        public Task<UserDTO> PatchUser(Delta<UserDTO> patch)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = BaseAuth.GetUserId(_context, claimsPrincipal);
            return UpdateAsync(userId, patch);
        }

        //[ResponseType(typeof (UserDTO))]
        //public async Task<IHttpActionResult> PostUser(UserDTO user)
        //{
        //    var current = await InsertAsync(user);
        //    return CreatedAtRoute("Tables", new {id = current.Id}, current);
        //}

        //public Task DeleteUser(string id)
        //{
        //    return DeleteAsync(id);
        //}
    }
}