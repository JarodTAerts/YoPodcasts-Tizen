using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PodcastApp.Accessors;
using PodcastApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PodcastApp.Services
{
    public class PocketCastsApiService
    {
        private static readonly PocketCastsApiAccessorcs pocketCastsApiAccessor = new PocketCastsApiAccessorcs();

        private static string authToken = "";

        public async Task Login(string username, string password)
        {
            HttpResponseMessage loginResponseMessage = await pocketCastsApiAccessor.Login(username, password);

            AuthResponse loginResponse = JsonConvert.DeserializeObject<AuthResponse>(await loginResponseMessage.Content.ReadAsStringAsync());

            authToken = loginResponse.Token;
        }

        public async Task<List<Podcast>> GetSubscribedPodcasts()
        {
            HttpResponseMessage subscribedPodcastsResponse = await pocketCastsApiAccessor.GetSubscribedPodcasts(authToken);

            JObject podcastJsonList = JObject.Parse(await subscribedPodcastsResponse.Content.ReadAsStringAsync());

            List<Podcast> subscribedPodcasts = JsonConvert.DeserializeObject<List<Podcast>>(podcastJsonList["podcasts"].ToString());

            return subscribedPodcasts;
        }
    }
}
