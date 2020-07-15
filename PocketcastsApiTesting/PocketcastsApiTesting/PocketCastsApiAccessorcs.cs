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
            loginCreds.Add("email", "jarod2017@gmail.com");
            loginCreds.Add("password", "Basketball#1");
            loginCreds.Add("scope", "webplayer");

            StringContent loginContent = new StringContent(JsonConvert.SerializeObject(loginCreds), Encoding.Default, "application/json");

            return await httpClient.PostAsync($"{baseUrl}login", loginContent);
        }

        public async Task<HttpResponseMessage> GetSubscribedPodcasts(string authToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            //httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");

            return await httpClient.PostAsync($"{baseUrl}podcast/list", new StringContent("{}"));
        }
    }
}
