using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using BeastBoards.Stubs;
using BeastBoards.Stubs.Api;
using MelonLoader;
using MelonLoader.TinyJSON;
using Newtonsoft.Json;
using UnityEngine;

namespace BeastBoards
{
    public class Api
    {
        private readonly HttpClient _httpClient;

#if DEBUG
        private readonly string _apiUrl = "https://localhost:7072";
#else
        private readonly string _apiUrl = "https://api.beastboards.videogamesarebad.co.uk";
#endif


        public string Jwtoken { get; set; }

        public Api()
        {

            _httpClient = new HttpClient();

            Task.Run(async () => await AuthenticateAsync());

        }



        public async Task AuthenticateAsync()
        {
            var req = new AuthenticateApiRequest() { Token = BeastBoardsMod.Steam.SteamToken };
            var json = JsonConvert.SerializeObject(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync(new Uri($"{_apiUrl}/auth"), content);

            if (result.IsSuccessStatusCode)
            {
                var jwt = await result.Content.ReadAsStringAsync();
                Jwtoken = jwt;

#if DEBUG
                Melon<BeastBoardsMod>.Instance.LoggerInstance.Msg($"JWT is {Jwtoken}");
#endif

                BeastBoardsMod.BeastBoardsIsRunning = true;
            }
        }

        public List<LeaderboardTiming> AddLeaderboardTiming(int levelNumber, float bestTime)
        {
            var body = new AddLeaderboardTimingRequest()
            {
                BestTime = bestTime,
                LevelNumber = levelNumber,
                FriendIds = BeastBoardsMod.Steam.Users.Where(x => !x.IsPlayer).Select(x => x.Id).ToList()
            };

            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_apiUrl}/leaderboard"),
                Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"),
                Headers =
                {
                    {HttpRequestHeader.Authorization.ToString(), Jwtoken }
                }
            };


            var result = _httpClient.SendAsync(req).GetAwaiter().GetResult();


            if (result.IsSuccessStatusCode)
            {
                var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                var items = JsonConvert.DeserializeObject<AddLeaderboardTimingResponse>(content);

                return items.Items.ToList();
            }

            return [];
        }



    }
}
