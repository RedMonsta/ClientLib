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
        public bool LabelsOn { get; private set; }
        public bool EntriesOn { get; private set; }

        public OrderPage(Order model, ApplicationViewModel viewModel, bool isExistOrder)
        {
            InitializeComponent();
            OrderParam = model;
            if (isExistOrder)
            {
                LabelsOn = true;
                EntriesOn = false;
            } else
            {
                LabelsOn = false;
                EntriesOn = true;
            }
            ViewModel = viewModel;           
            this.BindingContext = this;
        }

        public OrderPage(Order model, ApplicationViewModel viewModel, string dishname)
        {
            InitializeComponent();
            OrderParam = model;
            OrderParam.Dish = dishname;
            LabelsOn = false;
            EntriesOn = true;
            ViewModel = viewModel;
            this.BindingContext = this;
        }
    }
}
