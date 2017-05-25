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
    public partial class OrdersListPage : ContentPage
    {
        ApplicationViewModel viewModel;
        //string ClientLogin = "";

        protected override async void OnAppearing()
        {
            var isEmpty = await viewModel.GetOrders();
            viewModel.StatementText = "See your orders.";
            if (isEmpty) lblError.IsVisible = true;
            else lblError.IsVisible = false;
            base.OnAppearing();
        }

        public OrdersListPage(string login, string passwd, bool isoffline)
        {
            InitializeComponent();
            //ClientLogin = login;
            viewModel = new ApplicationViewModel("orderslistpage") { Navigation = this.Navigation, ClientLogin = login, ClientPassword = passwd, IsOfflineMode = isoffline };           
            BindingContext = viewModel;
            //Title = "Заказы пользователя " + login;
            Title = login + " orders";
            //if (isEmpty) lblError.IsVisible = true;
            //else lblError.IsVisible = false;
            //lblError.IsVisible = true;
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
