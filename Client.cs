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

        public override bool Equals(object obj)
        {
            Client client = obj as Client;
            return this.Login == client.Login;
        }
    }
}
