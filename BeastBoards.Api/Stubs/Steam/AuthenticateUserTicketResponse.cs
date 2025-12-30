using System.Text.Json.Serialization;

namespace BeastBoards.Api.Stubs.Steam
{
    public class AuthenticateUserTicketResponse
    {
        [JsonPropertyName("response")]
        public AuthenticateUserTicketResponseResponse Response { get; set; }

    }

    public class AuthenticateUserTicketResponseResponse
    {
        [JsonPropertyName("params")]
        public AuthenticateUserTicketResponseResponseParams Params { get; set; }
    }

    public class AuthenticateUserTicketResponseResponseParams
    {
        [JsonPropertyName("result")]
        public string Result { get; set; }
        [JsonPropertyName("steamid")]
        public string SteamId { get; set; }
        [JsonPropertyName("ownersteamid")]
        public string OwnerSteamId { get; set; }
        [JsonPropertyName("vacbanned")]
        public bool VacBanned { get; set; }
        [JsonPropertyName("publisherbanned")]
        public bool PublisherBanned { get; set; }
    }
}
