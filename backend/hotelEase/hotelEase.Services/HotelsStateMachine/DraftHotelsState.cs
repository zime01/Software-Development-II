using Azure.Core;
using hotelEase.Model.Requests;
using hotelEase.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services.HotelsStateMachine
{
    public class DraftHotelsState : BaseHotelsState
    {
        public DraftHotelsState(HotelEaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override Model.Hotel Update(int id, HotelsUpdateRequest request)
        {
            var set = Context.Set<Hotel>();

            var entity = set.Find(id);

            Mapper.Map(request, entity);

            Context.SaveChanges();

            return Mapper.Map<Model.Hotel>(entity);
        }

        public override Model.Hotel Activate(int id)
        {
            var set = Context.Set<Hotel>();

            var entity = set.Find(id);

            entity.StateMachine = "active";

            Context.SaveChanges();

            return Mapper.Map<Model.Hotel>(entity);
        }

        public override Model.Hotel Hide(int id)
        {
            var set = Context.Set<Hotel>();

            var entity = set.Find(id);

            entity.StateMachine = "hidden";

            Context.SaveChanges();

            return Mapper.Map<Model.Hotel>(entity);
        }

        public override List<string> AllowedActions(Hotel entity)
        {
            return new List<string>() { nameof(Activate), nameof(Update), nameof(Hide) };
        }
    }
}
