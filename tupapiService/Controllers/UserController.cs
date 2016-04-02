using System;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using AutoMapper;
using Microsoft.Azure.Mobile.Server.Config;
using tupapi.Shared.DataObjects;
using tupapi.Shared.Enums;
using tupapiService.Authentication;
using tupapiService.DataObjects;
using tupapiService.Helpers.CheckHelpers;
using tupapiService.Helpers.ExceptionHelpers;
using tupapiService.Models;

namespace tupapiService.Controllers
{
    [MobileAppController]
    [Authorize]
    public class UserController : ApiController
    {
        private readonly ITupapiContext _context;
        private readonly MapperConfiguration _config;

        public UserController()
        {
            _context = new TupapiContext();
            _config = Mapping.Mapping.GetConfiguration();
        }

        public UserController(ITupapiContext context)
        {
            _context = context;
            _config = Mapping.Mapping.GetConfiguration();
        }

        [Authorize]
        public HttpResponseMessage GetCurrentUser()
        {
            try
            {
                var claimsPrincipal = this.User as ClaimsPrincipal;
                Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Debug.WriteLine(this.Request.Headers.ToString());
  
                foreach (var c in claimsPrincipal.Claims)
                {
                    Debug.WriteLine(c.ToString());
                }
                var user = BaseAuth.GetUser(_context, claimsPrincipal);
                var mapper = _config.CreateMapper();
                var userDto = mapper.Map<Models.User, UserDTO>(user);
                return Request.CreateResponse(HttpStatusCode.OK, userDto);
            }
            catch (ApiException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new BaseResponse(ex.ApiResult, ex.ErrorType, ex.Message));
            }
            catch (EntitySqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new BaseResponse(ApiResult.Sql, ErrorType.None, ex.Message));
            }
            catch (ArgumentNullException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new BaseResponse(ApiResult.Unknown, ErrorType.Internal, ex.Message));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new BaseResponse(ApiResult.Unknown, ErrorType.Internal, ex.Message));
            }
        }
    }
}