namespace Api.Models.Ads
{
    public class RawAd
    {
        public string account_id { get; set; } = null!;
        public List<RawParameter> account_parameters { get; set; } = null!;
        public int ad_id { get; set; }
        public string ad_link { get; set; } = null!;
        public List<RawParameter> ad_parameters { get; set; } = null!;
        public object? body { get; set; }
        public string body_short { get; set; } = null!;
        public string category { get; set; } = null!;
        public bool company_ad { get; set; }
        public string currency { get; set; } = null!;
        public bool is_mine { get; set; }
        public int list_id { get; set; }
        public DateTime list_time { get; set; }
        public string message_id { get; set; } = null!;
        public bool phone_hidden { get; set; }
        public string price_byn { get; set; } = null!;
        public string price_usd { get; set; } = null!;
        public string remuneration_type { get; set; } = null!;
        public string subject { get; set; } = null!;
        public string type { get; set; } = null!;
    }
}