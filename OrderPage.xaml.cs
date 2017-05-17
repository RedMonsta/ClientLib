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
    public partial class OrderPage : ContentPage
    {
        public Order OrderParam { get; private set; }
        public ApplicationViewModel ViewModel { get; private set; }

        public OrderPage(Order model, ApplicationViewModel viewModel)
        {
            InitializeComponent();
            OrderParam = model;
            ViewModel = viewModel;
            this.BindingContext = this;
        }
    }
}
