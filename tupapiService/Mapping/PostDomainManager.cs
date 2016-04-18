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