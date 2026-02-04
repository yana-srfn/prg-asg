using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prg_asg
{
    class Customer
    {
        public string Name { get; }
        public string Email { get; }

        public List<Order> Orders { get; }

        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
            Orders = new List<Order>();
        }
    }
}
