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
    public partial class StartPage : ContentPage
    {
        ApplicationViewModel ViewModel { get; set; }

        public StartPage()
        {
            InitializeComponent();
            ViewModel = new ApplicationViewModel("startpage") { Navigation = this.Navigation };
            BindingContext = ViewModel;
        }

        private void btnStart_Clicked(object sender, EventArgs e)
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