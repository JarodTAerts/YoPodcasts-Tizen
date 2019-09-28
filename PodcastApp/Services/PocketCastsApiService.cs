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

        #region Interface Functions
        /// <summary>
        /// Function to login to the Pocket Casts Api and get and auth token we can use for later actions
        /// </summary>
        /// <param name="username">Username of the person to login</param>
        /// <param name="password">Password of the person to login</param>
        /// <returns>Nothing</returns>
        public async Task Login(string username, string password)
        {
            HttpResponseMessage loginResponseMessage = await pocketCastsApiAccessor.Login(username, password);

            AuthResponse loginResponse = JsonConvert.DeserializeObject<AuthResponse>(await loginResponseMessage.Content.ReadAsStringAsync());

            authToken = loginResponse.Token;
        }

        /// <summary>
        /// Function to get a list of all of the podcasts the logged in user is subscribed to
        /// </summary>
        /// <returns>List of Podcast Objects</returns>
        public async Task<List<Podcast>> GetSubscribedPodcasts()
        {
            HttpResponseMessage subscribedPodcastsResponse = await pocketCastsApiAccessor.GetSubscribedPodcasts(authToken);

            JObject podcastJsonList = JObject.Parse(await subscribedPodcastsResponse.Content.ReadAsStringAsync());

            List<Podcast> subscribedPodcasts = JsonConvert.DeserializeObject<List<Podcast>>(podcastJsonList["podcasts"].ToString());

            return subscribedPodcasts;
        }

        /// <summary>
        /// Function to get all the episodes for a particular podcast
        /// </summary>
        /// <param name="uuid">Id of the podcast you want to get the episodes for</param>
        /// <returns>List of episode objects</returns>
        public async Task<List<Episode>> GetEpisodesForPodcast(string uuid)
        {
            List<Episode> episodes = await GetEpisodeListFromResponse(await pocketCastsApiAccessor.GetEpisodesForPodcast(authToken, uuid));

            return episodes;
        }

        /// <summary>
        /// Function to get the "Queue" of episodes to play. For me this is just all the unplayed new episdoes from my subscribed podcasts
        /// In the future the meaning of this might evolve, but that is what it is for now
        /// </summary>
        /// <returns>List of Episode objects</returns>
        public async Task<List<Episode>> GetQueue()
        {
            List<Episode> episodes = await GetEpisodeListFromResponse(await pocketCastsApiAccessor.GetNewEpisodes(authToken));

            return episodes;
        }

        #endregion

        #region Helper Functions
        /// <summary>
        /// Helper function to take a response message and extract the episode list from the response
        /// </summary>
        /// <param name="httpResponseMessage">HttpResponseMessage object that was returned from the access, it should have a list of episode objects in it</param>
        /// <returns></returns>
        private async Task<List<Episode>> GetEpisodeListFromResponse(HttpResponseMessage httpResponseMessage)
        {
            JObject podcastJsonList = JObject.Parse(await httpResponseMessage.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<Episode>>(podcastJsonList["episodes"].ToString());
        }

        /// <summary>
        /// Helper function to take a response message and extract the podcast list from it
        /// </summary>
        /// <param name="httpResponseMessage">HttpResponseMessage object that was returned from the access, it should have a list of podcast objects in it</param>
        /// <returns></returns>
        private async Task<List<Podcast>> GetPodcastListFromResponse(HttpResponseMessage httpResponseMessage)
        {
            JObject podcastJsonList = JObject.Parse(await httpResponseMessage.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<Podcast>>(podcastJsonList["podcasts"].ToString());
        }
        #endregion
    }
}
