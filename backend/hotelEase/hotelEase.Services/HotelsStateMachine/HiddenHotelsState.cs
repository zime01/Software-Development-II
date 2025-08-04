using hotelEase.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services.HotelsStateMachine
{
    public class HiddenHotelsState : BaseHotelsState
    {
        public HiddenHotelsState(HotelEaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override Model.Hotel Edit(int id)
        {
            var set = Context.Set<Hotel>();

            var entity = set.Find(id);

            entity.StateMachine = "draft";

            Context.SaveChanges();

            return Mapper.Map<Model.Hotel>(entity);
        }

        public override List<string> AllowedActions(Hotel entity)
        {
            return new List<string>() { nameof(Edit) };
        }
    }
}
