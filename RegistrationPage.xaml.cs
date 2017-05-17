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

        public RegistrationPage()
        {
            InitializeComponent();
            ViewModel = new ApplicationViewModel() { Navigation = this.Navigation };
            ClientParam = new Client();
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