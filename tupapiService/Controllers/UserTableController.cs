using System;
using System.Data.Entity.Core;
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
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            try
            {
                base.Initialize(controllerContext);
                TupapiContext context = new TupapiContext();
                ClaimsPrincipal claimsPrincipal = this.User as ClaimsPrincipal;
                string userId = BaseAuth.GetUserId(context, claimsPrincipal);
                DomainManager = new UserDomainManager(context, Request, userId, true);
            }
            catch (Exception ex)
            {
                 Debug.WriteLine(ex.Message);
            }

        }

        public IQueryable<UserDTO> GetAllUsers()
        {
            return Query();
        }

        public SingleResult<UserDTO> GetUser(string id)
        {
            return Lookup(id);
        }

        public Task<UserDTO> PatchUser(string id, Delta<UserDTO> patch)
        {
            return UpdateAsync(id, patch);
        }

        [ResponseType(typeof (UserDTO))]
        public async Task<IHttpActionResult> PostUser(UserDTO user)
        {
            UserDTO current = await InsertAsync(user);
            return CreatedAtRoute("Tables", new {id = current.Id}, current);
        }

        public Task DeleteUser(string id)
        {
            return DeleteAsync(id);
        }
    }
}