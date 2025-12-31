using System.Xml.Linq;
using BeastBoards.Stubs;
using HarmonyLib;
using MelonLoader;
using MelonLoader.InternalUtils;
using Steamworks;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputRemoting;

[assembly: MelonInfo(typeof(BeastBoards.BeastBoardsMod), "BeastBoards", "1.0.0", "VideoGamesAreBad", null)]
[assembly: MelonGame("Strange Scaffold", "I Am Your Beast")]

namespace BeastBoards
{
    public class BeastBoardsMod : MelonMod
    {
        protected Callback<GetTicketForWebApiResponse_t> m_GetTicketForWebApiResponse;


        public static Steam Steam { get; set; } = null;
        public static Api Api { get; set; } = null;

        public static bool BeastBoardsIsRunning { get; set; } = false;


        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Hello from BeastBoards!");
        }

        public override void OnUpdate()
        {
            SteamAPI.RunCallbacks();
        }

        public override void OnLateInitializeMelon()
        {
            try
            {
                m_GetTicketForWebApiResponse = Callback<GetTicketForWebApiResponse_t>.Create(OnGetTicketForWebApiResponse);

                SteamUser.GetAuthTicketForWebApi("BEASTBOARDS");

            }
            catch (InvalidOperationException)
            {
                Error.ShowError("Error: SteamWorks is not initialized. BeastBoards will not work. Please make sure you are launching the game via Steam.");
            }

        }

        private void OnGetTicketForWebApiResponse(GetTicketForWebApiResponse_t result)
        {
            Steam = new Steam(result);
            Api = new Api();
        }

       
    }

    [HarmonyPatch(typeof(UILevelCompleteScreen), "DisplayLevelNameComplete", [])]
    public static class CompleteLevelPatch
    {
        private static void Postfix()
        {
            if (!BeastBoardsMod.BeastBoardsIsRunning)
            {
                Error.ShowError("Error: Failed to connect to the BeastBoards server. Please check your internet connection and restart the game.");
                return;
            }


            var info = GameManager.instance.levelController.GetInformationSetter().GetInformation();
            var data = GameManager.instance.progressManager.GetLevelData(info);
            

            List<LeaderboardTiming> leaderboard = BeastBoardsMod.Api.AddLeaderboardTiming(info.GetLevelNumber(), data.GetBestTime(), info.GetLevelCategoryName());

            AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "beastboardsui"));

            if (localAssetBundle == null)
            {
                return;
            }

            GameObject asset = localAssetBundle.LoadAsset<GameObject>("BeastBoardsCanvas");
            var created = GameObject.Instantiate(asset);
            var parent = created.GetNamedChild("BeastBoardsContainer").GetNamedChild("Leaderboard").GetNamedChild("Items");

            GameObject item = localAssetBundle.LoadAsset<GameObject>("BeastBoardItem");



            foreach (var friend in leaderboard)
            {
                var entry = GameObject.Instantiate(item, parent.transform, false);

                var img = entry.GetNamedChild("Image").GetComponent<RawImage>();
                img.texture = friend.GetSteamUser().AvatarTexture;


                var name = entry.GetNamedChild("Name").GetComponent<TextMeshProUGUI>();
                name.text = friend.GetSteamUser().PersonaName;

                var time = entry.GetNamedChild("Time").GetComponent<TextMeshProUGUI>();
                time.text = friend.Time.ToString("F2");

            }

            var scrollRect = created.GetNamedChild("BeastBoardsContainer").GetNamedChild("Leaderboard").GetComponent<ScrollRect>();
            scrollRect.verticalNormalizedPosition = scrollRect.flexibleHeight;


            localAssetBundle.Unload(false);
        }



    }

}