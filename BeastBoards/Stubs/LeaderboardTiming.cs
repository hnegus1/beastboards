using System;
using System.Collections.Generic;
using System.Text;

namespace BeastBoards.Stubs
{
    public class LeaderboardTiming
    {
        public int Id { get; set; }
        public float Time { get; set; }
        public int LevelNumber { get; set; }
        public ulong SteamId { get; set; }

        public SteamUserStub GetSteamUser()
        {
            return BeastBoardsMod.Steam.Users.FirstOrDefault(x => x.Id == SteamId);
        }

    }
}
