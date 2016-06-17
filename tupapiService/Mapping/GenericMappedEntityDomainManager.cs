using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using tupapiService.Models;

namespace tupapiService.Mapping
{
    public class GenericMappedEntityDomainManager<TData, TModel> : MappedEntityDomainManager<TData, TModel>
        where TData : class, ITableData
        where TModel : class, ITableData
    {
        protected readonly MapperConfiguration _config;

        public GenericMappedEntityDomainManager(TupapiContext context, HttpRequestMessage request)
            : base(context, request)
        {
            // Request = request;
            _config = Mapping.GetConfiguration();
        }

        public GenericMappedEntityDomainManager(TupapiContext context, HttpRequestMessage request,
            bool enableSoftDelete)
            : base(context, request, enableSoftDelete)
        {
            // Request = request;
            // EnableSoftDelete = enableSoftDelete;
            _config = Mapping.GetConfiguration();
        }

        public override IQueryable<TData> Query()
        {
            var query = Context.Set<TModel>().ProjectTo<TData>(_config);
            query = TableUtils.ApplyDeletedFilter(query, IncludeDeleted);
            return query;
        }

        public override SingleResult<TData> Lookup(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            var query = Context.Set<TModel>().Where(item => item.Id == id).ProjectTo<TData>(_config);
            query = TableUtils.ApplyDeletedFilter(query, IncludeDeleted);
            return SingleResult.Create(query);
        }

       

        public override Task<TData> UpdateAsync(string id, Delta<TData> patch)
        {
            return UpdateEntityAsync(patch, id);
        }


        public override Task<bool> DeleteAsync(string id)
        {
            return DeleteItemAsync(id);
        }
    }
}