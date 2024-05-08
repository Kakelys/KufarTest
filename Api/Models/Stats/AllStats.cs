namespace Api.Models.Stats
{
    public class AllStats
    {
        public List<FloorStats> FloorStats { get; set; }
        public List<RoomStats> RoomStats { get; set; }
        public List<MetroStats> MetroStats { get; set; }

    }
}