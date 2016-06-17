using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using tupapiService.DataObjects;
using tupapiService.Mapping;
using tupapiService.Models;

namespace tupapiService.Controllers
{
    [Authorize]
    public class UserPostController : TableController<PostDTO>
    {
        private TupapiContext _context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            _context = new TupapiContext();
            DomainManager = new UserPostDomainManager(_context, Request, true);
        }

        // GET tables/PostTable
        public IQueryable<PostDTO> GetAllPost()
        {
            return Query();
        }

        // GET tables/PostTable/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<PostDTO> GetPost(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/PostTable/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<PostDTO> PatchPost(string id, Delta<PostDTO> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/PostTable
        public async Task<IHttpActionResult> PostPost(PostDTO item)
        {
            PostDTO current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new {id = current.Id}, current);
        }

        // DELETE tables/PostTable/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePost(string id)
        {
            return DeleteAsync(id);
        }
    }
}