using hotelEase.Model;
using hotelEase.Model.SearchObjects;
using hotelEase.Services.Database;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public abstract class BaseService<TModel, TSearch, TDbEntity> : IService<TModel, TSearch> where TSearch : BaseSearchObject where TDbEntity : class where TModel : class
    {
        public HotelEaseContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public BaseService(HotelEaseContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public PagedResult<TModel> GetPaged(TSearch searchObject)
        {
            List<TModel> result = new List<TModel>();

            var query = Context.Set<TDbEntity>().AsQueryable();

            query = AddInclude(searchObject ,query);

            query = AddFilter(searchObject, query);

            var PropIsDeleted = typeof(TDbEntity).GetProperty("IsDeleted");

            if (PropIsDeleted != null)
            {
                query = query.Where(e => !EF.Property<bool?>(e, "IsDeleted").HasValue || EF.Property<bool?>(e, "IsDeleted") == false);
            }


            int count = query.Count();

            
            if(searchObject?.Page.HasValue == true && searchObject?.PageSize.HasValue == true)
            {
                query = query.Skip(searchObject.Page.Value * searchObject.PageSize.Value).Take(searchObject.PageSize.Value);
            }

            var list = query.ToList();

            result = Mapper.Map(list, result);

            PagedResult<TModel> pagedResult = new PagedResult<TModel>();

            pagedResult.ResultList = result;
            pagedResult.Count = count;

            return pagedResult;
        }

        public virtual IQueryable<TDbEntity> AddFilter(TSearch search, IQueryable<TDbEntity> query)
        {
            return query;
        }

        public TModel GetById(int id)
        {
            var entity = Context.Set<TDbEntity>().Find(id);

            var PropIsDeleted = typeof(TDbEntity).GetProperty("IsDeleted");

            if (entity != null && (PropIsDeleted != null || (bool)PropIsDeleted.GetValue(entity) == false)) 
            {
                return Mapper.Map<TModel>(entity);
            }
            else
            {
                return null;
            }

        }

        public virtual IQueryable<TDbEntity> AddInclude(TSearch search, IQueryable<TDbEntity> query)
        {
            return query;
        }
    }
}
