using System.Linq;
using System.Net.Http;
using AutoMapper.QueryableExtensions;
using Microsoft.Azure.Mobile.Server.Tables;
using tupapi.Shared.Enums;
using tupapiService.Authentication;
using tupapiService.DataObjects;
using tupapiService.Models;

namespace tupapiService.Mapping
{
    public class QueuePostDomainManager : GenericMappedEntityDomainManager<PostDTO, Post>
    {
        private readonly string _userId;

        public QueuePostDomainManager(TupapiContext context, HttpRequestMessage request)
            : base(context, request)
        {
            _userId = BaseAuth.GetUserId();
        }

        public QueuePostDomainManager(TupapiContext context, HttpRequestMessage request, bool enableSoftDelete)
            : base(context, request, enableSoftDelete)
        {
            _userId = BaseAuth.GetUserId();
        }

        public override IQueryable<PostDTO> Query()
        {
            var query =
                Context.Set<Post>()
                    .Where(item => item.Status == PhotoStatus.Running && item.Votes.Count(v => v.UserId == _userId) > 0)
                    .OrderBy(o => o.CreatedAt)
                    .ProjectTo<PostDTO>(_config);

            query = TableUtils.ApplyDeletedFilter(query, IncludeDeleted);
            return query;
        }
    }
}