﻿using PodcastApp.Models;
using PodcastApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PodcastApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QueuePage : CirclePage
    {
        private static readonly PocketCastsApiService pocketCastsApiService = new PocketCastsApiService();

        public QueuePage ()
		{
			InitializeComponent ();

            SetThingsUp();
		}

        private async Task SetThingsUp()
        {
            try
            {
                await pocketCastsApiService.Login("jarod2017@gmail.com", "Basketball#1");

                List<Episode> podcasts = await pocketCastsApiService.GetQueue();

                list.ItemsSource = podcasts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                int i = 88;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MainPage();
            return true;
        }
    }
}