using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using tupapiService.Models;

namespace tupapiService.Controllers
{
    [MobileAppController]
    [Authorize]
    public class UserController : ApiController
    {
        private ITupapiContext _context;

        public UserController()
        {
            _context = new TupapiContext();
        }

        
        public HttpResponseMessage Get()
        {
            ClaimsPrincipal claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Request.CreateResponse(HttpStatusCode.OK, sid);
        }
    }
}