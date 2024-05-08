using System.Text.Json;
using Api.Models;
using Api.Models.Ads;
using Api.Services.Interfaces;
using AutoMapper;
using RestSharp;

namespace Api.Services
{
    public class KufarClient(IMapper mapper) : IKufarClient
    {
        public async Task<List<Ad>> GetAds(string url, KufarParams? prms = null)
        {
            prms ??= new KufarParams();

            var options = new RestClientOptions(url);
            var client = new RestClient(options);

            var req = new RestRequest();
            req.AddParameter("cat", "1010");
            req.AddParameter("cur", "USD");
            req.AddParameter("size", prms.Size);

            if(!string.IsNullOrEmpty(prms.Gtsy))
                req.AddParameter(nameof(Param.gtsy), prms.Gtsy);

            if(!string.IsNullOrEmpty(prms.Rnt))
                req.AddParameter(nameof(Param.rnt), prms.Rnt);

            if(!string.IsNullOrEmpty(prms.Typ))
                req.AddParameter(nameof(Param.typ), prms.Typ);

            if(!string.IsNullOrEmpty(prms.Sort))
                req.AddParameter(nameof(Param.sort), prms.Sort);

            if(!string.IsNullOrEmpty(prms.Bkcl))
                req.AddParameter(nameof(Param.bkcl), prms.Bkcl);

            if(prms.Area > 0)
                req.AddParameter(nameof(Param.ar), prms.Area);

            if(prms.Floor > 0)
                req.AddParameter(nameof(Param.fl), prms.Floor);

            if(prms.Rooms > 0)
                req.AddParameter(nameof(Param.rms), prms.Rooms);

            if(prms.Metro > 0)
                req.AddParameter(nameof(Param.mee), prms.Metro);

            var res = await client.ExecuteGetAsync(req);
            if(!res.IsSuccessful || res.Content == null)
            {
                throw new HttpRequestException(res.Content, null, res.StatusCode);
            }

            var rawAds = JsonSerializer.Deserialize<AdResponse>(res.Content)!.ads;

            return mapper.Map<List<Ad>>(rawAds);
        }
    }
}