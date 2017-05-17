using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace OlympFoodClient
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        bool initialized = false; 
        Dish selectedDish;
        Order selectedOrder;
        private bool isBusy;
        public string ClientLogin = "Hello";

        public ObservableCollection<Dish> Dishes { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        DishService dishesService = new DishService();
        OrderService ordersService = new OrderService();
        ClientService clientService = new ClientService();
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BackCommand { protected set; get; }
        public ICommand UpdateCommand { protected set; get; }
        public ICommand CreateOrderCommand { protected set; get; }
        public ICommand DeleteOrderCommand { protected set; get; }
        public ICommand SaveOrderCommand { protected set; get; }
        public ICommand LoadOrdersCommand { protected set; get; }
        public ICommand SignInCommand { protected set; get; }
        public ICommand RegistrationCommand { protected set; get; }
        public ICommand GoToRegistrationCommand { protected set; get; }

        public ICommand StartCommand { protected set; get; }

        public INavigation Navigation { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("IsLoaded");
            }
        }


        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        public ApplicationViewModel()
        {
            Dishes = new ObservableCollection<Dish>();
            Orders = new ObservableCollection<Order>();
            IsBusy = false;
            BackCommand = new Command(Back);
            UpdateCommand = new Command(UpdateDishes);
            CreateOrderCommand = new Command(CreateOrder);
            DeleteOrderCommand = new Command(DeleteOrder);
            SaveOrderCommand = new Command(SaveOrder);
            LoadOrdersCommand = new Command(LoadOrders);
            SignInCommand = new Command(CheckClient);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            RegistrationCommand = new Command(RegistrateClient);
            GoToRegistrationCommand = new Command(GoToRegistration);

            StartCommand = new Command(RunApp);
        }

        public async void GoToRegistration()
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        public async void RegistrateClient(object cliObject)
        {
            Client client = cliObject as Client;

            if (client != null)
            {
                IsBusy = true;
                Client regclt = await clientService.Registrate(client);
                if (regclt.Login == "__IsExist__")
                {
                    ClientLogin = "Such user is exist";                   
                }
                else
                {
                    ClientLogin = client.Login + client.Password;                   
                }
                await Navigation.PushAsync(new DishesListPage(ClientLogin));
                IsBusy = false;
            }
        }

        public async void RunApp()
        {
            await Navigation.PushAsync(new ClientPage());
        }

        public async void CheckClient(object cliObject)
        {
            Client client = cliObject as Client;

            if (client != null)
            {
                IsBusy = true;
                bool isAuthorized = await clientService.Check(client);
                //string AuthLine = await clientService.Check(client);
                if (isAuthorized)
                {
                    ClientLogin = client.Login+client.Password;
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    await Navigation.PushAsync(new DishesListPage(ClientLogin));
                }
                else await Navigation.PushAsync(new DishesListPage("Non authorized"));
                IsBusy = false;

            }
        }

        public Dish SelectedDish
        {
            get { return selectedDish; }
            set
            {
                if (selectedDish != value)
                {
                    Dish tempDish = new Dish()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Energy_value = value.Energy_value,
                        Price = value.Price
                    };
                    selectedDish = null;
                    OnPropertyChanged("SelectedDish");
                    Navigation.PushAsync(new DishPage(tempDish, this));
                }
            }
        }
        

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                if (selectedOrder != value)
                {
                    Order tempOrd = new Order()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Surname = value.Surname,
                        Phone = value.Phone,
                        Address = value.Address,
                        Dish = value.Dish,
                        Status = value.Status,
                        Nickname = value.Nickname
                    };
                    selectedOrder = null;
                    OnPropertyChanged("SelectedOrder");
                    Navigation.PushAsync(new OrderPage(tempOrd, this));
                }
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void LoadOrders()
        {
            Navigation.PushAsync(new OrdersListPage(Orders.Any()));
        }

        private async void CreateOrder()
        {
            await Navigation.PushAsync(new OrderPage(new Order(), this));
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        public async void UpdateDishes()
        {
            IsBusy = true;
            IEnumerable<Dish> dishes = await dishesService.Get();

            //Dishes.Clear();
            while (Dishes.Any())
                Dishes.RemoveAt(Dishes.Count - 1);

            foreach (Dish f in dishes)
                Dishes.Add(f);
            IsBusy = false;
        }

        public async Task GetDishes()
        {
            if (initialized == true) return;
            IsBusy = true;
            IEnumerable<Dish> dishes = await dishesService.Get();

            //Dishes.Clear();
            while (Dishes.Any())
                Dishes.RemoveAt(Dishes.Count - 1);

            foreach (Dish f in dishes)
                Dishes.Add(f);

            IsBusy = false;
            initialized = true;
        }

        public async Task GetOrders()
        {
            if (initialized == true) return;
            IsBusy = true;
            IEnumerable<Order> orders = await ordersService.Get();
         
            //Dishes.Clear();
            while (Orders.Any())
                Orders.RemoveAt(Orders.Count - 1);
         
            foreach (Order f in orders)
                Orders.Add(f);
            IsBusy = false;
            initialized = true;
        }

        private async void SaveOrder(object ordObject)
        {
            Order ord = ordObject as Order;
            ord.Status = "Ожидает отправки";
            ord.Nickname = "RedMonsta";

            if (ord != null)
            {
                IsBusy = true;
                if (ord.Id > 0)
                {
                    Order updatedOrder = await ordersService.Update(ord);
                    if (updatedOrder != null)
                    {
                        int pos = Orders.IndexOf(updatedOrder);
                        Orders.RemoveAt(pos);
                        Orders.Insert(pos, updatedOrder);
                    }
                }
                else
                {
                    Order addedOrder = await ordersService.Add(ord);
                    if (addedOrder != null)
                        Orders.Add(addedOrder);
                }
                IsBusy = false;
            }
            Back();
        }

        private async void DeleteOrder(object ordObject)
        {
            Order ord = ordObject as Order;
            if (ord != null)
            {
                IsBusy = true;
                Order deletedOrd = await ordersService.Delete(ord.Id);
                if (deletedOrd != null)
                {
                    Orders.Remove(deletedOrd);
                }
                IsBusy = false;
            }
            Back();
        }
    }
}
