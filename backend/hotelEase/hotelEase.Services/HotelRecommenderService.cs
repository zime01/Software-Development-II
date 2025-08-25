using hotelEase.Services.Database;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class HotelRecommenderService
    {
        private readonly HotelEaseContext _context;
        private readonly IMapper _mapper;
        private readonly MLContext _mlContext;

        public HotelRecommenderService(HotelEaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _mlContext = new MLContext();
        }

        // ===============================
        // 1) Popular Hotels (baseline)
        // ===============================
        public List<Model.Hotel> GetPopularHotels(int top = 5) { 
           return _context.Hotels.Include(h => h.Rooms).
                                    ThenInclude(r => r.Reservations)
                                 .Include(h=>h.Rooms)
                                    .ThenInclude(r=>r.Assets)
                            .OrderByDescending(h => h.Rooms.Sum(r => r.Reservations.Count))
                            .Take(top).Select(h => _mapper.Map<Model.Hotel>(h)).ToList(); }

        // ===============================
        // 2) Content-Based Filtering
        // ===============================
        public List<Model.Hotel> GetContentBased(int hotelId, int top = 5)
        {
            var selectedHotel = _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefault(h => h.Id == hotelId);

            if (selectedHotel == null) return new List<Model.Hotel>();

            var selectedAvgPrice = selectedHotel.Rooms.Any()
                ? selectedHotel.Rooms.Average(r => r.PricePerNight)
                : 0;

            return _context.Hotels
                .Include(h => h.Rooms)
                .ThenInclude(h=>h.Assets)
                .Where(h => h.CityId == selectedHotel.CityId && h.Id != hotelId)
                .Select(h => new
                {
                    Hotel = h,
                    AvgPrice = h.Rooms.Any() ? h.Rooms.Average(r => r.PricePerNight) : 0
                })
                .OrderBy(h => Math.Abs(h.AvgPrice - selectedAvgPrice)) // slična cijena
                .ThenByDescending(h => h.Hotel.StarRating)             // prioritet veće zvjezdice
                .Take(top)
                .Select(h => _mapper.Map<Model.Hotel>(h.Hotel))
                .ToList();
        }

        // ===============================
        // 3) Collaborative Filtering (ML.NET)
        // ===============================
        public List<Model.Hotel> GetCollaborativeFiltering(int userId, int top = 5)
        {
            // pripremi interakcije (UserId, HotelId, Rating)
            var data = _context.Reviews
                .Where(r => r.Rating > 0)
                .Select(r => new HotelRating
                {
                    UserId = (uint)r.UserId,
                    HotelId = (uint)r.HotelId,
                    Label = (float)r.Rating
                }).ToList();

            if (!data.Any())
                return new List<Model.Hotel>();

            var trainingData = _mlContext.Data.LoadFromEnumerable(data);

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = nameof(HotelRating.UserId),
                MatrixRowIndexColumnName = nameof(HotelRating.HotelId),
                LabelColumnName = nameof(HotelRating.Label),
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var pipeline = _mlContext.Recommendation().Trainers.MatrixFactorization(options);
            var model = pipeline.Fit(trainingData);

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<HotelRating, HotelRatingPrediction>(model);

            // sve hotele
            var allHotels = _context.Hotels.Include(h=>h.Rooms).ThenInclude(r=>r.Assets).ToList();
            var scoredHotels = allHotels.Select(h =>
            {
                var prediction = predictionEngine.Predict(new HotelRating
                {
                    UserId = (uint)userId,
                    HotelId = (uint)h.Id
                });

                return new { Hotel = h, Score = prediction.Score };
            })
            .OrderByDescending(x => x.Score)
            .Take(top)
            .Select(x => _mapper.Map<Model.Hotel>(x.Hotel))
            .ToList();

            return scoredHotels;
        }

        // ML.NET input/output klase
        public class HotelRating
        {
            [KeyType(count: 10000)]  // predpostavi max broj usera
            public uint UserId { get; set; }

            [KeyType(count: 10000)]  // predpostavi max broj hotela
            public uint HotelId { get; set; }

            public float Label { get; set; }
        }

        public class HotelRatingPrediction
        {
            public float Score { get; set; }
        }
    }

}
