using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotelEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : BaseCRUDController<Model.Role, RolesSearchObject, RolesUpsertRequest, RolesUpsertRequest>
    {
        
        public RolesController(IRolesService service) : base(service)
        {
        }
    }
}
