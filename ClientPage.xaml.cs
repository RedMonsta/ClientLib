﻿using System;
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
    }
}