using Api.Models;
using Api.Models.Ads;

namespace Api.Services.Interfaces
{
    public interface IKufarClient
    {
        Task<List<Ad>> GetAds(string url, KufarParams? prms = null);
    }
}