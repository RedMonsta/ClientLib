using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Settings;
using System;


namespace OlympFoodClient
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        bool initialized = false;
        Dish selectedDish;
        Order selectedOrder;
        string choosendishname;
        private bool isBusy;
        private bool isExistUser;
        private bool isOfflineMode;
        private bool isOnlineMode;
        public string ClientLogin = "#DefaultNickname#";
        public string ClientPassword = "#DefaultPassword#";
        public readonly string SettingsFilename = "Settings.txt";
        public readonly string SavedOrdersFilename = "SavedOrders.txt";

        public string lblMessageText;

        //string UserSettingsNickname = "#DefaultNickname#";
        //bool UserSettingsAuthorised = false;

        //string NicknameFromFile = "#DefaultNickname#";

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
        public ICommand ExitClientCommand { protected set; get; }
        public ICommand TryToConnectCommand { protected set; get; }
        public ICommand DishToOrderCommand { protected set; get; }

        public INavigation Navigation { get; set; }

        public bool IsOfflineMode
        {
            get { return isOfflineMode; }
            set
            {
                isOfflineMode = value;
                isOnlineMode = !value;
                OnPropertyChanged("IsOfflineMode");
                OnPropertyChanged("IsOnlineMode");
            }
        }

        public bool IsOnlineMode
        {
            get { return isOnlineMode; }
            set
            {
                isOfflineMode = !value;
                isOnlineMode = value;
                OnPropertyChanged("IsOfflineMode");
                OnPropertyChanged("IsOnlineMode");
            }
        }

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

        public bool IsExistUser
        {
            get { return isExistUser; }
            set
            {
                isExistUser = value;
                OnPropertyChanged("IsExistUser");
 
            }
        }

        public string StatementText
        {
            get { return lblMessageText; }
            set
            {
                lblMessageText = value;
                OnPropertyChanged("StatementText");
            }
        }


        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        public ApplicationViewModel(string pagetype)
        {
            Dishes = new ObservableCollection<Dish>();
            Orders = new ObservableCollection<Order>();
            IsBusy = false;
            IsExistUser = false;
            IsOfflineMode = false;
            IsOnlineMode = !IsOfflineMode;
            StatementText = "Введите новый никнейм";
            BackCommand = new Command(Back);
            UpdateCommand = new Command(UpdateDishes);
            CreateOrderCommand = new Command(CreateOrder);
            DeleteOrderCommand = new Command(DeleteOrder);
            SaveOrderCommand = new Command(SaveOrder);
            LoadOrdersCommand = new Command(LoadOrders);
            SignInCommand = new Command(CheckClient);
            RegistrationCommand = new Command(RegistrateClient);
            GoToRegistrationCommand = new Command(GoToRegistration);
            StartCommand = new Command(RunApp);
            ExitClientCommand = new Command(ExitToAuthorization);
            TryToConnectCommand = new Command(TryToConnect);

            DishToOrderCommand = new Command(ChooseDishToOrder);

            if (pagetype == "startpage")
            {
                LoadSavedUser();
                //SaveUserToFile(ClientLogin, ClientPassword);
            }
            if (pagetype == "disheslistpage")
            {
                //SaveUserToFile(ClientLogin, ClientPassword);
            }
            if (pagetype == "clientpage")
            {
                StatementText = "Авторизация";
            }
        }


        public ApplicationViewModel(string pagetype, string login, string passwd)
        {
            Dishes = new ObservableCollection<Dish>();
            Orders = new ObservableCollection<Order>();
            IsBusy = false;
            IsOfflineMode = false;
            IsOnlineMode = !IsOfflineMode;
            BackCommand = new Command(Back);
            UpdateCommand = new Command(UpdateDishes);
            CreateOrderCommand = new Command(CreateOrder);
            DeleteOrderCommand = new Command(DeleteOrder);
            SaveOrderCommand = new Command(SaveOrder);
            LoadOrdersCommand = new Command(LoadOrders);
            SignInCommand = new Command(CheckClient);
            RegistrationCommand = new Command(RegistrateClient);
            GoToRegistrationCommand = new Command(GoToRegistration);
            StartCommand = new Command(RunApp);
            ExitClientCommand = new Command(ExitToAuthorization);
            TryToConnectCommand = new Command(TryToConnect);

            DishToOrderCommand = new Command(ChooseDishToOrder);

            ClientLogin = login;
            ClientPassword = passwd;

            if (pagetype == "startpage")
            {
                LoadSavedUser();
                //SaveUserToFile(ClientLogin, ClientPassword);
            }
            if (pagetype == "disheslistpage")
            {
                SaveUserToFile(ClientLogin, ClientPassword);
            }
            
        }

        public async void ChooseDishToOrder()
        {
            await Navigation.PushAsync(new OrderPage(new Order(), this, choosendishname));
        }

        public async void ExitToAuthorization()
        {
            ClientLogin = "#DefaultNickname#";
            ClientPassword = "#DefaultPassword#";
            await DependencyService.Get<IFileWorker>().SaveTextAsync(SettingsFilename, ClientLogin + " " + ClientPassword);
            StatementText = "Авторизация";
            await Navigation.PushAsync(new ClientPage(IsOfflineMode));
            var existingpages = Navigation.NavigationStack.ToList();
            //Navigation.RemovePage(existingpages[0]);
            for (int i = existingpages.Count - 2; i >= 0 ; i--)
            {
                Navigation.RemovePage(existingpages[i]);
            }
        }

        public async void LoadSavedUser()
        {
            //string filename = "Settings.txt";
            //ClientLogin = await DependencyService.Get<IFileWorker>().LoadTextAsync(filename);
            //await DependencyService.Get<IFileWorker>().SaveTextAsync(filename, "#HOLOCOST228");

            if (await DependencyService.Get<IFileWorker>().ExistsAsync(SettingsFilename))
            {
                string info = await DependencyService.Get<IFileWorker>().LoadTextAsync(SettingsFilename);
                string[] cliarr = info.Split(' ');
                ClientLogin = cliarr[0];
                ClientPassword = cliarr[1];
            }
            else
            {
                ClientLogin = "#DefaultNickname#";
                ClientPassword = "#DefaultPassword#";
            }

            
        }

        public async void SaveUserToFile(string login, string passwd)
        {
            string info = login + " " + passwd;
            //info = "RedMonsta 12345";
            await DependencyService.Get<IFileWorker>().SaveTextAsync(SettingsFilename, info);
        }

        public async void GoToRegistration()
        {
            await Navigation.PushAsync(new RegistrationPage(IsOfflineMode));
        }

        public async void RegistrateClient(object cliObject)
        {
            Client client = cliObject as Client;

            if (client != null)
            {
                IsBusy = true;
                //IsExistUser = false;
                Client regclt = await clientService.Registrate(client);
                if (regclt.Login == "#RequestException#")
                {
                    IsOfflineMode = true;
                    ClientLogin = "#DefaultNickname#";
                    ClientPassword = "#DefaultPassword#";
                }
                else
                {
                    if (regclt.Login == "#ExistNickname#")
                    {
                        // ClientLogin = "Such user is exist";     
                        ClientLogin = "#DefaultNickname#";
                        ClientPassword = "#DefaultPassword#";
                        //var existingpages = Navigation.NavigationStack.ToList();
                        IsExistUser = true;
                        StatementText = "Пользователь с таким ником уже существует";
                        //lblMessageText = "Пользователь с таким ником уже существует";
                    }
                    else
                    {
                        ClientLogin = client.Login;
                        ClientPassword = client.Password;
                        IsExistUser = false;
                        await Navigation.PushAsync(new DishesListPage(ClientLogin, ClientPassword, IsOfflineMode));
                        var existingpages = Navigation.NavigationStack.ToList();
                        //StatementText = "Введите новый никнейм";
                        for (int i = existingpages.Count - 2; i >= 0; i--)
                        {
                            Navigation.RemovePage(existingpages[i]);
                        }
                    }
                }
                IsBusy = false;
            }
        }

        public async void TryToConnect()
        {
            var client = new Client { Login = ClientLogin, Password = ClientPassword };
            //bool isAuthorized = await clientService.Check(client);
            IsOfflineMode = await clientService.CheckConnection();
            IsOnlineMode = !IsOfflineMode;
        }

        public async void RunApp()
        {
            IsOfflineMode = await clientService.CheckConnection();
            if (!IsOfflineMode)
            {
                if (ClientLogin == "")
                {
                    ClientLogin = "#DefaultNickname#";
                    ClientPassword = "#DefaultPassword#";
                }
                if (ClientLogin != "#DefaultNickname#")
                {
                    //StatementText = ClientLogin;
                    var client = new Client { Login = ClientLogin, Password = ClientPassword };
                    IsBusy = true;
                    bool isAuthorized = await clientService.Check(client);
                    if (isAuthorized)
                    {
                        // await Navigation.PopAsync();
                        await Navigation.PushAsync(new DishesListPage(ClientLogin, ClientPassword, IsOfflineMode));
                    }
                    else
                    {
                        ClientLogin = "#DefaultNickname#";
                        ClientPassword = "#DefaultPassword#";
                        await Navigation.PushAsync(new ClientPage(IsOfflineMode));
                    }
                    IsBusy = false;
                }
                else
                {
                    //StatementText = "Hello i'm here" + ClientLogin;
                    await Navigation.PushAsync(new ClientPage(IsOfflineMode));
                    var existingpages = Navigation.NavigationStack.ToList();
                    Navigation.RemovePage(existingpages[0]);
                }

            }
            else
            {
                if (ClientLogin != "#DefaultNickname#")
                {
                    await Navigation.PushAsync(new DishesListPage(ClientLogin, ClientPassword, IsOfflineMode));
                    StatementText = ClientLogin + ClientPassword + IsOfflineMode.ToString();
                    var existingpages = Navigation.NavigationStack.ToList();
                    for (int i = existingpages.Count - 2; i >= 0; i--)
                    {
                        Navigation.RemovePage(existingpages[i]);
                    }
                }
                else
                {
                    //StatementText = "Offline " + ClientLogin;
                    await Navigation.PushAsync(new ClientPage(IsOfflineMode));
                    var existingpages = Navigation.NavigationStack.ToList();
                    Navigation.RemovePage(existingpages[0]);
                }
            }
            //await Navigation.PushAsync(new DishesListPage(ClientLogin, ClientPassword));
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
                    ClientLogin = client.Login;
                    ClientPassword = client.Password;
                    //await Navigation.PopAsync();                   
                    await Navigation.PushAsync(new DishesListPage(ClientLogin, ClientPassword, IsOfflineMode));
                    //await Navigation.NavigationStack[0].
                }
                else
                {
                    
                    ClientLogin = "#DefaultNickname#";
                    ClientPassword = "#DefaultPassword#";                    
                    IsOfflineMode = await clientService.CheckConnection();
                    if (!IsOfflineMode) StatementText = "Нету подключения";
                    //await Navigation.PopAsync();
                    //await Navigation.PushAsync(new DishesListPage("Non authorized", "Non authorized#"));
                }
                IsBusy = false;
                var existingpages = Navigation.NavigationStack.ToList();
                for (int i = existingpages.Count - 2; i >= 0; i--)
                {
                    Navigation.RemovePage(existingpages[i]);
                }
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
                    choosendishname = tempDish.Name;
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
                        //Surname = value.Surname,
                        Phone = value.Phone,
                        Address = value.Address,
                        Dish = value.Dish,
                        Status = value.Status,
                        Nickname = value.Nickname
                    };
                    selectedOrder = null;
                    OnPropertyChanged("SelectedOrder");
                    Navigation.PushAsync(new OrderPage(tempOrd, this, true));
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
            //Navigation.PopAsync();

            Navigation.PushAsync(new OrdersListPage(ClientLogin, ClientPassword, IsOfflineMode));
            //var existingpages = Navigation.NavigationStack.ToList();
            //Navigation.RemovePage(existingpages[1]);
            //ClientLogin = existingpages[0].ToString();
            // Navigation.PushAsync(new DishesListPage(ClientLogin));


            //Navigation.RemovePage(existingpages[existingpages.Count - 2]);
        }

        private async void CreateOrder()
        {
            await Navigation.PushAsync(new OrderPage(new Order(), this, false));
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        public async void UpdateDishes()
        {
            if (!IsOfflineMode)
            {
                IsBusy = true;
                IEnumerable<Dish> dishes = await dishesService.Get();

                //Dishes.Clear();
                while (Dishes.Any())
                    Dishes.RemoveAt(Dishes.Count - 1);

                foreach (Dish f in dishes)
                    Dishes.Add(f);

                if (Dishes.Count == 1 && Dishes[0].Name == "#RequestException#")
                {
                    Dishes.Clear();
                    IsOfflineMode = true;
                    IsOnlineMode = false;
                }

                IsBusy = false;
            }
        }

        public async Task GetDishes()
        {
            if (!IsOfflineMode)
            {
                if (initialized == true) return;
                IsBusy = true;
                IEnumerable<Dish> dishes = await dishesService.Get();

                //Dishes.Clear();
                while (Dishes.Any())
                    Dishes.RemoveAt(Dishes.Count - 1);

                foreach (Dish f in dishes)
                    Dishes.Add(f);

                if (Dishes.Count == 1 && Dishes[0].Name == "#RequestException#")
                {
                    Dishes.Clear();
                    IsOfflineMode = true;
                    IsOnlineMode = false;
                }

                IsBusy = false;
                initialized = true;
            }
            //else 
        }

        public async Task<bool> GetOrders()
        {
            if (!IsOfflineMode)
            {
                bool result = false;
                if (initialized == true) return result;
                IsBusy = true;
                IEnumerable<Order> orders = await ordersService.Get(ClientLogin, ClientPassword);

                //Orders.Clear();
                while (Orders.Any())
                    Orders.RemoveAt(Orders.Count - 1);

                foreach (Order f in orders)
                    Orders.Add(f);

                if (Orders.Count == 1 && Orders[0].Name == "#RequestException#")
                {
                    Orders.Clear();
                    IsOfflineMode = true;
                    IsOnlineMode = false;
                    var tmpords = await LoadOrdersFromFile();
                    foreach (Order f in tmpords)
                        Orders.Add(f);
                }
                if (Orders.Count == 0) result = true;
                else result = false;
                //StatementText = "в файл " + orders.ToList()[0].Name + " " + ClientPassword;
                StatementText = Authorizer.EncryptStringByBase64(ClientPassword) + " " + Authorizer.DecryptStringByBase64(Authorizer.EncryptStringByBase64(ClientPassword));
                //if (Orders.Count > 0)
                    SaveOrdersToFile(Orders);

                IsBusy = false;
                initialized = true;
                return result;
                //return true;
            }
            else
            {
                while (Orders.Any())
                    Orders.RemoveAt(Orders.Count - 1);
                var tmpords = await LoadOrdersFromFile();
                //StatementText = "Я перед загрузкой из файла";
                //StatementText = await LoadLineFromFile();
                foreach (Order f in tmpords)
                    Orders.Add(f);
                //StatementText = Orders.Count.ToString();
                if (Orders.Count == 0) return true;
                else return false;
                //return true;
            }
        }

        private async void SaveOrder(object ordObject)
        {
            if (!IsOfflineMode)
            {
                Order ord = ordObject as Order;
                ord.Status = "Ожидает отправки";
                //ord.Nickname = "RedMonsta";
                ord.Nickname = ClientLogin;

                if (ord != null)
                {
                    IsBusy = true;
                    if (ord.Id > 0)
                    {
                        //Order updatedOrder = await ordersService.Update(ord);
                        //if (updatedOrder != null)
                        //{
                        //    int pos = Orders.IndexOf(updatedOrder);
                        //    Orders.RemoveAt(pos);
                        //    Orders.Insert(pos, updatedOrder);
                        //}
                    }
                    else
                    {
                        Order addedOrder = await ordersService.Add(ord, ClientLogin, ClientPassword);
                        if (addedOrder != null)
                        {
                            if (addedOrder.Name == "#RequestException#")
                            {
                                IsOfflineMode = true;
                                IsOnlineMode = false;
                            }
                            else Orders.Add(addedOrder);
                        }
                            
                    }
                    IsBusy = false;
                }
                Back();
            } 
        }

        private async void DeleteOrder(object ordObject)
        {
            if (!IsOfflineMode)
            {
                Order ord = ordObject as Order;
                if (ord != null)
                {
                    IsBusy = true;
                    Order deletedOrd = await ordersService.Delete(ord.Id, ClientLogin, ClientPassword);
                    if (deletedOrd != null)
                    {
                        if (deletedOrd.Name == "#RequestException#")
                        {
                            IsOfflineMode = true;
                            IsOnlineMode = false;
                        }
                        else
                        {
                            Orders.Remove(deletedOrd);
                            //if (Orders.Count > 0)
                                SaveOrdersToFile(Orders);
                        }
                    }
                    IsBusy = false;
                }
                Back();
            }
        }

        private async void SaveOrdersToFile(ObservableCollection<Order> ordlist)
        {
            string info = "";          
            foreach (var order in ordlist)
            {
                string oneordstr = order.Id.ToString() + "♪" + order.Name + "♪" + order.Phone + "♪" + order.Address + "♪" + order.Dish + "♪" + order.Status + "♪" + order.Nickname;
                info = info + oneordstr + "►";
            }
            if (info.Length > 0) info = info.Remove(info.Length - 1);
            await DependencyService.Get<IFileWorker>().SaveTextAsync(SavedOrdersFilename, info);
        }

        private async Task<ObservableCollection<Order>> LoadOrdersFromFile()
        {
            ObservableCollection<Order> reslist = new ObservableCollection<Order>();
            if (await DependencyService.Get<IFileWorker>().ExistsAsync(SavedOrdersFilename))
            {
                string info = await DependencyService.Get<IFileWorker>().LoadTextAsync(SavedOrdersFilename);
                if (info.Length > 0)
                {
                    string[] OrdersArray = info.Split('►');
                    foreach (var strorder in OrdersArray)
                    {
                        string[] fieldsarr = strorder.Split('♪');
                        Order tmpord = new Order();
                        tmpord.Id = Convert.ToInt32(fieldsarr[0]);
                        tmpord.Name = fieldsarr[1];
                        tmpord.Phone = fieldsarr[2];
                        tmpord.Address = fieldsarr[3];
                        tmpord.Dish = fieldsarr[4];
                        tmpord.Status = fieldsarr[5];
                        tmpord.Nickname = fieldsarr[6];
                        reslist.Add(tmpord);
                    }
                }
            }
            return reslist;
        }
    }
}
