using Plugin.Settings;
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
    public partial class App : Application
    {
        private static readonly PocketCastsApiService pocketCastsApiService = new PocketCastsApiService();

        public App()
        {
            InitializeComponent();

            //LoginToService().Wait();

            if(SettingsService.ApiToken == string.Empty)
            {
                MainPage = new LoginPage();
                return;
            }

            SetupNavigation();
        }

        public static void SetupNavigation()
        {
            NavigationService.RegisterPage("Home", new MainPage());
            NavigationService.RegisterPage("Queue", new QueuePage());
            NavigationService.RegisterPage("Subscribed", new SubscribedPodcastsPage());

            NavigationService.SetHomePage("Home");
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

