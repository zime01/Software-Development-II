using hotelEase.Model;
using hotelEase.Model.Requests;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services.HotelsStateMachine
{
    public class InitialHotelState : BaseHotelsState
    {
        public InitialHotelState(Database.HotelEaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override Hotel Insert(HotelsInsertRequest request)
        {
            var set = Context.Set<Database.Hotel>();

            var entity = Mapper.Map<Database.Hotel>(request);

            entity.StateMachine = "draft";

            set.Add(entity);

            Context.SaveChanges();

            return Mapper.Map<Hotel>(entity);    
        }

        public override List<string> AllowedActions(Database.Hotel entity)
        {
            return new List<string>() { nameof(Insert) };
        }

       
    }
}
