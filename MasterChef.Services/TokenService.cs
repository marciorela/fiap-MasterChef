using MasterChef.Contracts.Services;
using MasterChef.Domain.Dto;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace MasterChef.Services
{
    public class TokenService : ITokenService
    {
        public TokenService()
        {
                
        }

        public async Task<string> ObterTokenAsync(IConfiguration config, RestClient client)
        {
            var request = new RestRequest("auth", Method.Post)
                .AddJsonBody(new AuthRequest
                {
                    ClientId = config["Auth:ClientId"],
                    Secret = config["Auth:Secret"]
                });
            var response = await client.PostAsync<AuthResponse>(request);

            if (response != null)
            {
                return response.Token;
            }

            return "";
        }
    }
}
