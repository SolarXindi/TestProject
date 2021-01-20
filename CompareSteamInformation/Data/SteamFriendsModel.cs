using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CompareSteamInformation.Data
{
    public class SteamFriendsMasterModel
    {
        [JsonPropertyName("friendslist")]
        public SteamFriendsUpperModel MasterFriendList { get; set; }
    }

    public class SteamFriendsUpperModel
    {
        [JsonPropertyName("friends")]
        public SteamFriendModel[] FriendsList { get; set; }
    }

    public class SteamFriendModel
    {
        [JsonPropertyName("steamid")]
        public string SteamID { get; set; }

        [JsonPropertyName("relationship")]
        public string FriendRelationship { get; set; }
    }

    public class SteamPlayerMasterModel
    {
        public SteamPlayerUpperModel response { get; set; }
    }

    public class SteamPlayerUpperModel
    {
        public SteamPlayerModel[] players { get; set; }
    }

    public class SteamPlayerModel
    {
        public string steamid { get; set; }
        public string personaname { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
    }
}
