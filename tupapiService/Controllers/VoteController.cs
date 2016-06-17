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
    public class VoteController : TableController<VoteDTO>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            TupapiContext context = new TupapiContext();
            DomainManager = new VoteDomainManager(context, Request, true);
        }

        // GET tables/Vote
        public IQueryable<VoteDTO> GetAllVote()
        {
            return Query(); 
        }

        // GET tables/Vote/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<VoteDTO> GetVote(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Vote/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<VoteDTO> PatchVote(string id, Delta<VoteDTO> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Vote
        public async Task<IHttpActionResult> PostVote(VoteDTO item)
        {
            VoteDTO current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Vote/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteVote(string id)
        {
             return DeleteAsync(id);
        }
    }
}
