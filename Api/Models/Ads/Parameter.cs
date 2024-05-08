namespace Api.Models.Ads
{
    public class Parameter<T,K>
    {
        public string ParamLocal { get; set; } = null!;
        public T ValueLocal { get; set; } = default!;
        public string Param { get; set; } = null!;
        public K Value { get; set; } = default!;
        public string Pu { get; set; } = null!;
    }
}