using BeastBoards.Api.Stubs.Steam;
using RestSharp;

namespace BeastBoards.Api.Services
{
    public class SteamWebApiService
    {
        private readonly RestClient _client;
        private readonly BeastBoardsConfig _config;

        public SteamWebApiService(BeastBoardsConfig config)
        {
            _client = new RestClient("https://api.steampowered.com");
            _config = config;
        }


        public async Task<AuthenticateUserTicketResponse> AuthenticateUserTicketAsync(string ticket)
        {
            var request = new RestRequest("/ISteamUserAuth/AuthenticateUserTicket/v1/", Method.Get);

            request.AddParameter("key", _config.SteamApiKey)
                .AddParameter("appid", _config.IAmYourBeastAppId)
                .AddParameter("ticket", ticket)
                .AddParameter("identity", _config.SteamIdentity);

            var response = await _client.ExecuteAsync<AuthenticateUserTicketResponse>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception("Failed to authenticate with Steam");
            }

            return response.Data;
        }
    }
}
