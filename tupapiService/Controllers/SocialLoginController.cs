using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Versioning;
using System.Web;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json.Linq;
using tupapiService.Authentication;

namespace tupapiService.Controllers
{
    [MobileAppController]
    public class SocialLoginController : ApiController
    {
        [Route("/api/login")]
        [HttpGet]
        public HttpResponseMessage Login([FromUri]string code = null,
            [FromUri]string grant_type = null,
           [FromUri]string redirect_uri = null,
           [FromUri]string client_id = null,
           [FromUri]string client_secret = null,
            [FromUri]string error = null, [FromUri]string error_reason = null, [FromUri]string error_description = null)
        {
            if (!string.IsNullOrWhiteSpace(error))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error: "+error+" Error reason: "+error_reason+" Error description: "+error_description);

            return Request.CreateResponse(HttpStatusCode.OK, new AccessToken("Code :" + code + " Grant type: " + grant_type + "Redirect Uri: " + redirect_uri + "Client id: " + client_id + "Client_secret: " + client_secret));
        }
    }
}