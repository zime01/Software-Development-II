using hotelEase.Model.Requests;
using hotelEase.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace hotelEase.Services.HotelsStateMachine
{
    public class BaseHotelsState
    {
        public HotelEaseContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        public BaseHotelsState(HotelEaseContext context, IMapper mapper, IServiceProvider serviceProvider)
        {
            Context = context;
            Mapper = mapper;    
            ServiceProvider = serviceProvider;

        }
        public virtual Model.Hotel Insert(HotelsInsertRequest request)
        {
            throw new Exception("Method not allowed");
        }

        public virtual Model.Hotel Update(int id, HotelsUpdateRequest request)
        {
            throw new Exception("Method not allowed");
        }

        public virtual Model.Hotel Activate(int id)
        {
            throw new Exception("Method not allowed");

        }

        public virtual Model.Hotel Hide(int id)
        {
            throw new Exception("Method not allowed");
        }

        public virtual Model.Hotel Edit(int id)
        {
            throw new Exception("Method not allowed");
        }
        
        public virtual List<string> AllowedActions(Hotel entity)
        {
            throw new Exception("Method not allowed");
        }

        public BaseHotelsState CreateState(string stateName)
        {
            switch (stateName)
            {
                case "initial":
                    return ServiceProvider.GetService<InitialHotelState>();
                case "draft":
                    return ServiceProvider.GetService<DraftHotelsState>();
                case "active":
                    return ServiceProvider.GetService<ActiveHotelsState>();
                case "hidden":
                    return ServiceProvider.GetService<HiddenHotelsState>();
                default:throw new Exception("State not recognized");
            }
        }
    }
}


//initial, draft, active, hidden, active