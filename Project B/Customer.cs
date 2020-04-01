using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class Customer
    {
        private string customerName;
        private DateTime? birthday;
        public int age { get; set; }
        public string email { get; set; }

        public Customer(string customerName, DateTime? birthday, string email)
        {
            this.customerName = customerName;
            this.birthday = birthday;
            this.email = email;
        }
    }
}
