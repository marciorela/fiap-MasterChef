using Microsoft.Extensions.Configuration;
using RestSharp;

namespace MasterChef.Contracts.Services
{
    public interface ITokenService
    {
        Task<string> ObterTokenAsync(IConfiguration config, RestClient client);
    }
}
