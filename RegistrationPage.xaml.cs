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
    }
}