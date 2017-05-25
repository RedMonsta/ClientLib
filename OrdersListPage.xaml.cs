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
    public partial class OrdersListPage : ContentPage
    {
        ApplicationViewModel viewModel;
        //string ClientLogin = "";

        protected override async void OnAppearing()
        {
            var isEmpty = await viewModel.GetOrders();
            if (isEmpty) lblError.IsVisible = true;
            else lblError.IsVisible = false;
            base.OnAppearing();
        }

        public OrdersListPage(string login, string passwd, bool isoffline)
        {
            InitializeComponent();
            //ClientLogin = login;
            viewModel = new ApplicationViewModel("orderslistpage") { Navigation = this.Navigation, ClientLogin = login, ClientPassword = passwd, IsOfflineMode = isoffline };           
            BindingContext = viewModel;
            //Title = "Заказы пользователя " + login;
            Title = login + " orders";
            //if (isEmpty) lblError.IsVisible = true;
            //else lblError.IsVisible = false;
            //lblError.IsVisible = true;
        }
    }
}
