using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Microsoft.Azure.Mobile.Server.Config;
using tupapi.Shared.DataObjects;
using tupapi.Shared.Enums;
using tupapi.Shared.Enums.Auth;
using tupapiService.Authentication;
using tupapiService.DataObjects;
using tupapiService.Helpers.CheckHelpers;
using tupapiService.Helpers.ExceptionHelpers;
using tupapiService.Models;
using User = tupapiService.Models.User;

namespace tupapiService.Controllers
{
    [MobileAppController]
    public class LoginController : ApiController
    {

        private readonly ITupapiContext _context;
        public LoginController()
        {
            
            _context = new TupapiContext();

        }

        public LoginController(ITupapiContext context)
        {
            _context = context;

        }

        public HttpResponseMessage Login(StandartAuthRequest request)
        {
            try
            {
                // Check request and request props is not null
                CheckHelper.IsNull(request, "request");
                CheckHelper.IsNull(request.Password, request.PasswordPropertyName);
                // Find User
                var user = CheckData.UserExist(_context, false, email: request.Email, name: request.Name);
                if (user == null)
                    throw new ApiException(ApiResult.Validation, ErrorType.UserWithEmailorNameNotFound, request.Email?? request.Name);
                // Check if User is Blocked
                CheckData.IsUserBlocked(_context, null, user);
                // Check if User Account Exist
                var account = CheckData.AccountExist(_context, Provider.Standart, user.Id);
                // Check password
                BaseAuth auth = new BaseAuth(_context);
                auth.CheckPassword(user,request.Password);
                

                    var token = auth.CreateToken(account.AccountId);
                UserDTO userDto = Mapper.Map<User, UserDTO>(user);
                // Generate AuthenticationToken
                return Request.CreateResponse(HttpStatusCode.OK,
                    new LoginResult(token,
                        userDto));

            }
            catch (ApiException ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
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