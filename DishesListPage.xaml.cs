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
    public partial class DishesListPage : ContentPage
    {
        ApplicationViewModel viewModel;
        //string ClientLogin = "";

        protected override async void OnAppearing()
        {
            await viewModel.GetDishes();            
            base.OnAppearing();
        }

        public DishesListPage(string login, string passwd)
        {
            InitializeComponent();
            //ClientLogin = login;
            viewModel = new ApplicationViewModel("disheslistpage", login, passwd) {  Navigation = this.Navigation };
            BindingContext = viewModel;
            lblLogin.Text = login;
            //Title = login;
        }
    }
}
