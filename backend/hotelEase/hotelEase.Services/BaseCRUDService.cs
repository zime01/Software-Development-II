﻿using hotelEase.Model.SearchObjects;
using hotelEase.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public abstract class BaseCRUDService<TModel, TSearch, TDbEntity, TInsert, TUpdate> : BaseService<TModel, TSearch, TDbEntity> where TModel : class where TSearch : BaseSearchObject where TDbEntity : class
    {
        public BaseCRUDService(HotelEaseContext context, IMapper mapper) : base(context, mapper)
        {
            
        }

        public virtual TModel Insert(TInsert request)
        {
            TDbEntity entity = Mapper.Map<TDbEntity>(request);

            BeforeInsert(request, entity);

            Context.Add(entity);
            Context.SaveChanges();


            return Mapper.Map<TModel>(entity);
        }

        public virtual void BeforeInsert(TInsert request, TDbEntity entity)
        {

        }

        public virtual TModel Update(int id, TUpdate request)
        {
            var set = Context.Set<TDbEntity>();

            var entity = set.Find(id);

            Mapper.Map(request, entity);

            BeforeUpdate(request, entity);

            Context.SaveChanges();

            return Mapper.Map<TModel>(entity);
        }

        public virtual void BeforeUpdate(TUpdate request, TDbEntity entity)
        {

        }

        public virtual TModel Delete (int id)
        {
            var set = Context.Set<TDbEntity>();

            var entity = set.Find(id);

            if(entity == null)
            {
                throw new Exception("Item not found");
            }

            var IsDeletedProp = typeof(TDbEntity).GetProperty("IsDeleted");
            var DeletedTimeProp = typeof(TDbEntity).GetProperty("DeletedTime");

            if(IsDeletedProp == null  || DeletedTimeProp == null)
            {
                throw new Exception("Entity does not contain soft delete properties");
            }

            IsDeletedProp.SetValue(entity, true);
            DeletedTimeProp.SetValue(entity, DateTime.Now);

            Context.SaveChanges();

            return Mapper.Map<TModel>(entity);
        }
    }
}
