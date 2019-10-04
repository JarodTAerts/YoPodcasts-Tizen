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
	public partial class QueuePage : CirclePage
    {
        private static readonly PocketCastsApiService pocketCastsApiService = new PocketCastsApiService();

        private List<Episode> episodes = new List<Episode>();

        public QueuePage ()
		{
			InitializeComponent ();

            SetThingsUp();
		}

        private async Task SetThingsUp()
        {
            try
            {
                episodes = await pocketCastsApiService.GetQueue();

                list.ItemsSource = episodes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                int i = 88;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            NavigationService.GoBack();
            return true;
        }

        protected override void OnAppearing()
        {
            if (episodes.Count <= 0)
                return;

            list.ScrollTo(episodes[0], ScrollToPosition.Center, true);

            base.OnAppearing();
        }
    }
}