using Newtonsoft.Json;

namespace BeastBoards.Api.Stubs
{
    public class SteamUserStub
    {
        public ulong UserId { get; set; }
        [JsonProperty("exp")]
        public double Exp { get; set; }
        [JsonProperty("iat")]
        public double Iat { get; set; }
    }
}
