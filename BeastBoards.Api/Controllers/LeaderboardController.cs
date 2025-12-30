using System.Text.Json;
using BeastBoards.Api.Filters;
using BeastBoards.Api.Services;
using BeastBoards.Api.Stubs;
using BeastBoards.Api.Stubs.Api;
using Microsoft.AspNetCore.Mvc;

namespace BeastBoards.Api.Controllers
{
    [Route("leaderboard")]
    public class LeaderboardController(
        BeastBoardsLeaderboardService _leaderboard
    ) : Controller
    {


        [HttpPost]
        [BeastBoardsAuthFilter]
        public IActionResult Post([FromBody] AddLeaderboardTimingRequest req)
        {
            var steam = HttpContext.Items[nameof(SteamUserStub)] as SteamUserStub;

            var items = _leaderboard.AddLeaderboardTiming(req, steam.UserId);

            return Ok(JsonSerializer.Serialize(new AddLeaderboardTimingResponse() { Items = items }));
        }
    }
}
