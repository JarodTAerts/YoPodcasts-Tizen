using PodcastApp.Models;
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
	public partial class SubscribedPodcastsPage : CirclePage
	{
        private static readonly PocketCastsApiService pocketCastsApiService = new PocketCastsApiService();

        public SubscribedPodcastsPage ()
		{
			InitializeComponent ();

            SetThingsUp();
        }

        private async Task SetThingsUp()
        {
            try
            {
                List<Podcast> podcasts = await pocketCastsApiService.GetSubscribedPodcasts();

                list.ItemsSource = podcasts;

                SettingsService.SubscribedPodcasts = podcasts;
            }
            catch (Exception e)
            {
                list.ItemsSource = SettingsService.SubscribedPodcasts;

                if (list.ItemsSource == null)
                    list.ItemsSource = new List<Podcast> { new Podcast { Title = "No connection and no local data" }, new Podcast { Title = e.Message } };
            }
        }

        protected override bool OnBackButtonPressed()
        {
            NavigationService.GoBack();
            return true;
        }

        protected override void OnAppearing()
        {
            if (list.SelectedItem == null)
            {
                base.OnAppearing();
                return;
            }

            list.ScrollTo(list.SelectedItem, ScrollToPosition.Center, true);
            base.OnAppearing();
        }
    }
}