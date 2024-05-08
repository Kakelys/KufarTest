using AutoMapper;

namespace Api.Models.Ads
{
    public class AdProfile : Profile
    {
        public AdProfile()
        {
            CreateMap<RawParameter, Parameter<object, object>>()
            .ForMember(dest => dest.Param, o => o.MapFrom(src => src.p))
            .ForMember(dest => dest.Value, o => o.MapFrom(src => src.v))
            .ForMember(dest => dest.ParamLocal, o => o.MapFrom(src => src.pl))
            .ForMember(dest => dest.ValueLocal, o => o.MapFrom(src => src.vl))
            .ForMember(dest => dest.Pu, o => o.MapFrom(src => src.pu));

            CreateMap<List<RawParameter>, Dictionary<string, Parameter<object, object>>>()
            .ConvertUsing((src) => src.ToDictionary(el => el.pu, el => new Parameter<object, object> 
            {
                Param = el.p,
                Value = el.v,
                ParamLocal = el.pl,
                ValueLocal = el.vl,
                Pu = el.pu
            }));

            CreateMap<RawAd, Ad>()
                .ForMember(dest => dest.AccountId, o => o.MapFrom(src => src.account_id))
                .ForMember(dest => dest.AccountParameters, o => o.MapFrom(src => src.account_parameters))
                .ForMember(dest => dest.AdId, o => o.MapFrom(src => src.ad_id))
                .ForMember(dest => dest.AdLink, o => o.MapFrom(src => src.ad_link))
                .ForMember(dest => dest.AdParameters, o => o.MapFrom(src => src.ad_parameters))
                .ForMember(dest => dest.Body, o => o.MapFrom(src => src.body))
                .ForMember(dest => dest.BodyShort, o => o.MapFrom(src => src.body_short))
                .ForMember(dest => dest.Category, o => o.MapFrom(src => src.category))
                .ForMember(dest => dest.CompanyAd, o => o.MapFrom(src => src.company_ad))
                .ForMember(dest => dest.Currency, o => o.MapFrom(src => src.currency))
                .ForMember(dest => dest.IsMine, o => o.MapFrom(src => src.is_mine))
                .ForMember(dest => dest.ListId, o => o.MapFrom(src => src.list_id))
                .ForMember(dest => dest.ListTime, o => o.MapFrom(src => src.list_time))
                .ForMember(dest => dest.MessageId, o => o.MapFrom(src => src.message_id))
                .ForMember(dest => dest.PhoneHidden, o => o.MapFrom(src => src.phone_hidden))
                .ForMember(dest => dest.PriceByn, o => o.MapFrom(src => src.price_byn))
                .ForMember(dest => dest.PriceUsd, o => o.MapFrom(src => src.price_usd))
                .ForMember(dest => dest.RemunerationType, o => o.MapFrom(src => src.remuneration_type))
                .ForMember(dest => dest.Subject, o => o.MapFrom(src => src.subject))
                .ForMember(dest => dest.Type, o => o.MapFrom(src => src.type));
        }
    }
}