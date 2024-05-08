namespace Api.Models.Ads
{
    public class Ad
    {
        public string AccountId { get; set; } = null!;
        public List<Parameter<object, object>> AccountParameters { get; set; } = null!;
        public int AdId { get; set; }
        public string AdLink { get; set; } = null!;
        public Dictionary<string, Parameter<object, object>> AdParameters { get; set; } = null!;
        public object? Body { get; set; }
        public string BodyShort { get; set; } = null!;
        public string Category { get; set; } = null!;
        public bool CompanyAd { get; set; }
        public string Currency { get; set; } = null!;
        public bool IsMine { get; set; }
        public int ListId { get; set; }
        public DateTime ListTime { get; set; }
        public string MessageId { get; set; } = null!;
        public bool PhoneHidden { get; set; }
        public string PriceByn { get; set; } = null!;
        public string PriceUsd { get; set; } = null!;
        public string RemunerationType { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Type { get; set; } = null!;
    }
}