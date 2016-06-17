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
    public class VoteDomainManager : GenericMappedEntityDomainManager<VoteDTO, Vote>
    {
        private readonly string _userId;

        public VoteDomainManager(TupapiContext context, HttpRequestMessage request)
            : base(context, request)
        {
            _userId = BaseAuth.GetUserId();
        }

        public VoteDomainManager(TupapiContext context, HttpRequestMessage request, bool enableSoftDelete)
            : base(context, request, enableSoftDelete)
        {
            _userId = BaseAuth.GetUserId();
        }

        public override IQueryable<VoteDTO> Query()
        {
            var query = Context.Set<Vote>().Where(item => item.UserId == _userId).ProjectTo<VoteDTO>(_config);

            query = TableUtils.ApplyDeletedFilter(query, IncludeDeleted);
            return query;
        }

        public override SingleResult<VoteDTO> Lookup(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            var query =
                Context.Set<Vote>().Where(item => item.Id == id && item.UserId == _userId).ProjectTo<VoteDTO>(_config);
            query = TableUtils.ApplyDeletedFilter(query, IncludeDeleted);
            return SingleResult.Create(query);
        }

        public override async Task<VoteDTO> InsertAsync(VoteDTO item)
        {
            item.UserId = _userId;
            return await base.InsertAsync(item);
        }

        public override Task<VoteDTO> UpdateAsync(string id, Delta<VoteDTO> patch)
        {
            var item = Context.Set<Vote>().SingleOrDefault(x => x.Id == id);
            if (item.UserId != _userId)
            {
                throw new ApiException(ApiResult.Denied, ErrorType.NotOwner, null);
            }
            return UpdateEntityAsync(patch, id);
        }


        public override Task<bool> DeleteAsync(string id)
        {
            var item = Context.Set<Vote>().SingleOrDefault(x => x.Id == id);
            if (item.UserId != _userId)
            {
                throw new ApiException(ApiResult.Denied, ErrorType.NotOwner, null);
            }
            return DeleteItemAsync(id);
        }
    }
}