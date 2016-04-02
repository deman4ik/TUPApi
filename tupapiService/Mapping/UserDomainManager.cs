using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using AutoMapper.QueryableExtensions;
using Microsoft.Azure.Mobile.Server.Tables;
using tupapi.Shared.Enums;
using tupapiService.DataObjects;
using tupapiService.Helpers.ExceptionHelpers;
using tupapiService.Models;

namespace tupapiService.Mapping
{
    public class UserDomainManager : GenericMappedEntityDomainManager<UserDTO, User>
    {
        public UserDomainManager(TupapiContext context, HttpRequestMessage request, string userId)
            : base(context, request, userId)
        {
        }

        public UserDomainManager(TupapiContext context, HttpRequestMessage request, string userId, bool enableSoftDelete)
            : base(context, request, userId, enableSoftDelete)
        {
        }

        public override IQueryable<UserDTO> Query()
        {
            var query = Context.Set<User>().Where(u => u.Id == _userId).ProjectTo<UserDTO>(_config);
            query = TableUtils.ApplyDeletedFilter(query, IncludeDeleted);
            return query;
        }

        public override SingleResult<UserDTO> Lookup(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (id != _userId)
            {
                throw new ApiException(ApiResult.Denied, ErrorType.NotOwner, id);
            }
            var query = Context.Set<User>().Where(item => item.Id == _userId).ProjectTo<UserDTO>(_config);
            query = TableUtils.ApplyDeletedFilter(query, IncludeDeleted);
            return SingleResult.Create(query);
        }

        public override Task<UserDTO> UpdateAsync(string id, Delta<UserDTO> patch)
        {
            if (id != _userId)
            {
                throw new ApiException(ApiResult.Denied, ErrorType.NotOwner, id);
            }
            return UpdateEntityAsync(patch, id);
        }


        public override Task<bool> DeleteAsync(string id)
        {
            if (id != _userId)
            {
                throw new ApiException(ApiResult.Denied, ErrorType.NotOwner, id);
            }
            return DeleteItemAsync(id);
        }
    }
}