using System;

namespace Suggester.APIv2{
    public class Order{
        public int Id{get; set;}
        public Customer Customer{get; set;}
        public DateTime Placed{get; set;}
        public DateTime? Completed{get; set;}
    }
}