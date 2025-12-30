using System.Security.Cryptography;
using BeastBoards.Api.Filters;
using BeastBoards.Api.Services;
using BeastBoards.Api.Stubs;
using BeastBoards.Api.Stubs.Api;
using JWT.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BeastBoards.Api.Controllers
{

    [Route("auth")]
    public class AuthController(
        SteamWebApiService _steamApi,
        BeastBoardsJwtService _jwt
    ) : Controller
    {

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AuthenticateApiRequest req)
        {
            var result = await _steamApi.AuthenticateUserTicketAsync(req.Token);

            var jwt = _jwt.Create(result);

            return Ok(jwt);
        }

        [HttpGet]
        [BeastBoardsAuthFilter]
        public IActionResult TestAuth()
        {
            //filter is applied - getting here implies success!
            var steam = HttpContext.Items[nameof(SteamUserStub)] as SteamUserStub;

            return Ok(steam.UserId);
        }
   
    }
}
