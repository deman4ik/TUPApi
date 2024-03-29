﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using tupapiService.Authentication;
using tupapiService.DataObjects;
using tupapiService.Mapping;
using tupapiService.Models;

namespace tupapiService.Controllers
{
    [Authorize]
    public class UserController : TableController<UserDTO>
    {
        private TupapiContext _context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            _context = new TupapiContext();

            DomainManager = new UserDomainManager(_context, Request, true);
        }

        public IQueryable<UserDTO> GetAllUsers()
        {
            var userId = BaseAuth.GetUserId(RequestContext);
            return Query().Where(u => u.Id == userId);
        }

        public SingleResult<UserDTO> GetUser(string id)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = BaseAuth.GetUserId(claimsPrincipal);
            return Lookup(userId);
        }


        public Task<UserDTO> PatchUser(Delta<UserDTO> patch)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var userId = BaseAuth.GetUserId(claimsPrincipal);
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