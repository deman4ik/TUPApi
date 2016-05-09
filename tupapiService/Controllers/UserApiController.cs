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
    public class UserApiController : ApiController
    {
        private readonly ITupapiContext _context;
        private readonly MapperConfiguration _config;

        public UserApiController()
        {
            _context = new TupapiContext();
            _config = Mapping.Mapping.GetConfiguration();
        }

        public UserApiController(ITupapiContext context)
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
                return Request.CreateResponse(HttpStatusCode.OK, new Response<UserDTO>(ApiResult.Ok, userDto));
            }
            catch (ApiException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new Response<UserDTO>(ex.ApiResult,null, new ErrorResponse(ex.ErrorType, ex.Message, ex)));
            }
            catch (EntitySqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new Response<UserDTO>(ApiResult.Sql, null, new ErrorResponse(ErrorType.None, ex.Message, ex)));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new Response<UserDTO>(ApiResult.Unknown, null, new ErrorResponse(ErrorType.Internal, ex.Message, ex)));
            }
        }
    }
}