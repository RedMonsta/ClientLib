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
            viewModel = new ApplicationViewModel("disheslistpage", login, passwd) {  Navigation = this.Navigation, IsOfflineMode = isoffline };
            BindingContext = viewModel;
            lblLogin.Text = login;
            //Title = login;
        }
    }
}
