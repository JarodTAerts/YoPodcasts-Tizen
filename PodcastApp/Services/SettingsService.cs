using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using PodcastApp.Models;
using Tizen.Applications;

namespace PodcastApp.Services
{
    public static class SettingsService
    {

        public static List<Podcast> SubscribedPodcasts
        {
            get
            {
                if (Preference.Contains("SubscribedPodcasts"))
                {
                    return JsonConvert.DeserializeObject<List<Podcast>>(Preference.Get<string>("SubscribedPodcasts"));
                }

                return null;
            }
            set
            {
                string jsonText = JsonConvert.SerializeObject(value);
                Preference.Set("SubscribedPodcasts", jsonText);
            }
        }

        public static List<Episode> Queue
        {
            get
            {
                if (Preference.Contains("Queue"))
                {
                    return JsonConvert.DeserializeObject<List<Episode>>(Preference.Get<string>("Queue"));
                }

                return null;
            }
            set
            {
                string jsonText = JsonConvert.SerializeObject(value);
                Preference.Set("Queue", jsonText);
            }
        }

        public static List<Episode> DownloadQueue
        {
            get
            {
                if (Preference.Contains("DownloadQueue"))
                {
                    return JsonConvert.DeserializeObject<List<Episode>>(Preference.Get<string>("DownloadQueue"));
                }

                return null;
            }
            set
            {
                string jsonText = JsonConvert.SerializeObject(value);
                Preference.Set("DownloadQueue", jsonText);
            }
        }

        public static List<Episode> DownloadedEpisodes
        {
            get
            {
                if (Preference.Contains("DownloadedEpisodes"))
                {
                    return JsonConvert.DeserializeObject<List<Episode>>(Preference.Get<string>("DownloadedEpisodes"));
                }

                return null;
            }
            set
            {
                string jsonText = JsonConvert.SerializeObject(value);
                Preference.Set("DownloadedEpisodes", jsonText);
            }
        }

        public static string ApiToken
        {
            get
            {
                if (Preference.Contains("ApiToken"))
                {
                    return Preference.Get<string>("ApiToken");
                }

                return string.Empty;
            }
            set
            {
                Preference.Set("ApiToken", value);
            }
        }

        public static User CurrentUser
        {
            get
            {
                if (Preference.Contains("CurrentUser"))
                {
                    return JsonConvert.DeserializeObject<User>(Preference.Get<string>("CurrentUser"));
                }

                return null;
            }
            set
            {
                string jsonText = JsonConvert.SerializeObject(value);
                Preference.Set("CurrentUser", jsonText);
            }
        }

        public static Episode PlayingEpisode
        {
            get
            {
                if (Preference.Contains("PlayingEpisode"))
                {
                    return JsonConvert.DeserializeObject<Episode>(Preference.Get<string>("PlayingEpisode"));
                }

                return null;
            }
            set
            {
                string jsonText = JsonConvert.SerializeObject(value);
                Preference.Set("PlayingEpisode", jsonText);
            }
        }

    }
}
