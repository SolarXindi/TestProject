using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace CompareSteamInformation.Data
{
    public class SteamAPIService
    {
        public HttpClient _client;
        protected string apiKey = "XXXXXXXXXXXXXXXXXXXXX";

        public SteamAPIService(HttpClient client)
        {
            _client = client;
        }

        public async Task<SteamOwnedGamesMasterModel> GetFriendsFromSteamAPI(string steamID)
        {
            var requestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={apiKey}&steamid={steamID}&include_appinfo=true&include_played_free_games=true"),
            };

            var results = await _client.SendAsync(requestMessage);

            var resultString = results.IsSuccessStatusCode ? await results.Content.ReadAsStringAsync() : string.Empty;

            var returnValue = JsonSerializer.Deserialize<SteamOwnedGamesMasterModel>(resultString);

            return returnValue;
        }

        public async Task<SteamPlayerMasterModel> GetSteamFriends(string steamID)
        {
            var friendRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"http://api.steampowered.com/ISteamUser/GetFriendList/v1/?key={apiKey}&steamid={steamID}"),
            };

            var results = await _client.SendAsync(friendRequestMessage);

            var resultString = results.IsSuccessStatusCode ? await results.Content.ReadAsStringAsync() : string.Empty;

            var friendsList = JsonSerializer.Deserialize<SteamFriendsMasterModel>(resultString);

            var friends = friendsList.friendslist.friends.Select(s => s.SteamID);
            var friendString = string.Join(",", friends);

            var playerRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2?key={apiKey}&steamids={friendString}"),
            };

            var playerResponse = await _client.SendAsync(playerRequestMessage);

            var playerString = playerResponse.IsSuccessStatusCode ? await playerResponse.Content.ReadAsStringAsync() : string.Empty;

            var returnValue = JsonSerializer.Deserialize<SteamPlayerMasterModel>(playerString);

            return returnValue;
        }
    }
}
