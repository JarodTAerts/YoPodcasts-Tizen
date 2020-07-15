using PodcastApp.Models;
using PodcastApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketcastsApiTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            DoThings();

            Console.ReadKey();
        }

        static async void DoThings()
        {
            PocketCastsApiService pocketCastsApiService = new PocketCastsApiService();
            try
            {
                await pocketCastsApiService.Login("jarod2017@gmail.com", "Basketball#1");

                List<Podcast> podcasts = await pocketCastsApiService.GetSubscribedPodcasts();

                //podcasts.ItemsSource = podcasts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                int i = 88;
            }
        }
    }
}
