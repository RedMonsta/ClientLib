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
    public partial class RegistrationPage : ContentPage
    {
        public Client ClientParam { get; private set; }
        public ApplicationViewModel ViewModel { get; private set; }

        public RegistrationPage(bool isoffline)
        {
            InitializeComponent();
            ViewModel = new ApplicationViewModel("registrationpage") { Navigation = this.Navigation, IsOfflineMode = isoffline };
            ClientParam = new Client();
            //lblMessage.IsVisible = false;
            BindingContext = this;
        }

        public RegistrationPage(Client model, ApplicationViewModel viewModel)
        {
            InitializeComponent();
            ClientParam = model;
            ViewModel = viewModel;
            this.BindingContext = this;
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

        private void ButtonRegistrate_Clicked(object sender, EventArgs e)
        {
            (sender as Button).BackgroundColor = Color.FromHex("#fa7a09");
            Device.StartTimer(TimeSpan.FromSeconds(0.25), () =>
            {
                (sender as Button).BackgroundColor = Color.DarkOrange;
                return false;
            });
        }

        private void EntryLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as Entry).Text;
            if (text.Length > 32)
            {
                text = text.Remove(text.Length - 1);
                (sender as Entry).Text = text;
            }
        }

        private void EntryPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as Entry).Text;
            if (text.Length > 50)
            {
                text = text.Remove(text.Length - 1);
                (sender as Entry).Text = text;
            }
        }
    }
}