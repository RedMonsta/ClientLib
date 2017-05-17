using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace OlympFoodClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new OlympFoodClient.MainPage();
            //MainPage = new NavigationPage(new DishesListPage());

            MainPage = new NavigationPage(new ClientPage());
            //MainPage = new NavigationPage(new StartPage());
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
