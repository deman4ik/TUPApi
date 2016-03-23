using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using tupapiService.Models;

namespace tupapiService.Controllers
{
    [MobileAppController]
    public class UserController : ApiController
    {
        private ITupapiContext _context;

        public UserController()
        {
            _context = new TupapiContext();
        }


        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _context.Users.Where(x => x.Deleted == false).ToList());
        }
    }
}