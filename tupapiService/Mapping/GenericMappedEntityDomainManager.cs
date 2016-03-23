using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using AutoMapper.QueryableExtensions;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using tupapiService.Models;

namespace tupapiService.Mapping
{
    public class GenericMappedEntityDomainManager<TData, TModel>  : MappedEntityDomainManager<TData, TModel>
          where TData : class, ITableData
        where TModel : class, ITableData
    {
        private ITupapiContext _context;
        public GenericMappedEntityDomainManager(TupapiContext context, HttpRequestMessage request) : base(context, request)
        {
            Request = request;
            this._context = context;
        }

        public GenericMappedEntityDomainManager(TupapiContext context, HttpRequestMessage request, bool enableSoftDelete) : base(context, request, enableSoftDelete)
        {
            Request = request;
            this._context = context;
            this.EnableSoftDelete = enableSoftDelete;
        }

        public override IQueryable<TData> Query()
        {
            IQueryable<TData> query = this.Context.Set<TModel>().ProjectTo<TData>(); query = TableUtils.ApplyDeletedFilter(query, this.IncludeDeleted); return query;
        }

        public override SingleResult<TData> Lookup(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            IQueryable<TData> query = this.Context.Set<TModel>().Where(item => item.Id == id).ProjectTo<TData>();
            query = TableUtils.ApplyDeletedFilter(query, this.IncludeDeleted);
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