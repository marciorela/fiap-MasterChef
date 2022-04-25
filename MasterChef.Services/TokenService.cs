using MasterChef.Contracts.Services;
using MasterChef.Domain.Dto;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace MasterChef.Services
{
    public class TokenService : ITokenService
    {
        private readonly IMemoryCache _memoryCache;
        private const string TOKEN_KEY = "token";

        public TokenService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<string> ObterTokenAsync(IConfiguration config, RestClient client)
        {            
            if (_memoryCache.TryGetValue(TOKEN_KEY, out AuthResponse authResponse))
            {
                return authResponse.Token;
            }

            var request = new RestRequest("auth", Method.Post)
                .AddJsonBody(new AuthRequest
                {
                    ClientId = config["Auth:ClientId"],
                    Secret = config["Auth:Secret"]
                });
            var response = await client.PostAsync<AuthResponse>(request);

            if (response != null)
            {                
                _memoryCache
                    .Set(
                    TOKEN_KEY,
                    response,
                    new MemoryCacheEntryOptions //Tempo para manter em memória
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(response.ExpiresIn), //Tempo de expiração apartir de agora
                        SlidingExpiration = TimeSpan.FromSeconds(response.ExpiresIn / 2) //Caso não seja utilizado, vai remover nesse tempo
                    });

                return response.Token;
            }

            return "";
        }
    }
}
