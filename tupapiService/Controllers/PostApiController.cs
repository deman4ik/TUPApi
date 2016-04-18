using System;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using AutoMapper;
using Microsoft.Azure.Mobile.Server.Config;
using tupapi.Shared.DataObjects;
using tupapi.Shared.Enums;
using tupapi.Shared.Helpers;
using tupapiService.Authentication;
using tupapiService.DataObjects;
using tupapiService.Helpers;
using tupapiService.Helpers.CheckHelpers;
using tupapiService.Helpers.ExceptionHelpers;
using tupapiService.Helpers.StorageHelpers;
using tupapiService.Models;
using Post = tupapiService.Models.Post;

namespace tupapiService.Controllers
{
    [MobileAppController]
    [Authorize]
    public class PostApiController : ApiController
    {
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ITupapiContext _context;

        public PostApiController()
        {
            _context = new TupapiContext();
            _config = Mapping.Mapping.GetConfiguration();
            _mapper = _config.CreateMapper();
        }

        public PostApiController(ITupapiContext context)
        {
            _context = context;
            _config = Mapping.Mapping.GetConfiguration();
            _mapper = _config.CreateMapper();
        }

        [HttpPost]
        public HttpResponseMessage Post(PostDTO post)
        {
            try
            {
                var claimsPrincipal = this.User as ClaimsPrincipal;
                var user = BaseAuth.GetUser(_context, claimsPrincipal);
                var id = SequentialGuid.NewGuid();
                Post dbPost = new Post
                {
                    UserId = user.Id,
                    Description = post.Description,
                    Status = PhotoStatus.Planned,
                    Type = CheckData.GetPhotoType(user.Type),
                    Id = id,
                    PhotoUrl = Const.StorageBaseUrl + Const.StoragePostsContainer + "/" + id + ".jpeg"
                };

                var response = new PostResponse {Id = id};
                using (var storage = new AzureStorage())
                {
                    response.Sas = storage.GetPostsSas();
                }


                _context.Posts.Add(dbPost);
                _context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
            catch (ApiException ex)
            {
                Debug.WriteLine(ex);
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new BaseResponse(ex.ApiResult, ex.ErrorType, ex.Message));
            }
            catch (EntitySqlException ex)
            {
                Debug.WriteLine(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new BaseResponse(ApiResult.Sql, ErrorType.None, ex.Message));
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new BaseResponse(ApiResult.Unknown, ErrorType.Internal, ex.Message));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new BaseResponse(ApiResult.Unknown, ErrorType.Internal, ex.Message));
            }
        }
    }
}