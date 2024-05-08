using System.Globalization;
using System.Text.Json;
using Api.Models.Ads;
using Api.Models.Stats;

namespace Api.Utils
{
    public static class ParseExtension
    {
        public static double GetDouble(this string str)
        {
            return double.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
        }

        public static int GetInt(this string str)
        {
            return int.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
        }

        public static List<FloorStats> GetFloorStats(this List<Ad> ads)
        {
            return ads.Where(a => a.AdParameters.ContainsKey(nameof(Param.fl)) && a.AdParameters.ContainsKey(nameof(Param.psm)))
            .GroupBy(a => JsonSerializer.Deserialize<int[]>(a.AdParameters[nameof(Param.fl)].Value.ToString()!)![0], a => a)
            .ToDictionary(grp => grp.Key, grp => grp.Average(el => el.AdParameters[nameof(Param.psm)].Value.ToString()!.GetDouble()))
            .Select(el => new FloorStats
            {
                Floor = el.Key,
                AvgPrice = el.Value
            }).OrderBy(el => el.Floor).ToList();
        }

        public static List<RoomStats> GetRoomStats(this List<Ad> ads)
        {
            return ads.Where(a => a.AdParameters.ContainsKey(nameof(Param.rms)) && a.AdParameters.ContainsKey(nameof(Param.psm)))
            .GroupBy(a => a.AdParameters[nameof(Param.rms)].Value.ToString()!.GetInt(), a => a)
            .ToDictionary(grp => grp.Key, grp => grp.Average(el => el.AdParameters[nameof(Param.psm)].Value.ToString()!.GetDouble()))
            .Select(el => new RoomStats
            {
                Rooms = el.Key,
                AvgPrice = el.Value
            }).OrderBy(el => el.Rooms).ToList();
        }

        public static List<MetroStats> GetMetroStats(this List<Ad> ads)
        {
            return ads.Where(a => a.AdParameters.ContainsKey(nameof(Param.mee)) && a.AdParameters.ContainsKey(nameof(Param.psm)))
            .GroupBy(a => JsonSerializer.Deserialize<string[]>(a.AdParameters[nameof(Param.mee)].ValueLocal.ToString()!)![0], a => a)
            .ToDictionary(grp => grp.Key, grp => grp.Average(el => el.AdParameters[nameof(Param.psm)].Value.ToString()!.GetDouble()))
            .Select(el => new MetroStats
            {
                Metro = el.Key,
                AvgPrice = el.Value
            }).ToList();
        }
    }
}