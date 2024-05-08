namespace Api.Models
{
    public class KufarParams
    {
       public string? Typ { get; set; }
       public string? Rnt { get; set; }
       public int Size { get; set; } = 1000;
       public string? Sort { get; set; }
       public string? Bkcl { get; set; }
       public string? Gtsy { get; set; }
       public int Floor { get; set; }
       public int Area { get; set; }
       public int Rooms { get; set; }
       public int Metro { get; set; }
    }
}