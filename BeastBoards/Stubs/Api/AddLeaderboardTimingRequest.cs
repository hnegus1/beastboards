using System;
using System.Collections.Generic;
using System.Text;

namespace BeastBoards.Stubs.Api
{
    public class AddLeaderboardTimingRequest
    {
        public List<ulong> FriendIds { get; set; }
        public int LevelNumber { get; set; }
        public string Category { get; set; }
        public float BestTime { get; set; }
    }
}
