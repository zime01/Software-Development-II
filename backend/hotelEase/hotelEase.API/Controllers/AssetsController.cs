using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services;
using hotelEase.Services.Database;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace hotelEase.API.Controllers
{
    public class AssetsController : BaseCRUDController<Model.Asset, AssetsSearchObject, AssetsUpsertRequest, AssetsUpsertRequest>
    {
        private readonly HotelEaseContext _context;
        private readonly IAssetsService _assetsService;

        public AssetsController(IAssetsService service, HotelEaseContext context) : base(service)
        {
            _assetsService = service;
            _context = context;
        }

        [HttpGet("ByHotel/{hotelId}")]
        public IActionResult GetByHotelId(int hotelId)
        {
            var search = new AssetsSearchObject { HotelId = hotelId };
            var result = (_service as IAssetsService).GetPaged(search);
            return Ok(result);
        }

        [HttpGet("ByRoom/{roomId}")]
        public IActionResult GetByRoomId(int roomId)
        {
            var search = new AssetsSearchObject
            {
                RoomId = roomId,
                IsDeleted = false
            };

            var result = (_service as IAssetsService).GetPaged(search);
            return Ok(result);
        }

        [HttpPost("delete-assets")]
        public IActionResult DeleteAssets([FromBody] List<int> assetIds)
        {
            foreach (var id in assetIds)
            {
                var asset = _context.Assets.Find(id);
                if (asset != null)
                {
                    asset.IsDeleted = true;
                    asset.DeletedTime = DateTime.Now;
                }
            }
            _context.SaveChanges();
            return Ok(new { Message = "Assets deleted successfully." });
        }

        [HttpPost("insert-assets")]
        public IActionResult InsertAssets([FromBody] List<AssetsUpsertRequest> assets)
        {
            var insertedAssets = new List<Model.Asset>();
            foreach (var assetReq in assets)
            {
                var asset = (_service as IAssetsService).Insert(assetReq);
                insertedAssets.Add(asset);
            }
            return Ok(insertedAssets);
        }
    }
}
