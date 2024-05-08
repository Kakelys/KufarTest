using Api.Models.Ads;
using Api.Models.Stats;
using Api.Utils;

namespace Api.Services.Interfaces
{
    public interface IKufarService
    {
        /// <summary>
        /// first 1k apartments
        /// </summary>
        Task<AllStats> GetAllStats();
        /// <summary>
        /// 29k apartments (1k for each floor)
        /// </summary>
        Task<List<FloorStats>> GetFloorStats();
        /// <summary>
        /// 5k apartments (1k for every rooms)
        /// </summary>
        Task<List<RoomStats>> GetRoomStats();
        /// <summary>
        /// 33k apartments (33k for every station)
        /// </summary>
        Task<List<MetroStats>> GetMetroStats();
        Task<List<Ad>> GetRent(string area, DateTime? startRent, DateTime? endRent);
        Task<List<Ad>> GetByCoords(List<Point> points);
    }
}