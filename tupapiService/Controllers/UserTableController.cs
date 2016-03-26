using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using tupapiService.DataObjects;
using tupapiService.Mapping;
using tupapiService.Models;

namespace tupapiService.Controllers
{
    public class UserTableController : TableController<UserDTO>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            TupapiContext context = new TupapiContext();
            DomainManager = new UserDomainManager(context, Request, true);
        }

        public IQueryable<UserDTO> GetAllTodoItems()
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