using System;
using System.Data.Entity.Core;
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
                var claimsPrincipal = User as ClaimsPrincipal;
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

                var response = new Response<PostResponse>(ApiResult.Ok, new PostResponse {Id = id});
                using (var storage = new AzureStorage())
                {
                    response.Data.Sas = storage.GetPostsSas();
                }


                _context.Posts.Add(dbPost);
                _context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
            catch (ApiException ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Response<PostResponse>(ex.ApiResult, null, new ErrorResponse(ex.ErrorType, ex.Message, ex)));
            }
            catch (EntitySqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Response<PostResponse>(ApiResult.Sql, null, new ErrorResponse(ErrorType.None, ex.Message, ex)));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Response<PostResponse>(ApiResult.Unknown, null,
                        new ErrorResponse(ErrorType.Internal, ex.Message, ex)));
            }
        }
    }
}