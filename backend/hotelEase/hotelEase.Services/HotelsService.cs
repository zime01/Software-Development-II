using Azure.Core;
using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services.Database;
using hotelEase.Services.HotelsStateMachine;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class HotelsService : BaseCRUDService<Model.Hotel, HotelsSearchObject, Database.Hotel, HotelsInsertRequest, HotelsUpdateRequest>, IHotelsService
    {
        public BaseHotelsState BaseHotelsState { get; set; }
        public HotelsService(HotelEaseContext context, IMapper mapper, BaseHotelsState baseHotelsState) : base(context, mapper)
        {
            BaseHotelsState = baseHotelsState;
        }

        public override IQueryable<Database.Hotel> AddFilter(HotelsSearchObject search, IQueryable<Database.Hotel> query)
        {
            var filteredQuery = base.AddFilter(search, query);

            if (!string.IsNullOrWhiteSpace(search?.FTS))
            {
                filteredQuery = filteredQuery.Where(x=>x.Name.Contains(search.FTS) || x.Description.Contains(search.FTS));
            }

            return filteredQuery;
        }

        public override Model.Hotel Insert(HotelsInsertRequest request)
        {
            var state = BaseHotelsState.CreateState("initial");
            
            return state.Insert(request);
        }

        public override Model.Hotel Update(int id, HotelsUpdateRequest request)
        {
            var entity = GetById(id);
            var state = BaseHotelsState.CreateState(entity.StateMachine);
            return state.Update(id, request);
        }

        public Model.Hotel Activate(int id)
        {
            var entity = GetById(id);
            var state = BaseHotelsState.CreateState(entity.StateMachine);
            return state.Activate(id);
        }

        public Model.Hotel Edit(int id)
        {
            var entity = GetById(id);
            var state = BaseHotelsState.CreateState(entity.StateMachine);
            return state.Edit(id);
        }

        public Model.Hotel Hide(int id)
        {
            var entity = GetById(id);
            var state = BaseHotelsState.CreateState(entity.StateMachine);
            return state.Hide(id);
        }

        public List<string> AllowedActions(int id)
        {
            if(id <= 0)
            {
                var state = BaseHotelsState.CreateState("initial");
                return state.AllowedActions(null);
            }
            else
            {
                var entity = Context.Hotels.Find(id);
                var state = BaseHotelsState.CreateState(entity.StateMachine);
                return state.AllowedActions(entity);
            }
        }
        //public List<Model.Hotel> List = new List<Model.Hotel>()
        //{
        //    new Model.Hotel()
        //    {
        //        HotelId = 1,
        //        Name = "Hilton",
        //        Price = 149,
        //    },
        //    new Model.Hotel()
        //    {
        //        HotelId = 2,
        //        Name = "Swisotel",
        //        Price = 130,
        //    }

        //};
        //public Model.PagedResult<Model.Hotel> GetList(HotelsSearchObject searchObject)
        //{
        //    List<Model.Hotel> result = new List<Model.Hotel>();


        //    var query = Context.Hotels.AsQueryable();

        //    if(!string.IsNullOrWhiteSpace(searchObject.FTS))
        //    {
        //        query = query.Where(x=>x.Name.Contains(searchObject.FTS) ||  x.Description.Contains(searchObject.FTS));
        //    }

        //    if(searchObject.IsRoomsIcluded == true)
        //    {
        //        query = query.Include(x => x.Rooms);
        //    }

        //    int count = query.Count();

        //    if (!string.IsNullOrWhiteSpace(searchObject.OrderBy))
        //    {
        //        query = query.OrderBy(searchObject.OrderBy);
        //    }

        //    if (searchObject?.Page.HasValue == true && searchObject?.PageSize.HasValue == true)
        //    {
        //        query = query.Skip(searchObject.Page.Value * searchObject.PageSize.Value).Take(searchObject.PageSize.Value);
        //    }



        //    var list = query.ToList();




        //    var resultList = Mapper.Map(list, result);

        //    Model.PagedResult<Model.Hotel> response = new Model.PagedResult<Model.Hotel>();

        //    response.ResultList = resultList;
        //    response.Count = count;


        //    return response ;
        //}
    }
}
