using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympFoodClient
{
    public class Client
    {
        //public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        private int status { get; } = 1;

        public override bool Equals(object obj)
        {
            Client client = obj as Client;
            return this.Login == client.Login;
        }
    }

    public class ClientPlusOrder
    {
        public Client client { get; set; }
        public Order order { get; set; }
    }
}
