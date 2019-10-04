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
    public partial class LoginPage : CirclePage
    {
        private static readonly PocketCastsApiService pocketCastsApiService = new PocketCastsApiService();

        public LoginPage()
        {
            InitializeComponent();

            if (SettingsService.CurrentUser != null)
            {
                LoginCurrentUser();
            }
        }

        private async Task LoginCurrentUser()
        {
            User currentUser = SettingsService.CurrentUser;

            try
            {
                // TODO: Add error checking for if login fails and do something
                string authToken = await pocketCastsApiService.Login(currentUser.Email, currentUser.Password);

                SettingsService.ApiToken = authToken;

                App.SetupNavigation();
            }
            catch (Exception e)
            {
                int i = 88;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            User loginUser = new User
            {
                Email = emailEntry.Text,
                Password = passwordEntry.Text
            };

            try
            {
                // TODO: Add error checking for if login fails and do something
                string authToken = await pocketCastsApiService.Login(loginUser.Email, loginUser.Password);

                SettingsService.ApiToken = authToken;

                SettingsService.CurrentUser = loginUser;

                App.SetupNavigation();
            }
            catch (Exception e2)
            {
                int i = 88;
            }
        }
    }
}