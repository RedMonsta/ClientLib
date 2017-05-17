using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympFoodClient
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Dish { get; set; }
        public string Status { get; set; }
        public string Nickname { get; set; }

        public override bool Equals(object obj)
        {
            Order ord = obj as Order;
            return this.Id == ord.Id;
        }
    }
}
