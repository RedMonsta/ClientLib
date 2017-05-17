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

        protected override async void OnAppearing()
        {
            await viewModel.GetDishes();
            base.OnAppearing();
        }

        public DishesListPage(string login)
        {
            InitializeComponent();
            viewModel = new ApplicationViewModel() { Navigation = this.Navigation };
            BindingContext = viewModel;
            lblLogin.Text = login;
        }
    }
}
