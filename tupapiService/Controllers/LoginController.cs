using System;
using System.Data.Entity.Core;
using System.Net;
using System.Net.Http;
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
using LoginResult = tupapiService.DataObjects.LoginResult;
using User = tupapiService.Models.User;

namespace tupapiService.Controllers
{
    [MobileAppController]
    public class LoginController : ApiController
    {
        private readonly MapperConfiguration _config;

        private readonly ITupapiContext _context;
        private readonly IMapper _mapper;

        public LoginController()
        {
            _context = new TupapiContext();
            _config = Mapping.Mapping.GetConfiguration();
            _mapper = _config.CreateMapper();
        }

        public LoginController(ITupapiContext context)
        {
            _context = context;
            _config = Mapping.Mapping.GetConfiguration();
            _mapper = _config.CreateMapper();
        }

        public HttpResponseMessage Login(StandartAuthRequest request)
        {
            try
            {
                // Check request and request props is not null
                CheckVal.IsNull(request, nameof(request));
                CheckVal.IsNull(request.Password, nameof(request.Password));
                // Find User
                var user = CheckData.UserExist(_context, false, email: request.Email, name: request.Name);
                if (user == null)
                    throw new ApiException(ApiResult.Validation, ErrorType.UserWithEmailorNameNotFound,
                        request.Email ?? request.Name);
                // Check if User is Blocked
                CheckData.IsUserBlocked(_context, null, user);
                // Check if User Account Exist
                var account = CheckData.AccountExist(_context, Provider.Standart, user.Id);
                // Check password
                BaseAuth.CheckPassword(user, request.Password);


                var token = BaseAuth.CreateToken(user.Id);

                var userDto = _mapper.Map<User, UserDTO>(user);

                // Generate AuthenticationToken
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Response<LoginResult>(ApiResult.Ok, new LoginResult(token, userDto)));
            }
            catch (ApiException ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Response<LoginResult>(ex.ApiResult, null, new ErrorResponse(ex.ErrorType, ex.Message, ex)));
            }
            catch (EntitySqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Response<LoginResult>(ApiResult.Sql, null, new ErrorResponse(ErrorType.None, ex.Message, ex)));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Response<LoginResult>(ApiResult.Unknown, null,
                        new ErrorResponse(ErrorType.Internal, ex.Message, ex)));
            }
        }
    }
}