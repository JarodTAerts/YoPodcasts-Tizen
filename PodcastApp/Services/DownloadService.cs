using PodcastApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tizen.System;
using Xamarin.Forms;

namespace PodcastApp.Services
{
    public static class DownloadService
    {
        private static List<Episode> downloadQueue = new List<Episode>();

        private static object queueLock = new object();

        public static void Start()
        {
            CheckForDownloads();

            Task.Run(() => DownloadConsumer());
        }

        public static void CheckForDownloads()
        {
            if (SettingsService.DownloadQueue != null && SettingsService.DownloadQueue.Count > 0)
                downloadQueue = SettingsService.DownloadQueue;
        }

        public static void AddToQueue(Episode episode)
        {
            lock (queueLock)
            {
                downloadQueue.Add(episode);
                SettingsService.DownloadQueue.Add(episode);
            }
        }

        public static async void DownloadConsumer()
        {
            while (true)
            {
                if (downloadQueue.Count > 0)
                {
                    try
                    {
                        Episode episode;
                        lock (queueLock)
                        {
                            episode = downloadQueue[0];
                        }

                        byte[] fileDownload = await DownloadFile(episode.Url);

                        string storagePath = "/opt/usr/home/owner/data/yocasts";

                        if (!Directory.Exists(storagePath))
                            Directory.CreateDirectory(storagePath);

                        File.WriteAllBytes($"{storagePath}/{CleanPodcastName(episode.Title)}.mp3", fileDownload);

                        lock (queueLock)
                        {
                            downloadQueue.Remove(episode);
                            SettingsService.DownloadQueue.Remove(episode);
                            SettingsService.DownloadedEpisodes.Add(episode);
                        }
                    }
                    catch(Exception e)
                    {
                        int i = 88;
                    }
                }

                Task.Delay(TimeSpan.FromSeconds(10)).Wait();
            }
        }

        private static string CleanPodcastName(string name)
        {
            name = Regex.Replace(name, "/", "-");
            return Regex.Replace(name, " " ,"-");
        }

        public static async Task<byte[]> DownloadFile(string url)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(30);
                using (var result = await client.GetAsync(url))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        return await result.Content.ReadAsByteArrayAsync();
                    }

                }
            }
            return null;
        }
    }
}
