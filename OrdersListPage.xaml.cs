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

        protected override async void OnAppearing()
        {
            await viewModel.GetOrders();
            base.OnAppearing();
        }

        public OrdersListPage(bool isEmpty)
        {
            InitializeComponent();
            viewModel = new ApplicationViewModel() { Navigation = this.Navigation };           
            BindingContext = viewModel;
            if (isEmpty) lblError.IsVisible = true;
            else lblError.IsVisible = false;
        }
    }
}
