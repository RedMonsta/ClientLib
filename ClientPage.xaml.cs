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
    public partial class ClientPage : ContentPage
    {
        public Client ClientParam { get; private set; }
        public ApplicationViewModel ViewModel { get; private set; }

        public ClientPage(bool isoffline)
        {
            InitializeComponent();
            ViewModel = new ApplicationViewModel("clientpage") { Navigation = this.Navigation, IsOfflineMode = isoffline };
            //ClientParam = new Client { Login = "User", Id = 0, Password = "qwerty" } ;
            //ViewModel.IsOfflineMode = false;
            //ViewModel.IsBusy = false;
            ClientParam = new Client();
            BindingContext = this;
        }

        public ClientPage(Client model, ApplicationViewModel viewModel)
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

        private void EntryLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as Entry).Text;
            if (text.Length > 32)
            {
                text = text.Remove(text.Length - 1);
                (sender as Entry).Text = text;
            }
        }

        private void EntryPassword_TextChanged_1(object sender, TextChangedEventArgs e)
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