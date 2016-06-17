using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using AutoMapper.QueryableExtensions;
using Microsoft.Azure.Mobile.Server.Tables;
using tupapi.Shared.Enums;
using tupapiService.Authentication;
using tupapiService.DataObjects;
using tupapiService.Helpers.ExceptionHelpers;
using tupapiService.Models;

namespace tupapiService.Mapping
{
    public class UserPostDomainManager : GenericMappedEntityDomainManager<PostDTO, Post>
    {
        private readonly string _userId;
        public UserPostDomainManager(TupapiContext context, HttpRequestMessage request)
            : base(context, request)
        {
            _userId = BaseAuth.GetUserId();
        }

        public UserPostDomainManager(TupapiContext context, HttpRequestMessage request, bool enableSoftDelete)
            : base(context, request, enableSoftDelete)
        {
            _userId = BaseAuth.GetUserId();
        }

        public override IQueryable<PostDTO> Query()
        {
            var query = Context.Set<Post>().Where(item => item.UserId == _userId).ProjectTo<PostDTO>(_config);

            query = TableUtils.ApplyDeletedFilter(query, IncludeDeleted);
            return query;
        }

        public override SingleResult<PostDTO> Lookup(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            var query = Context.Set<Post>().Where(item => item.Id == id && item.UserId == _userId).ProjectTo<PostDTO>(_config);
            query = TableUtils.ApplyDeletedFilter(query, IncludeDeleted);
            return SingleResult.Create(query);
        }

        public override async Task<PostDTO> InsertAsync(PostDTO item)
        {
            item.UserId = _userId;
            item.Status = PhotoStatus.Planned;
            return await base.InsertAsync(item);
        }

        public override Task<PostDTO> UpdateAsync(string id, Delta<PostDTO> patch)
        {
            var item = Context.Set<Post>().SingleOrDefault(x => x.Id == id);
            if (item.UserId != _userId)
            {
                throw new ApiException(ApiResult.Denied, ErrorType.NotOwner, null);
            }
            return UpdateEntityAsync(patch, id);
        }


        public override Task<bool> DeleteAsync(string id)
        {
            var item = Context.Set<Post>().SingleOrDefault(x => x.Id == id);
            if (item.UserId != _userId)
            {
                throw new ApiException(ApiResult.Denied, ErrorType.NotOwner, null);
            }
            return DeleteItemAsync(id);
        }
    }
}