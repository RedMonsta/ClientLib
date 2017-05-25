using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OlympFoodClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DishesListPage : ContentPage
    {
        ApplicationViewModel viewModel;
        //string ClientLogin = "";

        protected override async void OnAppearing()
        {
            await viewModel.GetDishes();
            viewModel.IsBusy = false;
            //viewModel.IsLoaded = false;
            //dishesList.ItemTemplate
            base.OnAppearing();
        }

        public DishesListPage(string login, string passwd, bool isoffline)
        {
            InitializeComponent();
            //ClientLogin = login;
            viewModel = new ApplicationViewModel("disheslistpage", login, passwd) { Navigation = this.Navigation, IsOfflineMode = isoffline };
            BindingContext = viewModel;
            lblLogin.Text = "You enter as: " + login;
            //Title = login;
        }

        private void ButtonReconnect_Clicked(object sender, EventArgs e)
        {
            (sender as Button).BackgroundColor = Color.LightGray;
            Device.StartTimer(TimeSpan.FromSeconds(0.25), () =>
            {
                (sender as Button).BackgroundColor = Color.DarkGray;
                return false;
            });
        }

        private void ButtonOfflineOrders_Clicked(object sender, EventArgs e)
        {
            (sender as Button).BackgroundColor = Color.FromHex("#fa7a09");
            Device.StartTimer(TimeSpan.FromSeconds(0.25), () =>
            {
                (sender as Button).BackgroundColor = Color.DarkOrange;
                return false;
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            (sender as Button).BackgroundColor = Color.FromHex("#fa7a09");
            Device.StartTimer(TimeSpan.FromSeconds(0.25), () =>
            {
                (sender as Button).BackgroundColor = Color.DarkOrange;
                return false;
            });
        }
    }
}
