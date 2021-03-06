﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using PodcastApp.Services;
using PodcastApp.Models;

namespace PodcastApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        private static readonly PocketCastsApiService pocketCastsApiService = new PocketCastsApiService();

        public MainPage()
        {
            InitializeComponent();

            List<MainPageItem> mainPageItems = new List<MainPageItem>
            {
                new MainPageItem{Title="Queue", Description="View and play your queue"},
                new MainPageItem{Title="Subscribed Podcasts", Description="View your subscribed podcasts"}
            };

            list.ItemsSource = mainPageItems;
        }

        private void List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MainPageItem selectedItem = e.SelectedItem as MainPageItem;

            if (selectedItem == null)
                return;

            if(selectedItem.Title == "Queue")
            {
                NavigationService.NavigateTo("Queue");
            }
            else if(selectedItem.Title == "Subscribed Podcasts")
            {
                NavigationService.NavigateTo("Subscribed");
            }
        }

        protected override void OnAppearing()
        {

            if (list.SelectedItem == null)
            {
                base.OnAppearing();
                return;
            }

            list.ScrollTo(list.SelectedItem, ScrollToPosition.Center, true);
            list.SelectedItem = null;
            base.OnAppearing();
        }
    }
}