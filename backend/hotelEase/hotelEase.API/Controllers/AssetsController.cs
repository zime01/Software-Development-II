using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;

namespace hotelEase.API.Controllers
{
    public class AssetsController : BaseCRUDController<Model.Asset, AssetsSearchObject, AssetsUpsertRequest, AssetsUpsertRequest>
    {
        public AssetsController(IAssetsService service) : base(service) { }
    }
}
