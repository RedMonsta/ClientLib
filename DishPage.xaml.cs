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
    public partial class DishPage : ContentPage
    {
        public TextDish Model { get; private set; }
        public ApplicationViewModel ViewModel { get; private set; }

        public DishPage(TextDish model, ApplicationViewModel viewModel)
        {
            InitializeComponent();
            Model = model;
            ViewModel = viewModel;
            this.BindingContext = this;
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
