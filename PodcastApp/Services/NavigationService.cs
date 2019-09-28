using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PodcastApp.Services
{
    public class NavigationService
    {
        /// <summary>
        /// Dictionary holding all the persistient pages and their keys
        /// </summary>
        private static Dictionary<string, Page> pages = new Dictionary<string, Page>();

        /// <summary>
        /// Stack of pages that holds the page navigation history
        /// </summary>
        private static Stack<Page> pageStack = new Stack<Page>();

        /// <summary>
        /// Homepage of the application, not used really here but might be used in the future
        /// </summary>
        private static Page homePage = null;

        /// <summary>
        /// Function to register a page with the navigation service. This will ensure that the page will persist when you navigate away from it.
        /// </summary>
        /// <param name="pageKey">Key that you will use to navigate to this persitent page in the future</param>
        /// <param name="page">Actual Xamarin.Forms page object you want to be in the service</param>
        public static void RegisterPage(string pageKey, Page page)
        {
            if (pages.ContainsKey(pageKey))
                return;

            pages.Add(pageKey, page);
        }

        /// <summary>
        /// Fucntion to set the homepage of the application. It will also navigate to that homepage. 
        /// </summary>
        /// <param name="pageKey">Key of the page you want to be the homepage. This page must have been registered first</param>
        public static void SetHomePage(string pageKey)
        {
            if (!pages.ContainsKey(pageKey))
                throw new Exception("Page key is not registered.");

            homePage = pages[pageKey];
            App.Current.MainPage = homePage;
            pageStack.Push(homePage);
            return;
        }

        /// <summary>
        /// Function to navigate to a persitent page in the application
        /// </summary>
        /// <param name="pageKey">Key of registered page to navigate to</param>
        public static void NavigateTo(string pageKey)
        {
            if (!pages.ContainsKey(pageKey))
                throw new Exception("Page key is not registered.");

            pageStack.Push(pages[pageKey]);
            App.Current.MainPage = pages[pageKey];
        }

        /// <summary>
        /// Function to go back to the last page shown. If you are on the homepage then it will not do anything
        /// </summary>
        public static void GoBack()
        {
            if (pageStack.Count <= 1)
                return;

            pageStack.Pop();
            App.Current.MainPage = pageStack.Peek();
        }

        /// <summary>
        /// Function to navigate to a modal page that will not persist in memory. This will be added to the stack, but deleted when you go back.
        /// This is a good function to show popups and such.
        /// </summary>
        /// <param name="page">Xamarin.Forms page that you want to show</param>
        public static void PushModalPage(Page page)
        {
            pageStack.Push(page);
            App.Current.MainPage = page;
        }
    }
}
