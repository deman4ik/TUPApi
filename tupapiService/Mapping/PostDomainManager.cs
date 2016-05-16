using System.Net.Http;
using tupapiService.DataObjects;
using tupapiService.Models;

namespace tupapiService.Mapping
{
    public class PostDomainManager : GenericMappedEntityDomainManager<PostDTO, Post>
    {
        public PostDomainManager(TupapiContext context, HttpRequestMessage request)
            : base(context, request)
        {
        }

        public PostDomainManager(TupapiContext context, HttpRequestMessage request, bool enableSoftDelete)
            : base(context, request, enableSoftDelete)
        {
        }
    }
}