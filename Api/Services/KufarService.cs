using System.Data;
using System.Globalization;
using System.Text.Json;
using Api.Models;
using Api.Models.Ads;
using Api.Models.Stats;
using Api.Services.Interfaces;
using Api.Utils;

namespace Api.Services
{
    public class KufarService(IKufarClient kufarClient, IConfiguration config) : IKufarService
    {
        private const string _minskGeo = "country-belarus~province-minsk~locality-minsk";

        private readonly string _kufarUrl = config["KufarUrl"]
            ?? throw new ArgumentNullException("KufarUrl");

        public async Task<AllStats> GetAllStats()
        {
            var prms = new KufarParams
            {
                Typ = "sell",
                Gtsy = _minskGeo
            };

            var ads = await kufarClient.GetAds(_kufarUrl, prms);

            return new AllStats
            {
                FloorStats = ads.GetFloorStats(),
                RoomStats = ads.GetRoomStats(),
                MetroStats = ads.GetMetroStats()
            };
        }

        public async Task<List<FloorStats>> GetFloorStats()
        {
            var ads = new List<Ad>();

            for(var i = 1; i < 30; i++)
            {
                var prms = new KufarParams
                {
                    Typ = "sell",
                    Gtsy = _minskGeo,
                    Floor = i
                };

                ads.AddRange(await kufarClient.GetAds(_kufarUrl, prms));
            }

            return ads.GetFloorStats();
        }

        public async Task<List<RoomStats>> GetRoomStats()
        {
            var ads = new List<Ad>();

            for(var i = 1; i < 6; i++)
            {
                var prms = new KufarParams
                {
                    Typ = "sell",
                    Gtsy = _minskGeo,
                    Rooms = i
                };

                ads.AddRange(await kufarClient.GetAds(_kufarUrl, prms));
            }

            return ads.GetRoomStats();
        }

        public async Task<List<MetroStats>> GetMetroStats()
        {
            var ads = new List<Ad>();

            // 1 on kufar is empty
            for(var i = 2; i < 34; i++)
            {
                var prms = new KufarParams
                {
                    Typ = "sell",
                    Gtsy = _minskGeo,
                    Metro = i
                };

                ads.AddRange(await kufarClient.GetAds(_kufarUrl, prms));
            }

            return ads.GetMetroStats();
        }

        public async Task<List<Ad>> GetByCoords(List<Point> points)
        {
            var polygon = Figure.BuildFigure(points);
            if(polygon.Count < 3)
                throw new ArgumentException("Invalid points, not a figure");

            var ads = await kufarClient.GetAds(_kufarUrl);

            return ads.Where(a => IsInPolygon(polygon, a)).ToList();
        }

        public async Task<List<Ad>> GetRent(string area, DateTime? startRent, DateTime? endRent)
        {
            var isAreaNumber = int.TryParse(area, out var areaNumber);

            var prms = new KufarParams
            {
                Typ = "let",
                Rnt = "2",
                Sort = "prc.a",
                Area = isAreaNumber ? areaNumber : 0
            };

            // day id
            var magicDate = DateTime.ParseExact("2024-05-08", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var magicNumber = 19851;

            if(IsRentDateCorrect(startRent, endRent))
            {
                var startNumber = magicNumber + (startRent!.Value - magicDate).Days;
                var endNumber = magicNumber + (endRent!.Value - magicDate).Days;

                prms.Bkcl = $"rn:{startNumber},{endNumber},1,0";
            }

            var ads = await kufarClient.GetAds(_kufarUrl, prms);

            if(isAreaNumber)
                return ads;

            var adsRes = ads.Where(a => IsInArea(a, area));

            // if area passed as string try to get code first and than use search
            // because idk all area codes and kufar have limit 1000
            if(adsRes.Any())
            {
                prms.Area = adsRes.First().AdParameters[nameof(Param.ar)].Value.ToString()!.GetInt();
                ads = await kufarClient.GetAds(_kufarUrl, prms);
            }

            return ads;
        }

        private static bool IsInPolygon(List<Point> polygon, Ad ad)
        {
            if(!ad.AdParameters.TryGetValue(nameof(Param.gbx), out Parameter<object, object>? value))
                return false;

            var coords = JsonSerializer.Deserialize<double[]>(value.Value.ToString()!);
            if(coords == null || coords.Length < 2)
                return false;

            return Figure.IsPointInside(polygon, new Point(coords[0], coords[1]));
        }

        private static bool IsInArea(Ad ad, string area)
        {
            if(!ad.AdParameters.TryGetValue(nameof(Param.ar), out Parameter<object, object>? value))
                return false;

            return value.ValueLocal.ToString()! == area;
        }

        private static bool IsRentDateCorrect(DateTime? startRent, DateTime? endRent) =>
            startRent != null && endRent != null && (endRent.Value - startRent.Value).Days > 0;
    }
}