using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using tupapi.Shared.Enums;
using tupapiService.Authentication;
using tupapiService.DataObjects;
using tupapiService.Mapping;
using tupapiService.Models;

namespace tupapiService.Controllers
{
    [Authorize]
    public class QueuePostController : TableController<PostDTO>
    {
        private TupapiContext _context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            _context = new TupapiContext();
            DomainManager = new QueuePostDomainManager(_context, Request, true);
        }

        // GET tables/PostTable
        public IQueryable<PostDTO> GetAllPost()
        {
            return Query();
        }
    }
}