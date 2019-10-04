using Newtonsoft.Json;
using PodcastApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PodcastApp.Accessors
{
    public class PocketCastsApiAccessorcs
    {
        private readonly string baseUrl = "https://api.pocketcasts.com/user/";
        private readonly HttpClient httpClient;

        public PocketCastsApiAccessorcs()
        {
            httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Origin", $"https://playbeta.pocketcasts.com");
        }

        public async Task<HttpResponseMessage> Login(string username, string password)
        {
            Dictionary<string, string> loginCreds = new Dictionary<string, string>();
            loginCreds.Add("email", username);
            loginCreds.Add("password", password);
            loginCreds.Add("scope", "webplayer");

            StringContent loginContent = new StringContent(JsonConvert.SerializeObject(loginCreds), Encoding.Default, "application/json");

            return await httpClient.PostAsync($"{baseUrl}login", loginContent);
        }

        public async Task<HttpResponseMessage> GetSubscribedPodcasts(string authToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            return await httpClient.PostAsync($"{baseUrl}podcast/list", new StringContent("{}"));
        }

        public async Task<HttpResponseMessage> GetEpisodesForPodcast(string authToken, string uuid)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            Dictionary<string, string> loginCreds = new Dictionary<string, string>();
            loginCreds.Add("uuid", uuid);

            StringContent content = new StringContent(JsonConvert.SerializeObject(loginCreds), Encoding.Default, "application/json");

            return await httpClient.PostAsync($"{baseUrl}podcast/episodes", content);
        }

        public async Task<HttpResponseMessage> GetNewEpisodes(string authToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            return await httpClient.PostAsync($"{baseUrl}new_releases", new StringContent("{}"));
        }
    }
}
