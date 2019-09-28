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
        public App()
        {
            InitializeComponent();

            NavigationService.RegisterPage("Home", new MainPage());
            NavigationService.RegisterPage("Queue", new QueuePage());
            NavigationService.RegisterPage("Subscribed", new SubscribedPodcastsPage());

            //MainPage = new PodcastApp.MainPage();
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

