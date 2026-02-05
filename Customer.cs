//==========================================================
// Student Number : S10275337C
// Student Name : Dayana Sharafeena
// Student Name : Ng Sook Min Calista
//==========================================================

using System.Collections.Generic;

namespace PRGAssignment
{
    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Order> OrderList { get; set; } = new List<Order>();
    }
}
