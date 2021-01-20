using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareSteamInformation.Data
{
    public class SteamOwnedGamesMasterModel
    {
        public SteamOwnedGamesModel response { get; set; }
    }

    public class SteamOwnedGamesModel
    {
        public int game_count { get; set; }

        public OwnedGamesModel[] games { get; set; }
    }

    public class OwnedGamesModel
    {
        public int appid { get; set; }

        public string name{ get; set; }

        public string img_icon_url { get; set; }

        public string img_logo_url { get; set; }

        public bool has_community_visible_stats { get; set; }

        public int playtime_forever { get; set; }

        public int playtime_windows_forever { get; set; }
    }
}
