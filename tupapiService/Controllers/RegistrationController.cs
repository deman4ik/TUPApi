using System;
using System.Data.Entity.Core;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using tupapi.Shared.DataObjects;
using tupapi.Shared.Enums;
using tupapi.Shared.Enums.Auth;
using tupapiService.Authentication;
using tupapiService.Helpers.CheckHelpers;
using tupapiService.Helpers.ExceptionHelpers;
using tupapiService.Models;

namespace tupapiService.Controllers
{
    [MobileAppController]
    public class RegistrationController : ApiController
    {
        private readonly ITupapiContext _context;

        public RegistrationController()
        {
            _context = new TupapiContext();
        }

        public RegistrationController(ITupapiContext context)
        {
            _context = context;
        }

        public HttpResponseMessage Registration(StandartAuthRequest request)
        {
            try
            {
                // Check request and request props is not null
                CheckHelper.IsNull(request, "request");
                CheckHelper.IsNull(request.Email, nameof(request.Email));
                CheckHelper.IsNull(request.Name, nameof(request.Name));
                CheckHelper.IsNull(request.Password, nameof(request.Password));
                // We use lowercased User Names
                request.Email = request.Email.ToLower();
                request.Name = request.Name.ToLower();
                // Validate request props
                CheckHelper.EmailCheck(request.Email);

                CheckHelper.NameCheck(request.Name);
                CheckHelper.PasswordCheck(request.Password);
                // Check if User Already Exist
                CheckData.UserExist(_context, true, email: request.Email, name: request.Name);
                var newUser = BaseAuth.CreateUser(_context, Provider.Standart, request);
                return Request.CreateResponse(HttpStatusCode.Created,
                    new BaseResponse(ApiResult.Created, message: newUser.Id));
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