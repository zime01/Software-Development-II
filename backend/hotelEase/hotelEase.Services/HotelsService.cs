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

            if (!string.IsNullOrWhiteSpace(search?.CityName))
            {
                filteredQuery = filteredQuery.Include(x=>x.City).Where(x=>x.City.Name.Contains(search.CityName));
            }

            if (!string.IsNullOrWhiteSpace(search?.CityName))
            {
                filteredQuery = filteredQuery.Include(x => x.City)
                                             .Where(x => x.City.Name.Contains(search.CityName));
            }

            if (search.Adults.HasValue || search.RoomsCount.HasValue || (search.CheckInDate.HasValue && search.CheckOutDate.HasValue))
            {
                filteredQuery = filteredQuery
                    .Include(h => h.Rooms)
                        .ThenInclude(r => r.Reservations)
                    .Where(h =>
                        h.Rooms.Any(r =>
                            (search.Adults == null || r.Capacity >= search.Adults) &&
                            (search.RoomsCount == null || h.Rooms.Count() >= search.RoomsCount) &&
                            (search.CheckInDate == null || search.CheckOutDate == null ||
                             !r.Reservations.Any(res =>
                                 (search.CheckInDate < res.CheckOutDate) &&
                                 (search.CheckOutDate > res.CheckInDate)
                             ))
                        )
                    );
            }

            // Filter by price (najniža cijena sobe u hotelu)
            if (search.MinPrice.HasValue)
                filteredQuery = filteredQuery.Where(h => h.Rooms.Any(r => r.PricePerNight >= search.MinPrice.Value));

            if (search.MaxPrice.HasValue)
                filteredQuery = filteredQuery.Where(h => h.Rooms.Any(r => r.PricePerNight <= search.MaxPrice.Value));

            // Filter by star rating
            if (search.MinStarRating.HasValue)
                filteredQuery = filteredQuery.Where(h => h.StarRating >= search.MinStarRating.Value);

            // Amenities
            if (search.WiFi == true) filteredQuery = filteredQuery.Where(h => h.WiFi == true);
            if (search.Parking == true) filteredQuery = filteredQuery.Where(h => h.Parking == true);
            if (search.Pool == true) filteredQuery = filteredQuery.Where(h => h.Pool == true);
            if (search.Bar == true) filteredQuery = filteredQuery.Where(h => h.Bar == true);
            if (search.Fitness == true) filteredQuery = filteredQuery.Where(h => h.Fitness == true);
            if (search.SPA == true) filteredQuery = filteredQuery.Where(h => h.Spa == true);

            // Filtriranje
            if (!string.IsNullOrWhiteSpace(search.FilterBy))
            {
                switch (search.FilterBy.ToLower())
                {
                    case "wifi":
                        filteredQuery = filteredQuery.Where(h => h.WiFi == true);
                        break;
                    case "parking":
                        filteredQuery = filteredQuery.Where(h => h.Parking == true);
                        break;
                    case "pool":
                        filteredQuery = filteredQuery.Where(h => h.Pool == true);
                        break;
                    case "bar":
                        filteredQuery = filteredQuery.Where(h => h.Bar == true);
                        break;
                    case "fitness":
                        filteredQuery = filteredQuery.Where(h => h.Fitness == true);
                        break;
                    case "spa":
                        filteredQuery = filteredQuery.Where(h => h.Spa == true);
                        break;
                }
            }

            // Sortiranje
            if (!string.IsNullOrWhiteSpace(search.SortBy))
            {
                switch (search.SortBy.ToLower())
                {
                    case "price_asc":
                        filteredQuery = filteredQuery.OrderBy(h => h.Rooms.Min(r => r.PricePerNight));
                        break;
                    case "price_desc":
                        filteredQuery = filteredQuery.OrderByDescending(h => h.Rooms.Min(r => r.PricePerNight));
                        break;
                    case "rating":
                        filteredQuery = filteredQuery.OrderByDescending(h => h.StarRating);
                        break;
                }
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

        public override IQueryable<Database.Hotel> AddInclude(HotelsSearchObject search, IQueryable<Database.Hotel> query)
        {
            if (search.IsRoomsIcluded == true)
            {
                query = query.Include(x => x.Rooms).ThenInclude(x => x.Assets);
            }

            return query;
        }

        private void SetAveragePrice(Model.Hotel hotel, Database.Hotel dbHotel)
        {
            if (dbHotel.Rooms != null && dbHotel.Rooms.Any())
            {
                hotel.Price = dbHotel.Rooms
                    .Where(r => r.IsDeleted != true) // soft delete check
                    .DefaultIfEmpty()
                    .Average(r => r == null ? 0 : r.PricePerNight);
            }
            else
            {
                hotel.Price = 0;
            }
        }

        public new Model.Hotel GetById(int id)
        {
            var dbHotel = Context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefault(h => h.Id == id && (h.IsDeleted == false || h.IsDeleted == null));

            if (dbHotel == null) return null;

            var hotel = Mapper.Map<Model.Hotel>(dbHotel);
            SetAveragePrice(hotel, dbHotel);

            return hotel;
        }
        public override Model.PagedResult<Model.Hotel> GetPaged(HotelsSearchObject searchObject)
        {
            var pagedResult = base.GetPaged(searchObject);

            for (int i = 0; i < pagedResult.ResultList.Count; i++)
            {
                // Moramo učitati sobe da izračunamo prosjek
                var dbHotel = Context.Hotels
                    .Include(h => h.Rooms)
                    .FirstOrDefault(h => h.Id == pagedResult.ResultList[i].Id);

                if (dbHotel != null)
                    SetAveragePrice(pagedResult.ResultList[i], dbHotel);
            }

            return pagedResult;
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
